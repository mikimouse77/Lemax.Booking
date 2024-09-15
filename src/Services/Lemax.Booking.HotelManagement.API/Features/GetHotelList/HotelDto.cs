using System;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelList
{
    public class HotelDto
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
