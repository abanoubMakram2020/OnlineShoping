using AutoMapper;
using OnlineShoping.Application.DTOs.InputDTO;
using OnlineShoping.Application.DTOs.OutputDTO;
using OnlineShoping.Domain.Entities;

namespace OnlineShoping.Application.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleInputDTO>().ReverseMap();
            CreateMap<Role, RoleOutputDTO>().ReverseMap();

            CreateMap<User, UserInputDTO>().ReverseMap();
            CreateMap<User, UserOutputDTO>().ReverseMap();
            CreateMap<User, RegistrationDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();

            CreateMap<Category, CategoryInputDTO>().ReverseMap();
            CreateMap<Category, CategoryOutputDTO>().ReverseMap();

            CreateMap<Product, ProductInputDTO>().ReverseMap();
            CreateMap<Product, ProductOutputDTO>().ReverseMap();

            CreateMap<Cart, CartInputDTO>().ReverseMap();
            CreateMap<Cart, CartOutputDTO>().ReverseMap();

            CreateMap<CartItem, CartItemInputDTO>().ReverseMap();
            CreateMap<CartItem, CartItemOutputDTO>().ReverseMap();

        }
    }
}
