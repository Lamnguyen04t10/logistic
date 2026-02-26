using System.Application.Queries.QueryModels;
using Jim.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync(
            [FromQuery] GetTenantPaginationQuery request,
            CancellationToken cancellationToken = default
        )
        {
            return Ok(await Sender.Send(request, cancellationToken));
        }
    }
}
