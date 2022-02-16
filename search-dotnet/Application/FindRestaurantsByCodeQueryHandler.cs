using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Application.ViewModels;
using Application.Mappers;
using System.Linq;
using Application.Filters;
using JustEat.Search.Domain;

namespace Application
{
    public class FindRestaurantsByCodeQueryHandler : IRequestHandler<FindRestaurantsByCodeQuery, IEnumerable<RestaurantViewModel>>
    {
        private readonly IJustEatSearchRestaurantClient _client;
        private readonly ISearchRestaurantsMapper _mapper;
        private readonly IJsonDeserializer _deserializer;
        private readonly IRestaurantsFilter _restaurantFilter;

        public FindRestaurantsByCodeQueryHandler(IJustEatSearchRestaurantClient client, ISearchRestaurantsMapper mapper, IJsonDeserializer des, IRestaurantsFilter restaurantFilter)
        {
            _client = client;
            _mapper = mapper;
            _deserializer = des;
            _restaurantFilter = restaurantFilter;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(FindRestaurantsByCodeQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.Code)) return Enumerable.Empty<RestaurantViewModel>();

            var response = await _client.SearchRestaurantsByCode(query.Code);

            if (response.IsSuccessStatusCode)
            {
                SearchResult restaurants = await _deserializer.DeserializeAsync(response);

                IEnumerable<Restaurant> filteredRestaurants = _restaurantFilter.FilterByCuisine(restaurants?.Restaurants, query.Cuisine);

                IEnumerable<RestaurantViewModel> viewMdoel = _mapper.MapToViewModel(filteredRestaurants);

                return viewMdoel;
            }
            else
            {
                //TODO: extend this to translate response to HTTP status code and useful message
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
