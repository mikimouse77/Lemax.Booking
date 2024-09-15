using System.Text;
using System.Text.Json;

namespace Shared.Infrastructure.HttpServices
{
    public class SearchApiService
    {
        private readonly HttpClient _httpClient;
        protected Func<string, StringContent> Content { get; } = payload => new StringContent(content: payload, encoding: Encoding.UTF8, mediaType: "application/json");

        public SearchApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<string> AddHotelDocument(Guid hotelId, string name, decimal price, double lat, double lon)
        {
            try
            {
                var payload = JsonSerializer.Serialize(new { hotelId, name, price, lat, lon });
                var response = await _httpClient.PostAsync("/api/index/hotel", Content(payload));
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error pinging Search API", ex);
            }
        }
    }
}
