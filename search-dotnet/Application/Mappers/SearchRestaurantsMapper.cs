using Application.ViewModels;
using JustEat.Search.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application.Mappers
{
    public class SearchRestaurantsMapper : ISearchRestaurantsMapper
    {
        public IEnumerable<RestaurantViewModel> MapToViewModel(IEnumerable<Restaurant> result)
        {
            var vmList = new List<RestaurantViewModel>();
            foreach (var item in result)
            {
                var rating = new RatingViewModel() { Average = item
                    .Rating.Average, Count = item.Rating.Count, StarRating = item.Rating.StarRating };
                var cuisinesVm = new List<CuisineViewModel>();
                foreach (var cuisine in item.Cuisines ?? Enumerable.Empty<Cuisine>())
                {
                    cuisinesVm.Add(new CuisineViewModel() { Name = cuisine.Name, SeoName = cuisine.SeoName });
                }
                var rest = new RestaurantViewModel() { Name = item.Name, Rating = rating, Cuisines = cuisinesVm };
                vmList.Add(rest);
            }

            return vmList;
        }
    }
}
