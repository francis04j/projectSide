using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JustEat.Search.Domain;

namespace Application
{
    public class RestaurantsDeserializer : IJsonDeserializer
    {
        public async Task<SearchResult> DeserializeAsync(HttpResponseMessage response)
        {
            using var responseStream = await response?.Content.ReadAsStreamAsync();
            var restaurants = await JsonSerializer.DeserializeAsync<SearchResult> (responseStream);
            return restaurants;
        }
    }


}
