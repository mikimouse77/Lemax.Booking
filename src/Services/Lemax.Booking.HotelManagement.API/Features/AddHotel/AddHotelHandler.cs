using System.Threading;
using System.Threading.Tasks;
using BookingManagement.API.Infrastructure;
using BookingManagement.API.Models;
using Lemax.Booking.HotelManagement.API.Services;
using Lemax.Booking.Shared.Business.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Shared.Infrastructure.HttpServices;

namespace Lemax.Booking.HotelManagement.API.Features.AddHotel
{
    public class AddHotelHandler : BaseRequestHandler<AddHotelRequest, AddHotelResponse>
    {
        private readonly BookingContext _context;
        private readonly GeometryFactoryService _geometryFactoryService;
        private readonly SearchApiService _searchApiService;

        public AddHotelHandler(BookingContext context, ILogger<AddHotelHandler> logger,
            GeometryFactoryService geometryFactoryService,
            SearchApiService searchApiService) : base(logger)
        {
            _context = context;
            _geometryFactoryService = geometryFactoryService;
            _searchApiService = searchApiService;
        }

        public override async Task<AddHotelResponse> Handle(AddHotelRequest request, CancellationToken cancellationToken)
        {
            var location = _geometryFactoryService.CreatePoint(request.Lon, request.Lat);

            var hotelExists = await _context.Hotels
                .AnyAsync(h => h.Name == request.Name && h.Location.Contains(location), cancellationToken: cancellationToken);

            if (hotelExists)
            {
                return AddHotelResponse.Failure("Hotel with the same name and location is already added.", 1001);
            }

            var hotel = await AddNewHotel(request, location, cancellationToken);

            Logger.LogInformation("Hotel {HotelName} added successfully.", hotel.Name);

            await _searchApiService.AddHotelDocument(
                hotel.Id,
                hotel.Name,
                hotel.Price,
                hotel.Location.Y,
                hotel.Location.X);

            return new AddHotelResponse(hotel.Id);
        }

        private async Task<Hotel> AddNewHotel(AddHotelRequest request, Point location, CancellationToken cancellationToken)
        {
            var hotel = Hotel.New(request.Name, request.Price, location);

            await _context.Hotels.AddAsync(hotel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return hotel;
        }
    }
}
