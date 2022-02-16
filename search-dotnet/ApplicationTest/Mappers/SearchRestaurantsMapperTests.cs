using Application.Mappers;
using Application.ViewModels;
using Bogus;
using JustEat.Search.Domain;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ApplicationTest.Mappers
{
    public class SearchRestaurantsMapperTests
    {
        IEnumerable<Restaurant> restaurants;
        readonly SearchRestaurantsMapper sut;

        public SearchRestaurantsMapperTests()
        {
            sut = new SearchRestaurantsMapper();
        }

        [Fact]
        public void Should_Map_Restaurants_to_ViewModel()
        {
            var random = new Bogus.Randomizer();
            var fakeData = new Faker<Restaurant>()
                .RuleFor(u => u.Name, random.String())
                .RuleFor(u => u.Rating, new Rating())
                .RuleFor(u => u.Cuisines, random.ListItems(new List<Cuisine>()));
            restaurants = fakeData.Generate(5);

            var viewModel = sut.MapToViewModel(restaurants);

            viewModel.ShouldBeOfType<List<RestaurantViewModel>>();
            Assert.Equal(5, viewModel.Count());
        }

        [Fact]
        public void Should_Map_Ratings_to_ViewModel()
        {
            var random = new Randomizer();
            var ratingFaker = new Faker<Rating>()
                .RuleFor(x => x.Average, random.Double())
                .RuleFor(x => x.Count, random.Int())
                .RuleFor(x => x.StarRating, random.Double());
            var fakeData = new Faker<Restaurant>()
                .RuleFor(u => u.Name, random.String())
                .RuleFor(u => u.Rating, x => ratingFaker);
            restaurants = fakeData.Generate(1);

            var viewModel = sut.MapToViewModel(restaurants);

            foreach (var item in viewModel)
            {
                Assert.Equal(item.Rating.Average, restaurants.ElementAt(0).Rating.Average);
                Assert.Equal(item.Rating.Count, restaurants.ElementAt(0).Rating.Count);
                Assert.Equal(item.Rating.StarRating, restaurants.ElementAt(0).Rating.StarRating);
            }            
            
        }

        [Fact]
        public void Should_Map_Name()
        {
            string name = "RestaurantA";
            var random = new Randomizer();
            var ratingFaker = new Faker<Rating>()
                .RuleFor(x => x.Average, random.Double())
                .RuleFor(x => x.Count, random.Int())
                .RuleFor(x => x.StarRating, random.Double());
            var fakeData = new Faker<Restaurant>()
                .RuleFor(u => u.Name, name)
                .RuleFor(u => u.Rating, x => ratingFaker);
            restaurants = fakeData.Generate(1);

            var viewModel = sut.MapToViewModel(restaurants);

            foreach (var item in viewModel)
            {
                Assert.Equal(item.Name, name);
            }
        }
    }
}
