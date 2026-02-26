using Jim.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace System.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Get() => Ok("D<MMMMMMMMMMMMM");
    }
}
