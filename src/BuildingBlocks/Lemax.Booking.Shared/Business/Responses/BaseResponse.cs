namespace Lemax.Booking.Shared.Business.Responses
{
    public abstract class BaseResponse<TResponse> where TResponse : BaseResponse<TResponse>, new()
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? ErrorCode { get; set; }

        public BaseResponse(bool success = true, string message = null, int? errorCode = null)
        {
            Success = success;
            Message = message;
            ErrorCode = errorCode;
        }

        public static TResponse Failure(string message, int errorCode) =>
             new TResponse() { Success = false, Message = message, ErrorCode = errorCode };
    }
}
