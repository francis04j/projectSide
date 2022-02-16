using JustEat.Search.Domain;
using System.Collections.Generic;

namespace Application
{
    public class SearchResult
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}
