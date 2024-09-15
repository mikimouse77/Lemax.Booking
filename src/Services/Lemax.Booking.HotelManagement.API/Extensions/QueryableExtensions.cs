using System.Linq;
using System.Threading.Tasks;
using Lemax.Booking.Shared.Business.Responses;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement.API.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<T> GetPagedData<T, T2>
            (this IQueryable<T2> query, int pageNumber, int pageSize)
            where T : PaginationResponse<T, T2>, new()
        {
            var totalCount = await query.LongCountAsync();
            var data = await query.Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();

            return new T { Data = data, PageNumber = pageNumber, PageSize = pageSize, TotalCount = totalCount };
        }
    }
}
