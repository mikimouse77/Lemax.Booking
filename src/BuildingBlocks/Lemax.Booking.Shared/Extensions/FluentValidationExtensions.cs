using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lemax.Booking.Shared.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblies(assemblies);
        }
    }
}