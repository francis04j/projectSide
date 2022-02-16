using MediatR;
using Application.ViewModels;
using System.Collections.Generic;

namespace Application
{
    public class FindRestaurantsByCodeQuery : IRequest<IEnumerable<RestaurantViewModel>>
    {
        public string Code { get; }
        public string Cuisine { get; }

        public FindRestaurantsByCodeQuery(string code)
        {
            Code = code;
        }

        public FindRestaurantsByCodeQuery(string code, string cuisine)
        {
            Code = code;
            Cuisine = cuisine;
        }
    }
}
