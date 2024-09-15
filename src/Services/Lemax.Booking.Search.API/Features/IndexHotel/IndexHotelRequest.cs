using Lemax.Booking.Shared.Business.Requests;
using MediatR;

namespace Lemax.Booking.Search.API.Features.IndexHotel
{
    public class IndexHotelRequest : BaseRequest<Unit>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
