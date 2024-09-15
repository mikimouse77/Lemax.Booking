using BookingManagement.API.Infrastructure;
using BookingManagement.API.Models;
using FluentAssertions;
using Lemax.Booking.HotelManagement.API.Features.AddHotel;
using Lemax.Booking.HotelManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Infrastructure.HttpServices;

namespace Lemax.Booking.HotelManagement.API.Tests.Features.AddHotel
{
    public class AddHotelTests
    {
        private readonly GeometryFactoryService _geometryFactoryService;
        private readonly Mock<SearchApiService> _mockSearchApiService;
        private readonly Mock<ILogger<AddHotelHandler>> _mockLogger;
        public AddHotelHandler _handler;
        private readonly BookingContext _mockContext;

        public AddHotelTests()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockContext = new BookingContext(options);

            _geometryFactoryService = new GeometryFactoryService();

            _mockSearchApiService = new Mock<SearchApiService>(Mock.Of<HttpClient>());
            _mockSearchApiService.Setup(x =>
                    x.AddHotelDocument(It.IsAny<Guid>(), It.IsAny<string>(),
                    It.IsAny<decimal>(), It.IsAny<double>(), It.IsAny<double>()))
                .Returns(Task.FromResult(""));

            _mockLogger = new Mock<ILogger<AddHotelHandler>>();

            // Initialize the handler here to avoid repetition in each test
            _handler = new AddHotelHandler(
                _mockContext,
                _mockLogger.Object,
                _geometryFactoryService,
                _mockSearchApiService.Object);
        }

        [Fact]
        public async Task Handle_HotelAlreadyExists_ReturnsFailureResponse()
        {
            // Arrange
            var request = new AddHotelRequest { Name = "Test Hotel", Lon = 10.0, Lat = 20.0 };
            var location = _geometryFactoryService.CreatePoint(request.Lon, request.Lat);
            var existingHotel = new Hotel { Name = "Test Hotel", Location = location };

            _mockContext.Hotels.Add(existingHotel);
            await _mockContext.SaveChangesAsync();

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Hotel with the same name and location is already added.");
        }

        [Fact]
        public async Task Handle_HotelDoesNotExist_AddsHotelAndReturnsSuccessResponse()
        {
            // Arrange
            var request = new AddHotelRequest { Name = "New Hotel", Lon = 10.0, Lat = 20.0, Price = 100.0m };
            var location = _geometryFactoryService.CreatePoint(request.Lon, request.Lat);
            var newHotel = Hotel.New(request.Name, request.Price, location);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
            _mockSearchApiService.Verify(s => s.AddHotelDocument(response.HotelId.Value, newHotel.Name, newHotel.Price, newHotel.Location.Y, newHotel.Location.X), Times.Once);
        }
    }
}
