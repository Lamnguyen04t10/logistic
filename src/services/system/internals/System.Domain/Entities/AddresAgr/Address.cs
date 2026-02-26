using System.Domain.Entities.TenantAgr;
using System.Domain.Entities.UserAgr;
using Core.Entity;

namespace System.Domain.Entities.AddresAgr
{
    public partial class Address : BaseEntity
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public int Position { get; private set; }
        public bool IsDefault { get; private set; }
        public string ZipCode { get; private set; }
        public Guid? UserId { get; private set; }
        public virtual User User { get; private set; }
        public Guid? TenantId { get; private set; }
        public virtual Tenant Tenant { get; private set; }
    }
}
