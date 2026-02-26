using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jim.WebApi.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender? _sender;
        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>()!;

        public async Task<IActionResult> Send<T>(T request, CancellationToken cancellationToken = default)
        {
            if (request is null)
            {
                return BadRequest("Request cannot be null.");
            }

            var result = await Sender.Send(request, cancellationToken);
            return result is not null ? Ok(result) : NotFound();
        }
    }
}
