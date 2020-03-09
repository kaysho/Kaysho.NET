namespace Kaysho.NET.Core.Contracts.V1.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public bool Error { get; set; }

        public Response() { }

        public Response(T response, string message, bool error)
        {
            Data = response;
            Message = message;
            Error = error;
        }

    }
}
