using AcceptanceTests.Models;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using RESTFulSense.Clients;

namespace AcceptanceTests
{
    public class SearchApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient baseClient;
        public IRESTFulApiFactoryClient apiFactoryClient { get; }

        public SearchApiBroker()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.baseClient = this.webApplicationFactory.CreateClient();
            this.apiFactoryClient = new RESTFulApiFactoryClient(this.baseClient);
        }
        private const string searchRelativeUrl = "api/search";

        public async Task<Restaurant> FindRestaurant(Query query) =>
            await apiFactoryClient.GetContentAsync<Restaurant>($"{searchRelativeUrl}?{query.Term}");

  
    }
}
