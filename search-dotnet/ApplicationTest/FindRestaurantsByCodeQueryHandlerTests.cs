using Application.Filters;
using Application.Mappers;
using Application.ViewModels;
using JustEat.Search.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Application.Test
{
    public class FindRestaurantsByCodeQueryHandlerTests
    {
        private Mock<IJustEatSearchRestaurantClient> searchClient;
        private Mock<ISearchRestaurantsMapper> mapper;
        private Mock<IJsonDeserializer> deserializer;
        private Mock<IRestaurantsFilter> filterer;

        FindRestaurantsByCodeQuery query;
        SearchResult searchResult;
        List<RestaurantViewModel> restaurantsVm;
        List<Restaurant> restaurants;
        HttpResponseMessage httpResponse;

        public FindRestaurantsByCodeQueryHandlerTests()
        {
            searchClient = new Mock<IJustEatSearchRestaurantClient>();
            mapper = new Mock<ISearchRestaurantsMapper>();
            deserializer = new Mock<IJsonDeserializer>();
            filterer = new Mock<IRestaurantsFilter>();
            restaurants = new List<Restaurant>() { 
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = "Korean" }  } },
                new Restaurant() { Id = 1, Name = "African Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = "African" }  } }
            };
            searchResult = new SearchResult() { Restaurants = restaurants };
            restaurantsVm = new List<RestaurantViewModel>() { 
                new RestaurantViewModel() { Name = "Gourmet Burgers", Rating = new RatingViewModel(), Cuisines = new List<CuisineViewModel>() { new CuisineViewModel() { Name = "Korean" } } },
                new RestaurantViewModel() { Name = "African Burgers", Rating = new RatingViewModel(), Cuisines = new List<CuisineViewModel>() }
            };

            query = new FindRestaurantsByCodeQuery(code: "se1", cuisine: "Korean");
            httpResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(restaurants) };
            searchClient.Setup(sc => sc.SearchRestaurantsByCode(query.Code)).Returns(Task.FromResult(httpResponse));
            deserializer.Setup(ds => ds.DeserializeAsync(httpResponse)).Returns(Task.FromResult(searchResult));
            filterer.Setup(ft => ft.FilterByCuisine(It.IsAny<IEnumerable<Restaurant>>(), query.Cuisine)).Returns(restaurants);
            mapper.Setup(mp => mp.MapToViewModel(It.IsAny<IEnumerable<Restaurant>>())).Returns(restaurantsVm);
        }

        [Fact]
        public async Task Should_return_for_empty_search_word()
        {
            var query = "";
            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, filterer.Object);

            var result = await sut.Handle(new FindRestaurantsByCodeQuery(query), CancellationToken.None);

            Assert.False(result.Any());
        }

        [Fact]
        public async Task Should_return_for_null_search_word()
        {
            string query = null;
            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, null);

            var result = await sut.Handle(new FindRestaurantsByCodeQuery(query), CancellationToken.None);

            Assert.False(result.Any());
        }

        [Fact]
        public async Task Should_Find_using_SearchClient()
        {            
         
            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, filterer.Object);

           await sut.Handle(new FindRestaurantsByCodeQuery(query.Code), CancellationToken.None);

            searchClient.Verify(sc => sc.SearchRestaurantsByCode(query.Code));
        }

        [Fact]
        public async Task Should_deserialize_http_respopnse()
        {
    
            deserializer.Setup(ds => ds.DeserializeAsync(httpResponse)).Returns(Task.FromResult(searchResult));

            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, filterer.Object);
            var result = await sut.Handle(new FindRestaurantsByCodeQuery(query.Code), CancellationToken.None);

            deserializer.Verify(x => x.DeserializeAsync(httpResponse), Times.Once());
        }

        [Fact]
        public async Task Should_map_success_result_using_Mapper()
        {          
           
            mapper.Setup(mp => mp.MapToViewModel(searchResult.Restaurants)).Returns(restaurantsVm);

            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, filterer.Object);
            var result = await sut.Handle(new FindRestaurantsByCodeQuery(query.Code), CancellationToken.None);

            result.ShouldBeOfType<List<RestaurantViewModel>>();
        }

        [Fact]
        public void Should_handle_http_non_success_call()
        {
            var httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = JsonContent.Create("Not Found") };
            searchClient.Setup(sc => sc.SearchRestaurantsByCode(query.Code)).Returns(Task.FromResult(httpResponse));
            deserializer.Setup(ds => ds.DeserializeAsync(httpResponse)).Returns(Task.FromResult(searchResult));
            

            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, filterer.Object);
            var ex = Assert.ThrowsAsync<Exception>(async () => await sut.Handle(new FindRestaurantsByCodeQuery(query.Code), CancellationToken.None));

            Assert.Equal("Not Found", ex.Result.Message);
        }

        [Fact]
        public async void Should_filter_using_cuisine_parameter()
        {
            //arrange
            var cuisineQuery = new FindRestaurantsByCodeQuery(code: "se1", cuisine: "Korean");
            var sut = new FindRestaurantsByCodeQueryHandler(searchClient.Object, mapper.Object, deserializer.Object, filterer.Object);

            //act
            await sut.Handle(cuisineQuery, CancellationToken.None);

            //assert
            filterer.Verify(x => x.FilterByCuisine(searchResult.Restaurants, cuisineQuery.Cuisine), Times.Once());
        }

    }
}
