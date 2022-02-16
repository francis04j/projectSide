using JustEat.Search.Domain;
using System.Collections.Generic;

namespace Application.Filters
{
    public interface IRestaurantsFilter
    {
        IEnumerable<Restaurant> FilterByCuisine(IEnumerable<Restaurant> restaurants, string filter);
    }
}
