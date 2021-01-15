using Hahn.ApplicatonProcess.December2020.Web.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers.Applicants
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IMediator mediator;

        public ApplicantController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Add(Add.Request request)
        {
            return StatusCode(StatusCodes.Status201Created, await mediator.Send(request));
        }

        [HttpGet("{ApplicantId}")]
        public async Task<ActionResult<ApiResponse>> Get([FromRoute]Get.Request request)
        {
            return await mediator.Send(request);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Update(Update.Request request)
        {
            return await mediator.Send(request);
        }

        [HttpDelete("{ApplicantId}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute]Delete.Request request)
        {
            return await mediator.Send(request);
        }
    }
}
