using AutoMapper;
using Application.Commands;
using Domain.Entities;
using Application.DTOs;

namespace Application.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCart,ShoppingCartDto>().ReverseMap();
            CreateMap<CreateShoppingCartCommand,ShoppingCart>().ReverseMap();

        }
    }
}
