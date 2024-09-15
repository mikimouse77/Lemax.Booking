using System;
using Lemax.Booking.Shared.Business.Responses;

namespace Lemax.Booking.HotelManagement.API.Features.UpdateHotel
{
    public class UpdateHotelResponse : BaseResponse<UpdateHotelResponse>
    {
        public UpdateHotelResponse()
        {
        }

        public UpdateHotelResponse(Guid hotelId, string name, decimal price, double lat, double lon)
        {
            HotelId = hotelId;
            Name = name;
            Price = price;
            Lat = lat;
            Lon = lon;
        }

        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
