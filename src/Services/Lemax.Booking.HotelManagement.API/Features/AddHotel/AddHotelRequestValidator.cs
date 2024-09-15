using FluentValidation;

namespace Lemax.Booking.HotelManagement.API.Features.AddHotel
{
    public class AddHotelRequestValidator : AbstractValidator<AddHotelRequest>
    {
        public AddHotelRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Hotel name is required.")
                .MinimumLength(2)
                .WithMessage("Hotel name must be at least 2 characters long.")
                .MaximumLength(100)
                .WithMessage("Hotel name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Lat)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Lon)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180.");
        }
    }
}
