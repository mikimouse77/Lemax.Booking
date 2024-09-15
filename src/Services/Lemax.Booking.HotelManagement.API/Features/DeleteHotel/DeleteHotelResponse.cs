using System;
using Lemax.Booking.Shared.Business.Responses;

namespace Lemax.Booking.HotelManagement.API.Features.DeleteHotel
{
    public class DeleteHotelResponse : BaseResponse<DeleteHotelResponse>
    {
        public DeleteHotelResponse()
        {
        }

        public DeleteHotelResponse(Guid hotelId)
        {
            HotelId = hotelId;
        }

        public Guid HotelId { get; set; }
    }
}
