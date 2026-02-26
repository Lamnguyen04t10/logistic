using MediatR;

namespace System.Application.Commands.CommandModels
{
    public sealed class LogoutCommand : IRequest<Unit>
    {
        public Guid UserId { get; init; }
        public string Jti { get; init; }
    }
}
