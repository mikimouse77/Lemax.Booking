using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Lemax.Booking.Search.API.Features.IndexHotel
{
    [ApiController]
    [Route("api/index")]
    public class IndexHotelController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public IndexHotelController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("hotel")]
        public async Task<IActionResult> IndexHotel([FromBody] IndexHotelRequest request)
        {
            await _dispatcher.Send(request);

            return Ok("Hotel indexed successfully");
        }
    }
}
