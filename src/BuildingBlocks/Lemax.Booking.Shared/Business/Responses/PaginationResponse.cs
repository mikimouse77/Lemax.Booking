namespace Lemax.Booking.Shared.Business.Responses
{
    public abstract class PaginationResponse<TResponse, TData> : BaseResponse<TResponse>
        where TResponse : PaginationResponse<TResponse, TData>, new()
    {
        public PaginationResponse()
        {
        }

        public PaginationResponse(IEnumerable<TData> data, int pageNumber, int pageSize, long totalCount)
            : base(true)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IEnumerable<TData> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
