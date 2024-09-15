using System.Threading;
using System.Threading.Tasks;
using BookingManagement.API.Infrastructure;
using Lemax.Booking.HotelManagement.API.Services;
using Lemax.Booking.Shared.Business.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lemax.Booking.HotelManagement.API.Features.UpdateHotel
{
    public class UpdateHotelHandler : BaseRequestHandler<UpdateHotelRequest, UpdateHotelResponse>
    {
        private readonly BookingContext _context;
        private readonly GeometryFactoryService _geometryFactoryService;

        public UpdateHotelHandler(BookingContext context, ILogger<UpdateHotelHandler> logger,
            GeometryFactoryService geometryFactoryService) : base(logger)
        {
            _context = context;
            _geometryFactoryService = geometryFactoryService;
        }

        public override async Task<UpdateHotelResponse> Handle(UpdateHotelRequest request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == request.HotelId, cancellationToken);

            if (hotel == null)
            {
                Logger.LogWarning("Hotel with ID {HotelId} not found.", request.HotelId);
                return UpdateHotelResponse.Failure("Hotel not found.", 1002);
            }

            // Check if updated name and location exist -> return error

            var location = _geometryFactoryService.CreatePoint(request.Lon, request.Lat);

            hotel
                .WithName(request.Name)
                .WithPrice(request.Price)
                .WithLocation(location);

            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateHotelResponse(hotel.Id, hotel.Name, hotel.Price, hotel.Location.Y, hotel.Location.X);
        }
    }
}
