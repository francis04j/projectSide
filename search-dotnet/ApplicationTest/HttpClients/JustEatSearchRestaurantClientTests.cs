using Application;
using Application.HttpClients;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationTest.HttpClients
{
    public class JustEatSearchRestaurantClientTests
    {
        JustEatSearchRestaurantClient sut;
        Mock<IConfiguration> configuration;
        const string testContent = "test content";
        const string searchUrl = "http://anyurl";
        Mock<IHttpClient> httpClientMock;

        public JustEatSearchRestaurantClientTests()
        {
            var mockIConfigurationSection = new Mock<IConfigurationSection>();
            mockIConfigurationSection.Setup(x => x.Key).Returns("SearchBaseUrl");
            mockIConfigurationSection.Setup(x => x.Value).Returns(searchUrl);
            configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("SearchBaseUrl")).Returns(mockIConfigurationSection.Object);

            var model = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(testContent)
            };

           httpClientMock = new Mock<IHttpClient>();
            httpClientMock.Setup(c => c.GetAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(model));

            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(model);
            sut = new JustEatSearchRestaurantClient(configuration.Object, httpClientMock.Object);
        }

        [Fact]
        public async void Should_SearchUsingConfigUrlAndSearchWord()
        {
            string searchWord = "ec1";
            HttpResponseMessage result = await sut.SearchRestaurantsByCode(searchWord);

            httpClientMock.Verify(x => x.GetAsync(It.IsAny<string>()));
        }

        [Fact]
        public async void Should_SearchRestaurantsByCode()
        {
            HttpResponseMessage result = await sut.SearchRestaurantsByCode("ec1");

            Assert.Equal(testContent, result.Content.ReadAsStringAsync().Result);
        }
    }
}
