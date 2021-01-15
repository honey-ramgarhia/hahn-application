namespace Hahn.ApplicatonProcess.December2020.Web.Utils
{
    public static class WebConstants
    {
        public static class Regexps
        {
            public const string VALID_EMAIL = "^*@*.(com|in|eu|org|net)$";
        }

        public static class ApplicationResources
        {
            public const string APPLICANT = "Applicant";
        }

        public static class ModelProperties
        {
            public const string ID = "ID";
        }

        public static class ResponseMessages
        {
            public const string INVALID_COUNTRY_NAME = "The country name is not valid.";
            public const string UNKNOWN_ERROR = "Unknown error.";
            
            public static string DOES_NOT_EXISTS(string resource)
            {
                return DOES_NOT_EXISTS(resource, null, null);
            }

            public static string DOES_NOT_EXISTS(string resource, string key, string value)
            {
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                {
                    return $"{resource} ({key}: {value}) does not exists.";
                }
                else
                {
                    return $"{resource} does not exists.";
                }
            }
        }
    }
}
