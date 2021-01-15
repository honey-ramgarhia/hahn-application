using System;

namespace Hahn.ApplicatonProcess.December2020.Web.Utils
{
    public class ApiException : Exception
    {
        public int HttpStatusCode { get; set; }

        public ApiException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
