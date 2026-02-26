using System.Domain.Entities.UserAgr;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace System.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.AddSeeding();
        }

        public DbSet<T> Entity<T>()
            where T : class => Set<T>();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }

    public static class SeedingExtension
    {
        public static ModelBuilder AddSeeding(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(UserSeedData.GetUser());
            modelBuilder
                .Entity<User>()
                .OwnsOne(u => u.Password)
                .HasData(UserSeedData.GetPassword());
            modelBuilder
                .Entity<User>()
                .OwnsOne(u => u.PhoneNumber)
                .HasData(UserSeedData.GetPhoneNumber());

            return modelBuilder;
        }
    }

    public static class UserSeedData
    {
        public static Guid UserId = Guid.NewGuid();

        public static object GetUser() =>
            new
            {
                Id = UserId,
                UserName = "admin",
                Email = "lamnv9611@gmail.com",
                CreatedOn = DateTime.UtcNow.Date,
                CreatedBy = Guid.Empty,
                UpdatedOn = DateTime.UtcNow.Date,
                UpdatedBy = Guid.Empty,
                IsActive = true,
            };

        public static object GetPassword()
        {
            var salt = PasswordHasher.GenerateSalt();
            string passwordHash = PasswordHasher.HashPassword("admin@1234", salt);

            return new
            {
                UserId = UserId,
                PasswordHash = passwordHash,
                Salt = salt,
            };
        }

        public static object GetPhoneNumber() =>
            new
            {
                UserId = UserId,
                Code = "+84",
                Phone = "869929410",
            };
    }
}
