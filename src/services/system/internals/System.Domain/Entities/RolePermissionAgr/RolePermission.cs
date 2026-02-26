using System.Domain.Entities.RoleAgr;
using System.Domain.Entities.ServiceAgr;
using Core.Entity;

namespace System.Domain.Entities.RolePermissionAgr
{
    public class RolePermission : BaseEntity
    {
        public Guid ServiceId { get; private set; }
        public Service Service { get; private set; }
        public int PermissionValue { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }

        public RolePermission() { }

        public RolePermission(Guid serviceId, Guid roleId)
        {
            SetId();
            ServiceId = serviceId;
            RoleId = roleId;
        }
    }
}
