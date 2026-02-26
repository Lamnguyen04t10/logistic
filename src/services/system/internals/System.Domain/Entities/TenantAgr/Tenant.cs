using Core.Entity;

namespace System.Domain.Entities.TenantAgr
{
    public class Tenant : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Website { get; private set; }
        public string Code { get; private set; }
        public TenanType Type { get; private set; }

        public Tenant() { }
    }
}
