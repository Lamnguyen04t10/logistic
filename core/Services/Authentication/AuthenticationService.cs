using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services.Authentication
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfig _config;

        public AuthenticationService(IOptions<JwtConfig> config)
        {
            _config = config.Value;
        }

        private JwtConfig GetJwtConfig() => _config;

        public async Task<string> GenerateTokenAsync(
            JwtToken tokenModel,
            JwtConfig jwt,
            DateTime? expiredOn = null
        )
        {
            Claim[] claims = JwtToken.GenerateClaims(tokenModel);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: expiredOn ?? DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtConfig = GetJwtConfig();

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtConfig.Issuer)
                ),
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtConfig.Audience,
                ClockSkew = TimeSpan.Zero,
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParams, out _);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
