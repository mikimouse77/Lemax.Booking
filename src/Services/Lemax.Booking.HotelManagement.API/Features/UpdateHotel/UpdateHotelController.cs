using System;
using System.Threading.Tasks;
using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Lemax.Booking.HotelManagement.API.Features.UpdateHotel
{
    [ApiController]
    [Route("api/hotel")]
    public class UpdateHotelController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public UpdateHotelController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPut("{hotelId}")]
        public async Task<IActionResult> UpdateHotel(Guid hotelId, [FromBody] UpdateHotelRequest request)
        {
            if (hotelId == Guid.Empty)
            {
                return BadRequest("HotelId is required.");
            }
            request.HotelId = hotelId;
            var response = await _dispatcher.Send(request);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
