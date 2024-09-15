using Lemax.Booking.Search.API.Infrastructure.Configurations;
using Lemax.Booking.Shared.Business.Handlers;

namespace Lemax.Booking.Search.API.Features.SearchHotel
{
    public class SearchHotelHandler : BaseRequestHandler<SearchHotelRequest, SearchHotelResponse>
    {
        private readonly IElasticSearchService _elasticSearchService;

        public SearchHotelHandler(IElasticSearchService elasticSearchService,
            ILogger<SearchHotelHandler> logger)
            : base(logger)
        {
            _elasticSearchService = elasticSearchService;
        }

        public override async Task<SearchHotelResponse> Handle(SearchHotelRequest request, CancellationToken cancellationToken)
        {
            return await _elasticSearchService.SearchHotelsAsync<SearchHotelResponse>(
                request.Lat,
                request.Lon,
                request.Radius,
                request.PageNumber,
                request.PageSize);
        }
    }
}
