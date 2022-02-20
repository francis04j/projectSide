using AcceptanceTests.Models;
using FluentAssertions;
using System.Threading.Tasks;
using Api;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Exceptions;
using Tynamix.ObjectFiller;
using Xunit;

namespace AcceptanceTests.APIs.Search
{
    // this is to allow us to inject the broker into our tests
   [Collection(nameof(ApiTestCollection))]
    public class SearchApiTests //: IClassFixture<WebApplicationFactory<Startup>>
    {
       private readonly SearchApiBroker _searchApiBroker;

        public SearchApiTests(SearchApiBroker searchApiBroker) => 
            _searchApiBroker = searchApiBroker;
        
      //  private readonly WebApplicationFactory<Startup> _factory;

     /*  public SearchApiTests(WebApplicationFactory<Startup> factory)
       {
           _factory = factory;
       }
    */
       private Query CreateRandomQuery() =>
           new Filler<Query>().Create();
       
        [Fact]
        public async Task ShouldSearchRestaurant()
        {
            //Arrange
            var query = CreateRandomQuery();
           // var client = _factory.CreateClient();
            //--- create student

            //Act
            //-- 
           var response = await _searchApiBroker.FindRestaurant(query);
          // var response = await client.GetAsync("api/search?id=1234s");
           
           //Assert
          
           //response.EnsureSuccessStatusCode();
           response.Should().NotBeNull();
           
           // query.Should().NotBeNull();
        }

     }
}
