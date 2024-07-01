using AutoMapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Helper
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
            CreateMap<User, UserAdminDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();

        }
    }
}
