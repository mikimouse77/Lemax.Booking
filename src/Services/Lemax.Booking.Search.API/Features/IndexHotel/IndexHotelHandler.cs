using Lemax.Booking.Search.API.Infrastructure.Configurations;
using Lemax.Booking.Search.API.Models;
using Lemax.Booking.Shared.Business.Handlers;
using MediatR;
using Nest;

namespace Lemax.Booking.Search.API.Features.IndexHotel
{
    public class IndexHotelHandler : BaseRequestHandler<IndexHotelRequest, Unit>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public IndexHotelHandler(IElasticSearchService elasticSearchService,
            ILogger<IndexHotelHandler> logger)
            : base(logger)
        {
            _elasticSearchService = elasticSearchService;
        }

        public override async Task<Unit> Handle(IndexHotelRequest request, CancellationToken cancellationToken)
        {
            var hotelDocument = new HotelDocument
            {
                Id = request.HotelId,
                Name = request.Name,
                Price = request.Price,
                Location = new GeoLocation(request.Lat, request.Lon)
            };

            await _elasticSearchService.IndexHotelAsync(hotelDocument);

            Logger.LogInformation("Hotel with ID {HotelId} indexed successfully.", request.HotelId);

            return Unit.Value;
        }
    }
}
