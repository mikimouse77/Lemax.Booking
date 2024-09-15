using System.Threading;
using System.Threading.Tasks;
using BookingManagement.API.Infrastructure;
using Lemax.Booking.Shared.Business.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lemax.Booking.HotelManagement.API.Features.DeleteHotel
{
    public class DeleteHotelHandler : BaseRequestHandler<DeleteHotelRequest, DeleteHotelResponse>
    {
        private readonly BookingContext _context;

        public DeleteHotelHandler(BookingContext context, ILogger<DeleteHotelHandler> logger)
            : base(logger)
        {
            _context = context;
        }

        public override async Task<DeleteHotelResponse> Handle(DeleteHotelRequest request, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == request.HotelId, cancellationToken);

            if (hotel == null)
            {
                return new DeleteHotelResponse(hotel.Id);
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteHotelResponse(hotel.Id);
        }
    }
}
