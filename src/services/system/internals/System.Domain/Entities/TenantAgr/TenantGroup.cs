using System.Domain.Entities.UserAgr;
using Core.Entity;

namespace System.Domain.Entities.TenantAgr
{
    internal class TenantGroup : BaseEntity
    {
        public Guid TenantId { get; set; }
        public virtual ICollection<Tenant> Tenants { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public string Remark { get; set; }

        public TenantGroup() { }
    }
}
