using AutoMapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Dto.Product;
using LaptopShopSystem.Dto.Review;
using LaptopShopSystem.Dto.Voucher;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Helper
{
    public class MapperProfiles : Profile
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
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewUpdateDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Voucher, VoucherDto>().ReverseMap();
            CreateMap<Voucher, VoucherCreateDto>().ReverseMap();
            CreateMap<Voucher, VoucherUpdateDto>().ReverseMap();
        }
    }
}
