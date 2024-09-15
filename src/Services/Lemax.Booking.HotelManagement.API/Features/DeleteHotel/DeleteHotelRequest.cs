using System;
using Lemax.Booking.Shared.Business.Requests;

namespace Lemax.Booking.HotelManagement.API.Features.DeleteHotel
{
    public class DeleteHotelRequest : BaseRequest<DeleteHotelResponse>
    {
        public Guid HotelId { get; set; }

        public DeleteHotelRequest(Guid hotelId)
        {
            HotelId = hotelId;
        }
    }
}
