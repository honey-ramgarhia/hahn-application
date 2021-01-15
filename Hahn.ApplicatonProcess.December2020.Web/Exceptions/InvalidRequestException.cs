using System;

namespace Hahn.ApplicatonProcess.December2020.Web.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public string[] Errors { get; set; }

        public InvalidRequestException(string[] errors)
        {
            Errors = errors;
        }
    }
}
