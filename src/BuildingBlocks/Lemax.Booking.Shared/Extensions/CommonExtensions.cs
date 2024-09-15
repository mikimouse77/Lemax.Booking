using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.HttpServices;

namespace Lemax.Booking.Shared.Extensions
{
    public static class CommonExtensions
    {
        public static IServiceCollection AddSearchApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<SearchApiService>(client =>
            {
                client.BaseAddress = new Uri(configuration["SearchApi:BaseUrl"]);
            });

            return services;
        }
    }
}
