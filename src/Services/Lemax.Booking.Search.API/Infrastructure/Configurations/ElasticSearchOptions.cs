using System.Collections.Concurrent;
using Lemax.Booking.Search.API.Models;
using Lemax.Booking.Shared.Business.Responses;
using Microsoft.Extensions.Options;
using Nest;

namespace Lemax.Booking.Search.API.Infrastructure.Configurations
{
    public class ElasticSearchOptions
    {
        public string Url { get; set; }
        public string DefaultIndex { get; set; }
    }

    public static class ElasticSearchIndexCache
    {
        private static readonly ConcurrentDictionary<string, bool> _indexCache = new ConcurrentDictionary<string, bool>();

        public static bool IndexExists(string indexName)
        {
            return _indexCache.ContainsKey(indexName);
        }

        public static void AddIndex(string indexName)
        {
            _indexCache.TryAdd(indexName, true);
        }
    }

    public interface IElasticSearchService
    {
        Task IndexHotelAsync(HotelDocument hotelDocument);
        Task<TResponse> SearchHotelsAsync<TResponse>(double lat, double lon, int radius,
            int page, int pageSize) where TResponse : PaginationResponse<TResponse, HotelDocumentResultDto>, new();
    }
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _client;
        private readonly string _defaultIndex;

        public ElasticSearchService(IElasticClient client, IOptions<ElasticSearchOptions> options)
        {
            _client = client;
            _defaultIndex = options.Value.DefaultIndex;

            CreateIndexIfNotExists();
        }

        private void CreateIndexIfNotExists()
        {
            if (ElasticSearchIndexCache.IndexExists(_defaultIndex))
            {
                return;
            }

            var existsResponse = _client.Indices.Exists(new IndexExistsRequest(_defaultIndex));
            if (!existsResponse.Exists)
            {
                var createIndexResponse = _client.Indices.Create(_defaultIndex, c => c
                    .Map<HotelDocument>(m => m
                        .AutoMap()
                        .Properties(p => p
                            .GeoPoint(g => g
                                .Name(n => n.Location)))));

                if (!createIndexResponse.IsValid)
                {
                    throw new Exception($"Failed to create index: {createIndexResponse.OriginalException.Message}");
                }
            }

            ElasticSearchIndexCache.AddIndex(_defaultIndex);
        }

        public async Task IndexHotelAsync(HotelDocument hotelDocument)
        {
            var response = await _client.IndexAsync(hotelDocument, idx => idx.Index(_defaultIndex));
            if (!response.IsValid)
            {
                throw new Exception($"Failed to index document: {response.OriginalException.Message}");
            }
        }

        public async Task<TResponse> SearchHotelsAsync<TResponse>(double lat,
            double lon, int radius, int page, int pageSize) where TResponse : PaginationResponse<TResponse, HotelDocumentResultDto>, new()
        {
            var searchResponse = await _client.SearchAsync<HotelDocumentResultDto>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Must(mu => mu
                            .GeoDistance(g => g
                                .Field(f => f.Location)
                                .DistanceType(GeoDistanceType.Arc)
                                .Location(lat, lon)
                                .Distance($"{radius}m")))))
                .Sort(ss => ss
                    .GeoDistance(g => g
                        .Field(f => f.Location)
                        .DistanceType(GeoDistanceType.Arc)
                        .Order(SortOrder.Ascending)
                        .Unit(DistanceUnit.Meters)
                        .Points(new GeoLocation(lat, lon)))
                    .Ascending(a => a.Price))
                .From((page - 1) * pageSize)
                .Size(pageSize)
                .TrackTotalHits(true));

            if (!searchResponse.IsValid)
            {
                throw new Exception($"Search request failed: {searchResponse.OriginalException.Message}");
            }

            var paginationResponse = new TResponse
            {
                Data = searchResponse.Documents,
                TotalCount = searchResponse.Total,
                PageNumber = page,
                PageSize = pageSize
            };

            IncludeDistanceInResult(searchResponse);

            return paginationResponse;
        }

        private static void IncludeDistanceInResult(ISearchResponse<HotelDocumentResultDto> searchResponse)
        {
            foreach (var hit in searchResponse.Hits)
            {
                var hotelDocument = hit.Source;
                var distance = hit.Sorts.FirstOrDefault() ?? 0.0;

                _ = double.TryParse(distance.ToString(), out var hotelDistance);
                hotelDocument.Distance = (int)Math.Round(hotelDistance);
            }
        }
    }

    public class HotelDocumentResultDto : HotelDocument
    {
        public int Distance { get; set; }
    }
}
