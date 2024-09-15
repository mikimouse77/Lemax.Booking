using Lemax.Booking.Shared.Business.Requests;

namespace Lemax.Booking.Search.API.Features.SearchHotel
{
    public class SearchHotelRequest : PaginationRequest<SearchHotelResponse>
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        /// <summary>
        /// Radius in meters
        /// </summary>
        public int Radius { get; set; }
    }
}
