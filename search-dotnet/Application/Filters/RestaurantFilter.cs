using JustEat.Search.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Filters
{
    public class RestaurantFilter : IRestaurantsFilter
    {
        public IEnumerable<Restaurant> FilterByCuisine(IEnumerable<Restaurant> restaurants, string filter)
        {
            return restaurants.Where(x => x.Cuisines != null && x.Cuisines.Any(c => String.Equals(c.Name, filter)));
        }
    }
}
