using System.Reflection;
using Lemax.Booking.Shared.Business.Dispatchers;
using Lemax.Booking.Shared.Infrastructure.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Lemax.Booking.Shared.Extensions
{
    public static class CqrsExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(assemblies));
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }
    }
}
