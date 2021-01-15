namespace Hahn.ApplicatonProcess.December2020.Web.Dtos
{
    public class ApiErrorResponse
    {
        public string[] Errors { get; set; }

        public ApiErrorResponse(string[] errors)
        {
            Errors = errors;
        }

        public ApiErrorResponse(string error)
        {
            Errors = new string[] { error };
        }
    }
}
