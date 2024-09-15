using MediatR;

namespace Lemax.Booking.Shared.Business.Dispatchers
{
    public interface IDispatcher
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest;
    }
}
