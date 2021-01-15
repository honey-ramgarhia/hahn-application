using Hahn.ApplicatonProcess.December2020.Web.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Hahn.ApplicatonProcess.December2020.Web.Utils
{
    public static class InvalidModelStateResponseHandler
    {
        public static IActionResult Handle(ActionContext actionContext)
        {
            var invalidModelStateEntries = actionContext.ModelState
                .Root
                .Children
                .Where(it => it.ValidationState == ModelValidationState.Invalid);

            var errorList = invalidModelStateEntries.SelectMany(it => it.Errors.Select(it => it.ErrorMessage));
            throw new InvalidRequestException(errorList.ToArray());
        }
    }
}
