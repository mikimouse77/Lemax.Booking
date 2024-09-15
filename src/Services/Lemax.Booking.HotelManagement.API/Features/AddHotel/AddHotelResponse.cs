using System;
using Lemax.Booking.Shared.Business.Responses;

namespace Lemax.Booking.HotelManagement.API.Features.AddHotel
{
    public class AddHotelResponse : BaseResponse<AddHotelResponse>
    {
        public AddHotelResponse()
        {
        }

        public AddHotelResponse(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid? HotelId { get; }
    }
}
