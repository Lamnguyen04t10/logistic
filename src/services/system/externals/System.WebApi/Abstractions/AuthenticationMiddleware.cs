using System.Security.Claims;
using Core.Services.Authentication;
using Core.Services.Redis;

namespace System.WebApi.Abstractions
{
    public class RedisAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public RedisAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context,
            IAuthenticationService tokenService
            //IRedisService redisService
        )
        {
            string? token = context
                .Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();

            if (string.IsNullOrWhiteSpace(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Missing access token");
                return;
            }

            // ✅ Validate JWT
            ClaimsPrincipal? principal;
            try
            {
                principal = tokenService.ValidateToken(token);
            }
            catch
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            if (principal == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            // ✅ Extract jti & userId
            string? jti = principal.FindFirst("jti")?.Value;
            string? userIdStr = principal.FindFirst("sub")?.Value;

            if (
                string.IsNullOrEmpty(jti)
                || string.IsNullOrEmpty(userIdStr)
                || !Guid.TryParse(userIdStr, out var userId)
            )
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token claims");
                return;
            }

            //bool isRevoked = await redisService.IsJtiBlacklistedAsync(jti);
            //if (isRevoked)
            //{
            //    context.Response.StatusCode = 401;
            //    await context.Response.WriteAsync("Token has been revoked");
            //    return;
            //}

            //string? tokenInRedis = await redisService.GetAccessTokenAsync(userId);
            //if (tokenInRedis != token)
            //{
            //    context.Response.StatusCode = 401;
            //    await context.Response.WriteAsync("Token mismatch or expired");
            //    return;
            //}

            context.User = principal;

            await _next(context);
        }
    }
}
