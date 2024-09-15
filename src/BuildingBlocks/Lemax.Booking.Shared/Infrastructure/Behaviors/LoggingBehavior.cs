using System.Diagnostics;
using Lemax.Booking.Shared.Infrastructure.Configurations;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lemax.Booking.Shared.Infrastructure.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull, IRequest<TResponse>
     where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly RequestLoggingOptions _options;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IOptions<RequestLoggingOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("[START] Executing request {RequestName} with data {RequestData}",
                typeof(TRequest).Name, request);

            var timer = Stopwatch.StartNew();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.ElapsedMilliseconds;
            if (timeTaken > _options.TimeThresholdMilliseconds)
            {
                _logger.LogWarning("The request {RequestName} with data {RequestData} took {TimeTaken} milliseconds.",
                    typeof(TRequest).Name, request, timeTaken);
            }

            _logger.LogInformation("[END] Executed request {RequestName}",
                typeof(TRequest).Name);

            return response;
        }
    }
}