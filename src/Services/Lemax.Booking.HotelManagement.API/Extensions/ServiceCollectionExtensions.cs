using Lemax.Booking.HotelManagement.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingManagement.API.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBookingContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(nameof(BookingContext)),
                x => x.UseNetTopologySuite()));

            return services;
        }

        public static IServiceCollection ConfigureGeometry(this IServiceCollection services)
        {
            services.AddSingleton<GeometryFactoryService>();

            return services;
        }
    }
}
