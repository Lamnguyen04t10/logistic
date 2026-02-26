using System.Domain.Entities.RolePermissionAgr;
using System.Domain.Entities.UserAgr;
using Core.Entity;

namespace System.Domain.Entities.RoleAgr
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public ICollection<RolePermission> RolePermissions { get; private set; }

        public Role() { }

        public Role(string name, string code)
        {
            SetId();
            Name = name;
            Code = code;
        }

        public void AddRoleToUser(Guid userId)
        {
            UserId = userId;
        }
    }
}
