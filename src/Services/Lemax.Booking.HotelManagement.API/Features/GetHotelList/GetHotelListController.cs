using System.Threading.Tasks;
using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelList
{
    [ApiController]
    [Route("api/hotel")]
    public class GetHotelListController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public GetHotelListController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotelList([FromQuery] GetHotelListRequest query)
        {
            var response = await _dispatcher.Send(query);
            return Ok(response);
        }
    }
}
