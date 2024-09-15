using FluentValidation;

namespace Lemax.Booking.HotelManagement.API.Features.GetHotelList
{
    public class GetHotelListRequestValidator : AbstractValidator<GetHotelListRequest>
    {
        public GetHotelListRequestValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0).WithMessage("PageNumber must be greater than 0.");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0.");
        }
    }
}
