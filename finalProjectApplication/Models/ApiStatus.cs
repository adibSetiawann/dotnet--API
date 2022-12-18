using System.Net;
using Newtonsoft.Json;

namespace FinalProjectApplication
{
    public class ApiStatus
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiStatus(int statusCode)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)statusCode;
            this.StatusCode = statusCode;
            this.StatusDescription = parsedCode.ToString();
        }

        public ApiStatus(int statusCode, string statusDescription)
        {
            this.StatusCode = statusCode;
            this.StatusDescription = statusDescription;
        }

        public ApiStatus(int statusCode, string statusDescription, string message)
            :  this(statusCode, statusDescription)
        {
            this.Message = message;
        }
    }
}