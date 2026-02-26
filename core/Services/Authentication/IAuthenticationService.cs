using System.Security.Claims;

namespace Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> GenerateTokenAsync(
            JwtToken tokenModel,
            JwtConfig jwt,
            DateTime? expiredOn = null
        );
        ClaimsPrincipal ValidateToken(string token);
    }
}
