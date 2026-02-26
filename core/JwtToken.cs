using System.Security.Claims;
using Core.Enumration;

namespace Core
{
    public class JwtToken(string email, string phoneNumber)
    {
        public string Email { get; private set; } = email;
        public string PhoneNumber { get; private set; } = phoneNumber;
        public string Jti { get; set; } = Guid.NewGuid().ToString();

        public static Claim[] GenerateClaims(JwtToken model)
        {
            return
            [
                new Claim(TokenValue.Email.Name, model.Email),
                new Claim(TokenValue.PhoneNumber.Name, model.PhoneNumber ?? string.Empty),
            ];
        }
    }
}
