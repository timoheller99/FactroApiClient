namespace FactroApiClient.SharedContracts
{
    using System;
    using System.Net;

    public class FactroApiException : Exception
    {
        public FactroApiException()
        {
        }

        public FactroApiException(string message)
            : base(message)
        {
        }

        public FactroApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public FactroApiException(string message, string requestRoute, HttpStatusCode statusCode, string responseContent)
            : base(message)
        {
            this.RequestRoute = requestRoute;
            this.StatusCode = statusCode;
            this.ResponseContent = responseContent;
        }

        public string RequestRoute { get; }

        public HttpStatusCode StatusCode { get; }

        public string ResponseContent { get; }
    }
}
