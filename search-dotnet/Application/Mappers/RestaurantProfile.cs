using Application.ViewModels;
using AutoMapper;
using JustEat.Search.Domain;

namespace Application.Mappers
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {

            CreateMap<Restaurant, RestaurantViewModel>()
                .ForMember(rs => rs.Name, opt => opt.MapFrom(rs => rs.Name))
                .ForMember(rs => rs.Rating, opt => opt.MapFrom(rs => rs.Rating))
                .ForMember(rs => rs.Cuisines, opt => opt.MapFrom(rs => rs.Cuisines));

           
        }        
    }
}
