using FluentValidation;

namespace Lemax.Booking.Search.API.Features.IndexHotel
{
    public class IndexHotelRequestValidator : AbstractValidator<IndexHotelRequest>
    {
        public IndexHotelRequestValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty()
                .WithMessage("Hotel ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Hotel name is required.");

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
