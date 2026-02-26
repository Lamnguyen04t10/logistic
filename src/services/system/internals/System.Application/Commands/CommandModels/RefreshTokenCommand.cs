using MediatR;

namespace System.Application.Commands.CommandModels
{
    public sealed class RefreshTokenCommand : IRequest<LoginCommandResponseModel>
    {
        public Guid UserId { get; init; }
        public string RefreshToken { get; init; }
    }
}
