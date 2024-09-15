using System;
using System.Threading.Tasks;
using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Lemax.Booking.HotelManagement.API.Features.DeleteHotel
{
    [ApiController]
    [Route("api/hotel")]
    public class DeleteHotelController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public DeleteHotelController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpDelete("{hotelId}")]
        public async Task<IActionResult> DeleteHotel(Guid hotelId)
        {
            var request = new DeleteHotelRequest(hotelId);
            var response = await _dispatcher.Send(request);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
