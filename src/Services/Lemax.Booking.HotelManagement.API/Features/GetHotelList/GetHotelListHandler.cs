using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingManagement.API.Infrastructure;
using BookingManagement.API.Infrastructure.Extensions;
using Lemax.Booking.Shared.Business.Handlers;
using Microsoft.Extensions.Logging;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelList
{
    public class GetHotelListHandler : BaseRequestHandler<GetHotelListRequest, GetHotelListResponse>
    {
        private readonly BookingContext _context;

        public GetHotelListHandler(BookingContext context, ILogger<GetHotelListHandler> logger)
            : base(logger)
        {
            _context = context;
        }

        public override async Task<GetHotelListResponse> Handle(GetHotelListRequest request, CancellationToken cancellationToken)
        {
            var query = _context.Hotels.Select(h => new HotelDto
            {
                HotelId = h.Id,
                Name = h.Name,
                Price = h.Price,
                Latitude = h.Location.Y,
                Longitude = h.Location.X
            });

            return await query.GetPagedData<GetHotelListResponse, HotelDto>(request.PageNumber, request.PageSize);
        }
    }
}
