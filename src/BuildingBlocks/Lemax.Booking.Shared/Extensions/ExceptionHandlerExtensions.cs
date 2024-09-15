using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lemax.Booking.Shared.Extensions
{
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");

                    if (exceptionHandlerPathFeature?.Error != null)
                    {
                        logger.LogError(exceptionHandlerPathFeature.Error, "An unhandled exception occurred.");

                        context.Response.StatusCode = exceptionHandlerPathFeature.Error switch
                        {
                            ValidationException validationException =>
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest,
                            _ => context.Response.StatusCode = (int)HttpStatusCode.InternalServerError
                        };
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }

                    context.Response.ContentType = "application/json";

                    var result = new
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "An unexpected error occurred. Please try again later."
                    };

                    var jsonResponse = JsonSerializer.Serialize(result);
                    await context.Response.WriteAsync(jsonResponse);
                });
            });
        }
    }
}
