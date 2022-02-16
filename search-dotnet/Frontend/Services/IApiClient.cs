using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public interface IApiClient
    {
        Task<dynamic> SearchRestaurantByPostCode(string postCode);
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> SearchRestaurantByPostCode(string postCode)
        {
            var result = await _httpClient.GetAsync($"/api/search/{postCode}");
            throw new NotImplementedException();
        }
    }
}
