using Lemax.Booking.Search.API.Features.SearchHotel;
using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Lemax.Booking.Search.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public SearchController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("hotels")]
        public async Task<IActionResult> SearchHotels([FromQuery] SearchHotelRequest request)
        {
            var hotels = await _dispatcher.Send(request);
            return Ok(hotels);
        }
    }
}
