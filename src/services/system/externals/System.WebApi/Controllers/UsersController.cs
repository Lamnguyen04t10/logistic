using System.Application.Queries.QueryModels;
using Jim.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUserPaginationAsync(
            [FromQuery] GetUserPaginationQuery query,
            CancellationToken cancellationToken
        )
        {
            return Ok(await Sender.Send(query, cancellationToken));
        }
    }
}
