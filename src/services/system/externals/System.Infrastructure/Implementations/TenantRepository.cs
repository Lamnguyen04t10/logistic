using System.Domain.Abstractions;
using System.Domain.Entities.TenantAgr;

namespace System.Infrastructure.Implementations
{
    public class TenantRepository(ApplicationDbContext context)
        : Repository<Tenant>(context),
            ITenantRepository { }
}
