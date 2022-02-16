using AutoMapper;
using JustEat.Search.Domain;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class RestaurantViewModel
    {
        public string Name { get; set; }
        public RatingViewModel Rating { get; set; }
        public IEnumerable<CuisineViewModel> Cuisines { get; set; }
    }
}