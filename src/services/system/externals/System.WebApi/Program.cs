using System.Application;
using System.Infrastructure;
using System.Text.Json;
using System.WebApi;
using System.WebApi.Abstractions;
using Core;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load Configuration
var conApplication = builder.Configuration.Get<AppSettings>();

var connectionString =
    conApplication?.ConnectionStrings.Application
    ?? throw new InvalidOperationException("Missing connection string");

// Configure Services
//builder.Services.AddRedis(conApplication.Configuration.Redis.Configuration);
builder.Services.AddConfigureDependencyInjection(builder.Configuration);
builder.Services.AddCustomDbContext(connectionString ?? string.Empty);

builder
    .Services.AddControllers(options =>
    {
        options.Conventions.Add(new RouteTokenTransformerConvention(new LowercaseTransformer()));
    })
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAngularDev",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddCustomDependencyInjection();
builder.Services.AddApplicationDependencyInjection();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(System.Application.AssemblyReference).Assembly)
);

var app = builder.Build();

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "System API V1"));
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");

app.UseAuthorization();
app.MapControllers();
await app.RunAsync();

public static class ConfigureExtensions
{
    public static IServiceCollection AddConfigureDependencyInjection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<Logging>(configuration.GetSection("Logging"));
        services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
        services.Configure<ServiceConfig>(configuration.GetSection("Configuration:Service"));
        services.Configure<JwtConfig>(configuration.GetSection("Configuration:Jwt"));
        services.Configure<AppSettings>(configuration);
        return services;
    }
}
