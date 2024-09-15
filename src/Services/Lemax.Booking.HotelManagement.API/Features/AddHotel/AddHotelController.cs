using System.Threading.Tasks;
using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Lemax.Booking.HotelManagement.API.Features.AddHotel
{
    [ApiController]
    [Route("api/hotel")]
    public class AddHotelController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public AddHotelController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelRequest request)
        {
            var response = await _dispatcher.Send(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Created($"/api/hotel/{response.HotelId}", response);
        }
    }
}
