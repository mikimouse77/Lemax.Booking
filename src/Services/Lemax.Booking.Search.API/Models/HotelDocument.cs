using Nest;

namespace Lemax.Booking.Search.API.Models
{
    public class HotelDocument
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [GeoPoint]
        public GeoLocation Location { get; set; }
    }
}
