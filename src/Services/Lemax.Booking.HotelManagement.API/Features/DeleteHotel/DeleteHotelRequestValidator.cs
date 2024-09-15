using FluentValidation;

namespace Lemax.Booking.HotelManagement.API.Features.DeleteHotel
{
    public class DeleteHotelRequestValidator : AbstractValidator<DeleteHotelRequest>
    {
        public DeleteHotelRequestValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty().WithMessage("HotelId is required.");
        }
    }
}
