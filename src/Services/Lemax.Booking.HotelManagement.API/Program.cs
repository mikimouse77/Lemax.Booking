using System;
using BookingManagement.API.Infrastructure;
using BookingManagement.API.Infrastructure.Extensions;
using Lemax.Booking.Shared.Extensions;
using Lemax.Booking.Shared.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lemax.Booking.HotelManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });

            builder.Services.Configure<RequestLoggingOptions>(builder.Configuration.GetSection("RequestLogging"));

            builder.Services
                .AddSearchApiService(builder.Configuration)
                .AddCqrs(typeof(HotelManagementAssembly).Assembly)
                .AddBookingContext(builder.Configuration)
                .ConfigureGeometry()
                .AddEndpointsApiExplorer()
                .AddFluentValidators(typeof(HotelManagementAssembly).Assembly)
                .AddSwaggerGen();

            var app = builder.Build();

            EnsureDatabaseCreatedAndMigrated(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.UseCustomExceptionHandler();

            app.Run();
        }

        private static void EnsureDatabaseCreatedAndMigrated(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<BookingContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or initializing the database.");
            }
        }
    }
}
