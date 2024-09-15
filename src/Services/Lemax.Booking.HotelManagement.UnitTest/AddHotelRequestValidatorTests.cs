using FluentValidation.TestHelper;
using Lemax.Booking.HotelManagement.API.Features.AddHotel;

namespace Lemax.Booking.HotelManagement.API.Tests.Features.AddHotel
{
    public class AddHotelRequestValidatorTests
    {
        private readonly AddHotelRequestValidator _validator;

        public AddHotelRequestValidatorTests()
        {
            _validator = new AddHotelRequestValidator();
        }

        [Fact]
        public void ShouldHaveValidationError_WhenNameIsEmpty()
        {
            var request = new AddHotelRequest
            {
                Name = string.Empty,
                Price = 100,
                Lat = 45.678,
                Lon = -123.456
            };

            var result = _validator.TestValidate(request);

            result
                .ShouldHaveValidationErrorFor(x => x.Name)
                .WithErrorMessage("Hotel name is required.");
        }

        [Fact]
        public void ShouldHaveValidationError_WhenPriceIsZero()
        {
            var request = new AddHotelRequest
            {
                Name = "Hotel ABC",
                Price = 0,
                Lat = 45.678,
                Lon = -123.456
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Price)
                .WithErrorMessage("Price must be greater than zero.");
        }

        [Fact]
        public void ShouldHaveValidationError_WhenLatitudeIsLessThanMinusNinety()
        {
            var request = new AddHotelRequest
            {
                Name = "Hotel ABC",
                Price = 100,
                Lat = -91,
                Lon = -123.456
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Lat)
                .WithErrorMessage("Latitude must be between -90 and 90.");
        }

        [Fact]
        public void ShouldHaveValidationError_WhenLongitudeIsGreaterThanOneEighty()
        {
            var request = new AddHotelRequest
            {
                Name = "Hotel ABC",
                Price = 100,
                Lat = 45.678,
                Lon = 181
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.Lon)
                .WithErrorMessage("Longitude must be between -180 and 180.");
        }

        [Fact]
        public void ShouldNotHaveValidationError_WhenAllPropertiesAreValid()
        {
            var request = new AddHotelRequest
            {
                Name = "Hotel ABC",
                Price = 100,
                Lat = 45.678,
                Lon = -123.456
            };

            var result = _validator.TestValidate(request);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
