using Application.ViewModels;
using JustEat.Search.Domain;
using System.Collections.Generic;

namespace Application.Mappers
{
    public interface ISearchRestaurantsMapper
    {
        IEnumerable<RestaurantViewModel> MapToViewModel(IEnumerable<Restaurant> result);
    }
}
