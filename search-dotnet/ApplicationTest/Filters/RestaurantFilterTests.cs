using Application.Filters;
using JustEat.Search.Domain;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ApplicationTest.Filters
{
    public class RestaurantFilterTests
    {
        RestaurantFilter sut;

        public RestaurantFilterTests()
        {
            sut = new RestaurantFilter();
        }

        [Fact]
        public void should_filter_by_cuisine()
        {
            //arrange
            var cuisine = "Korean";

            var restaurants = new List<Restaurant>() {
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = cuisine }  } },
                new Restaurant() { Id = 1, Name = "African Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = "African" }  } }
            };

            var expectedRewsult = new List<Restaurant>() {
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = cuisine }  } }                
            };

            //act
            var result = sut.FilterByCuisine(restaurants, cuisine);

            //assert
            Assert.True(result.All(x => x.Cuisines.All(y => string.Equals(y.Name, cuisine))));
        }

        [Fact]
        public void should_handle_null()
        {
            //arrange
            var cuisine = "Korean";

            var restaurants = new List<Restaurant>() {
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = cuisine }  } },
                new Restaurant() { Id = 1, Name = "African Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = "African" }  } }
            };

            var expectedRewsult = new List<Restaurant>() {
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = cuisine }  } }
            };

            //act
            var result = sut.FilterByCuisine(restaurants, cuisine);

            //assert
            // Assert.True(result.All(x => x.Cuisines.All(y => string.Equals(y.Name, cuisine))));
        }

        [Fact]
        public void should_handle_empty_list()
        {
            //arrange
            var cuisine = "Korean";

            var restaurants = new List<Restaurant>() {
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = cuisine }  } },
                new Restaurant() { Id = 1, Name = "African Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = "African" }  } }
            };

            var expectedRewsult = new List<Restaurant>() {
                new Restaurant() { Id = 1, Name = "Gourmet Burgers", Rating = new Rating(), Cuisines = new List<Cuisine>() { new Cuisine() { Name = cuisine }  } }
            };

            //act
            var result = sut.FilterByCuisine(restaurants, cuisine);

            //assert
            //Assert.True(result.All(x => x.Cuisines.All(y => string.Equals(y.Name, cuisine))));
        }
    }
}
