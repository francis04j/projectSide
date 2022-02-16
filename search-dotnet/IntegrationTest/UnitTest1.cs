using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTest
{
    public class UnitTest1
    {
        protected readonly HttpClient TestClient;

        protected UnitTest1()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("TestDb"));

                    });
                });
            TestClient = appFactory.CreateClient();
        }
     
        public async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", GetJwt());
        }

        public async Task<string> GetJwt()
        {
            var response = TestClient.PostAsJsonAsync(ApiROutrs.)
        }
    }
}
