using System.Domain.Entities.UserAgr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace System.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Users");

            builder.OwnsOne(
                o => o.Password,
                a =>
                {
                    a.Property(p => p.PasswordHash).HasColumnName("Password").IsRequired(true);

                    a.Property(p => p.Salt).HasColumnName("PasswordSalt").IsRequired(true);
                    a.WithOwner();
                }
            );

            builder.OwnsOne(
                o => o.PhoneNumber,
                a =>
                {
                    a.Property(p => p.Phone).HasColumnName("Phone").IsRequired(true);

                    a.Property(p => p.Code).HasColumnName("Code").IsRequired(true);
                    a.WithOwner();
                }
            );
        }
    }
}
