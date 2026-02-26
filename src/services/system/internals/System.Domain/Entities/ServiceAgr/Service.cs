using System.Domain.Entities.RolePermissionAgr;
using Core.Entity;

namespace System.Domain.Entities.ServiceAgr
{
    public class Service : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        public Service() { }

        public Service(string name, string code, string description)
        {
            SetId();
            Name = name;
            Code = code;
            Description = description;
        }
    }
}
