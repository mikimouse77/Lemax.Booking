using System;
using Lemax.Booking.Shared.Business.Requests;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelDetails
{
    public class GetHotelDetailsRequest : BaseRequest<GetHotelDetailsResponse>
    {
        public Guid HotelId { get; set; }

        public GetHotelDetailsRequest(Guid hotelId)
        {
            HotelId = hotelId;
        }
    }
}
