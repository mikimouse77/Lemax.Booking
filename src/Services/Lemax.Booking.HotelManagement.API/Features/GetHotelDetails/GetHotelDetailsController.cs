using System;
using System.Threading.Tasks;
using Lemax.Booking.Shared.Business.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.HttpServices;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelDetails
{
    [ApiController]
    [Route("api/hotel")]
    public class GetHotelDetailsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly SearchApiService _searchApiService;

        public GetHotelDetailsController(IDispatcher dispatcher, SearchApiService searchApiService)
        {
            _dispatcher = dispatcher;
            _searchApiService = searchApiService;
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotelDetails(Guid hotelId)
        {
            var request = new GetHotelDetailsRequest(hotelId);
            var response = await _dispatcher.Send(request);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
