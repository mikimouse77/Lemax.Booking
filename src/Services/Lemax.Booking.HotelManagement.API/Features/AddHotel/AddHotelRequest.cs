using Lemax.Booking.Shared.Business.Requests;

namespace Lemax.Booking.HotelManagement.API.Features.AddHotel
{
    public class AddHotelRequest : BaseRequest<AddHotelResponse>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
