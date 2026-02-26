using System.Domain.Entities.TenantAgr;
using Core.Enumration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace System.Infrastructure.EntityConfigurations
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        void IEntityTypeConfiguration<Tenant>.Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Tenants");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.Property(x => x.Website).HasMaxLength(255);

            var converter = new ValueConverter<TenanType, string>(
                v => v.Name,
                v => Enumeration.FromDisplayName<TenanType>(v)
            );

            builder.Property(x => x.Type).HasMaxLength(20).HasConversion(converter).IsRequired();
        }
    }
}
