using FluentValidation;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelDetails
{
    public class GetHotelDetailsRequestValidator : AbstractValidator<GetHotelDetailsRequest>
    {
        public GetHotelDetailsRequestValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty().WithMessage("HotelId is required.");
        }
    }
}
