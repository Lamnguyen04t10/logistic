using System.Application.Commands.CommandModels;
using Core.Services.Redis;
using MediatR;
using Microsoft.Extensions.Logging;

namespace System.Application.Commands.CommandHandlers
{
    public sealed class LogoutCommandHandler(
        //IRedisService _redisService,
        ILogger<LogoutCommandHandler> _logger
    ) : IRequestHandler<LogoutCommand, Unit>
    {
        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Logout user {request.UserId}");

            // Xoá token trong Redis
            //await _redisService.RemoveAccessTokenAsync(request.UserId);
            //await _redisService.RemoveRefreshTokenAsync(request.UserId);

            //// Nếu muốn blacklist jti (tùy chọn bảo mật cao hơn)
            //if (!string.IsNullOrEmpty(request.Jti))
            //{
            //    await _redisService.BlacklistJtiAsync(request.Jti, TimeSpan.FromMinutes(15));
            //}

            return Unit.Value;
        }
    }
}
