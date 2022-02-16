using AcceptanceTests.Models;
using FluentAssertions;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace AcceptanceTests.APIs.Search
{
    // this is to allow us to inject the broker into our tests
    [Collection(nameof(ApiTestCollection))]
    public class SearchApiTests
    {
        private readonly SearchApiBroker searchApiBroker;

        public SearchApiTests(SearchApiBroker searchApiBroker) => 
            this.searchApiBroker = searchApiBroker;

        private Query CreateRandomQuery() =>
            new Filler<Query>().Create();

        [Fact]
        public async Task ShouldSearchRestaurant()
        {
            //given
            var query = CreateRandomQuery();
            //--- create student

            //when
            //-- 
            var actualRestaurant = searchApiBroker.FindRestaurant(query);
            
            //then
            
        }

     }
}
