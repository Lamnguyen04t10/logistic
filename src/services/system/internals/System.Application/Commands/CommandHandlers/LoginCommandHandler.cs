using System.Application.Commands.CommandModels;
using System.Domain.Abstractions;
using System.Domain.Entities.UserAgr;
using Core;
using Core.Services.Authentication;
using Core.Services.Redis;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace System.Application.Commands.CommandHandlers
{
    public sealed class LoginCommandHandler(
        IUserRepository _userRepository,
        IAuthenticationService _authenticationService,
        IOptions<JwtConfig> _jwtConfig,
        //IRedisService _redisService,
        ILogger<LoginCommand> _logger
    ) : IRequestHandler<LoginCommand, LoginCommandResponseModel>
    {
        public async Task<LoginCommandResponseModel> Handle(
            LoginCommand request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("Start request login");

            User user =
                await _userRepository.GetByUserNameAsync(request.UserName)
                ?? throw new KeyNotFoundException("UserName is not exist");

            if (!user.IsMatchPassword(request.Password))
                throw new KeyNotFoundException("Password is not valid");

            // 3. Tạo jti
            string jti = Guid.NewGuid().ToString("N");

            // 4. Tạo AccessToken với jti
            JwtToken tokenPayload = new(user.Email, user.PhoneNumber.ToString());
            string accessToken = await _authenticationService.GenerateTokenAsync(
                tokenPayload,
                _jwtConfig.Value
            );

            string refreshToken = Guid.NewGuid().ToString("N");

            //await _redisService.SetAccessTokenAsync(
            //    user.Id,
            //    accessToken,
            //    TimeSpan.FromMinutes(_jwtConfig.Value.ExpireMinutes)
            //);
            //await _redisService.SetRefreshTokenAsync(user.Id, refreshToken, TimeSpan.FromDays(7));

            return new LoginCommandResponseModel(accessToken, refreshToken);
        }
    }
}
