using Lemax.Booking.Search.API.Extensions;
using Lemax.Booking.Shared.Extensions;
using Lemax.Booking.Shared.Infrastructure.Configurations;

namespace Lemax.Booking.Search.API
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
                .AddElasticSearch(builder.Configuration)
                .AddCqrs(typeof(Program).Assembly)
                .AddEndpointsApiExplorer()
                .AddFluentValidators(typeof(Program).Assembly)
                .AddSwaggerGen();

            var app = builder.Build();

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
    }
}
