using System.Application.Commands.CommandModels;
using System.Domain.Abstractions;
using Core;
using Core.Services.Authentication;
using Core.Services.Redis;
using MediatR;
using Microsoft.Extensions.Options;

namespace System.Application.Commands.CommandHandlers
{
    public class RefreshTokenCommandHandler(
        //IRedisService _redisService,
        IAuthenticationService _authenticationService,
        IUserRepository _userRepository,
        IOptions<JwtConfig> _jwtConfig
    ) : IRequestHandler<RefreshTokenCommand, LoginCommandResponseModel>
    {
        public async Task<LoginCommandResponseModel> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken
        )
        {
            //var storedToken = await _redisService.GetRefreshTokenAsync(request.UserId);
            //if (storedToken is null || storedToken != request.RefreshToken)
            //    throw new UnauthorizedAccessException("Invalid refresh token");

            var user =
                await _userRepository.GetByIdAsync(request.UserId)
                ?? throw new KeyNotFoundException("User not found");

            var jwt = new JwtToken(user.Email, user.PhoneNumber.ToString());
            string accessToken = await _authenticationService.GenerateTokenAsync(
                jwt,
                _jwtConfig.Value
            );

            //await _redisService.SetAccessTokenAsync(
            //    user.Id,
            //    accessToken,
            //    TimeSpan.FromMinutes(_jwtConfig.Value.ExpireMinutes)
            //);

            return new LoginCommandResponseModel(accessToken, request.RefreshToken);
        }
    }
}
