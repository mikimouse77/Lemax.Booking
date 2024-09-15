using System;
using Lemax.Booking.Shared.Business.Responses;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelDetails
{
    public class GetHotelDetailsResponse : BaseResponse<GetHotelDetailsResponse>
    {
        public GetHotelDetailsResponse()
        {
        }

        public GetHotelDetailsResponse(Guid hotelId, string name, decimal price, double latitude, double longitude)
        {
            HotelId = hotelId;
            Name = name;
            Price = price;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
