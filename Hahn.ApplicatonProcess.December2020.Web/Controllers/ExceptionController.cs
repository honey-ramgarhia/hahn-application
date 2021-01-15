using Hahn.ApplicatonProcess.December2020.Web.Dtos;
using Hahn.ApplicatonProcess.December2020.Web.Utils;
using static Hahn.ApplicatonProcess.December2020.Web.Utils.WebConstants.ResponseMessages;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hahn.ApplicatonProcess.December2020.Web.Exceptions;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [Route("/exception")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<ApiErrorResponse> HandleException()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionFeature == null)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ApiErrorResponse(UNKNOWN_ERROR);
            }

            var exception = exceptionFeature.Error;
            if (exception is InvalidRequestException)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new ApiErrorResponse((exception as InvalidRequestException).Errors);
            }
            else if (exception is ApiException)
            {
                Response.StatusCode = (exception as ApiException).HttpStatusCode;
            }
            else
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return new ApiErrorResponse(exception.Message);
        }
    }
}
