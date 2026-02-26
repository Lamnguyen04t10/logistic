using System.Application.Commands.CommandModels;
using Jim.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand request,
            CancellationToken cancellationToken
        )
        {
            return await Send(request, cancellationToken);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenCommand command,
            CancellationToken cancellationToken
        )
        {
            var result = await Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            await Send(command);
            return Ok();
        }
    }
}
