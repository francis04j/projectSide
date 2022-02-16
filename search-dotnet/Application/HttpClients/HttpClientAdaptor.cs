using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.HttpClients
{
    public sealed class HttpClientAdaptor : IHttpClient
    {
        readonly HttpClient httpClient;

        public HttpClientAdaptor(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {            
            try
            {
                var response = await httpClient.GetAsync(uri);
                return response;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
