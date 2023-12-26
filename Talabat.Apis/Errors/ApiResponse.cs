namespace Talabat.Apis.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you are made",
                401 => "Authorized, you are not",
                404 => "Resourse was not found",
                500 => "Server error, errors are the path to the dark side",
                _ => null
            };
        }
    }
}
