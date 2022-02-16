using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.HttpClients;
using Microsoft.Extensions.Configuration;

namespace Application
{
    public class JustEatSearchRestaurantClient : IJustEatSearchRestaurantClient
    {
        private readonly IConfiguration Configuration;
        private readonly IHttpClient _httpClient;

        public JustEatSearchRestaurantClient(IConfiguration configuration, IHttpClient httpClient)
        {
            Configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SearchRestaurantsByCode(string code)
        {
            try
            {
                string requestUrl = $"{Configuration.GetSection("SearchBaseUrl").Value}/{code}";
                return await _httpClient.GetAsync(requestUrl);
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}