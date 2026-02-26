using Core.Enumration;

namespace System.Domain.Entities.TenantAgr
{
    public class TenanType : Enumeration
    {
        public TenanType() { }

        public TenanType(int Id, string Name)
            : base(Id, Name) { }

        public static TenanType Admin = new(1, "ADMIN");
        public static TenanType Client = new(2, "CLIENT");
    }
}
