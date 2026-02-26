using System.Domain.Entities.AddresAgr;
using System.Domain.Entities.ComplexTypes;
using System.Domain.Entities.UserAgr.ValueObjects;

namespace System.Domain.Entities.UserAgr
{
    public partial class User
    {
        public User(string userName, string email, Password password, PhoneNumber phoneNumber)
        {
            base.SetId();
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = userName;
        }

        public void SetPhoneNumber(string code, string phone)
        {
            PhoneNumber = new PhoneNumber(code, phone);
        }

        public void SetPhoneNumber(PhoneNumber phone)
        {
            PhoneNumber = phone;
        }

        public void SetAddress(IEnumerable<Address> addresses)
        {
            Addresses = addresses.ToList();
        }

        public void ChangePassword(string password)
        {
            Password = new Password(password);
        }

        public bool IsMatchPassword(string password)
        {
            Password pass = new Password(password, this.Password.Salt);
            return Password.Equals(pass);
        }
    }
}
