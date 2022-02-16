using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class PostControllerTests : UnitTest1
    {
        [Fact]
        public async Task Get_WithoutAnyID_ReturnsEmptyResponse()
        {
            //Arrange

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }
    }
}
