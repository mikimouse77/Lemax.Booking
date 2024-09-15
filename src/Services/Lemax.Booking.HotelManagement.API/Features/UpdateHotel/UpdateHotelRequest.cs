using System;
using System.Text.Json.Serialization;
using Lemax.Booking.Shared.Business.Requests;

namespace Lemax.Booking.HotelManagement.API.Features.UpdateHotel
{
    public class UpdateHotelRequest : BaseRequest<UpdateHotelResponse>
    {
        [JsonIgnore]
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
