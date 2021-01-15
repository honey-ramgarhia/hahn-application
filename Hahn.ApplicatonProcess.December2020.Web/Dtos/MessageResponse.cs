namespace Hahn.ApplicatonProcess.December2020.Web.Dtos
{
    public class MessageResponse
    {
        public string Message { get; set; }

        public MessageResponse(string message)
        {
            Message = message;
        }
    }
}
