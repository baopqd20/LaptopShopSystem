using AutoMapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Dto.Product;
using LaptopShopSystem.Mapper;
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


            CreateMap<ProductCategory,ProductCategoryDto>().ReverseMap();
            CreateMap<Review,ReviewDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();


        }
    }
}
