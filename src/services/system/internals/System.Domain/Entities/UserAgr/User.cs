using System.Domain.Entities.AddresAgr;
using System.Domain.Entities.ComplexTypes;
using System.Domain.Entities.UserAgr.ValueObjects;
using Core.Abstractions;
using Core.Entity;

namespace System.Domain.Entities.UserAgr
{
    public partial class User : BaseEntity, IAggregateRoot
    {
        public string UserName { get; private set; }
        public Password Password { get; private set; }
        public string Email { get; private set; }
        public virtual ICollection<Address> Addresses { get; private set; }

        public PhoneNumber PhoneNumber { get; set; }

        public User() { }
    }
}
