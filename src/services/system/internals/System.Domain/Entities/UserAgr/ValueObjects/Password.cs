using Core.Abstractions;
using Core.Entity;

namespace System.Domain.Entities.UserAgr.ValueObjects
{
    public class Password : ValueObject
    {
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }

        public Password() { }

        public Password(string password)
        {
            string salt = PasswordHasher.GenerateSalt();
            PasswordHash = PasswordHasher.HashPassword(password, salt);
            Salt = salt;
        }

        public Password(string password, string salt)
        {
            Salt = salt;
            PasswordHash = PasswordHasher.HashPassword(password, salt);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PasswordHash;
            yield return Salt;
        }
    }
}
