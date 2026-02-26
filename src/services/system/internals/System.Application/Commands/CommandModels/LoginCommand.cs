using MediatR;

namespace System.Application.Commands.CommandModels
{
    public sealed record LoginCommand(string UserName, string Password)
        : IRequest<LoginCommandResponseModel>;

    public sealed record LoginCommandResponseModel(string Token, string RefreshToken);
}
