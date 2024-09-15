using MediatR;

namespace Lemax.Booking.Shared.Business.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ISender _sender;

        public Dispatcher(ISender sender)
        {
            _sender = sender;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return await _sender.Send(request, cancellationToken);
        }

        public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            await _sender.Send(request, cancellationToken);
        }
    }
}
