using System.Diagnostics;
using System.Text.Json;

namespace System.WebApi.Abstractions
{
    public class RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger,
        IConfiguration configuration
    )
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<RequestLoggingMiddleware> _logger = logger;
        private readonly bool _enableLogging = configuration.GetValue<bool>("EnableRequestLogging");

        public async Task Invoke(HttpContext context)
        {
            if (!_enableLogging)
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();
            var user = context.User.Identity?.Name ?? "Anonymous";
            var method = context.Request.Method;
            var path = context.Request.Path;
            var query = context.Request.QueryString.ToString();
            var requestBody = await ReadRequestBody(context.Request);
            var originalBodyStream = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            stopwatch.Stop();

            var responseBodyContent = await ReadResponseBody(context.Response);
            var statusCode = context.Response.StatusCode;
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);

            var logData = new
            {
                User = user,
                Method = method,
                Path = path,
                Query = query,
                RequestBody = requestBody,
                StatusCode = statusCode,
                ResponseBody = responseBodyContent,
                ElapsedTimeMs = stopwatch.ElapsedMilliseconds,
            };

            var logMessage = JsonSerializer.Serialize(logData);
            _logger.LogInformation(logMessage);
            LogToFile(logMessage);
        }

        private static async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            if (request.Body.CanRead && request.ContentLength > 0)
            {
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body;
            }
            return string.Empty;
        }

        private static async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(response.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }

        private static void LogToFile(string logMessage)
        {
            var logDirectory = "Logs";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logFileName = $"Logs/request_{DateTime.UtcNow:yyyy-MM-dd}.log";
            File.AppendAllText(logFileName, $"{logMessage}\n");
        }
    }
}
