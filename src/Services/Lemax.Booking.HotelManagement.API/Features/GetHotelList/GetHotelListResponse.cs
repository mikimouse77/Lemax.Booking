using System.Collections.Generic;
using Lemax.Booking.Shared.Business.Responses;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelList
{
    public class GetHotelListResponse : PaginationResponse<GetHotelListResponse, HotelDto>
    {
        public GetHotelListResponse()
        {
        }

        public GetHotelListResponse(IEnumerable<HotelDto> data, int pageNumber, int pageSize, long totalCount)
            : base(data, pageNumber, pageSize, totalCount)
        {
        }
    }
}
