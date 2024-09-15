using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lemax.Booking.Shared.Business.Requests
{
    public abstract class BaseRequest<TResponse> : IRequest<TResponse>
    {
        [JsonIgnore]
        [BindNever]
        public Guid RequestedBy { get; set; }
    }
}
