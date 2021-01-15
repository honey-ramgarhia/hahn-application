using Microsoft.AspNetCore.Http;

namespace Hahn.ApplicatonProcess.December2020.Web.Services
{
    public class ApplicationHttpContextService : IApplicationHttpContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApplicationHttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetApplicationBaseUrl()
        {
            var request = httpContextAccessor.HttpContext.Request;
            return $"{request.Scheme}://{request.Host}";
        }
    }
}
