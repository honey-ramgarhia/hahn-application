namespace Hahn.ApplicatonProcess.December2020.Web.Dtos
{
    public class ApiResponse
    {
        public ResponseMetadata Meta { get; set; }
        public object Data { get; set; }

        public ApiResponse(object data) : this(data, null)
        { }

        public ApiResponse(object data, string accessUrl)
        {
            Data = data;
            Meta = new ResponseMetadata { AccessUrl = accessUrl };
        }

        public class ResponseMetadata
        {
            public string AccessUrl { get; set; }
        }
    }
}
