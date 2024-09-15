namespace Lemax.Booking.Shared.Business.Requests
{
    public abstract class PaginationRequest<TResponse> : BaseRequest<TResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
