using System.Threading;
using System.Threading.Tasks;
using BookingManagement.API.Infrastructure;
using Lemax.Booking.Shared.Business.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelDetails
{
    public class GetHotelDetailsHandler : BaseRequestHandler<GetHotelDetailsRequest, GetHotelDetailsResponse>
    {
        private readonly BookingContext _context;

        public GetHotelDetailsHandler(BookingContext context, ILogger<GetHotelDetailsHandler> logger)
            : base(logger)
        {
            _context = context;
        }

        public override async Task<GetHotelDetailsResponse> Handle(GetHotelDetailsRequest request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == request.HotelId, cancellationToken);

            if (hotel == null)
            {
                Logger.LogWarning("Hotel with ID {HotelId} not found.", request.HotelId);
                return GetHotelDetailsResponse.Failure("Hotel not found.", 1002);
            }

            return new GetHotelDetailsResponse(hotel.Id, hotel.Name, hotel.Price, hotel.Location.Y, hotel.Location.X);
        }
    }
}
