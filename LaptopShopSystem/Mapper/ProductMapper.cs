using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Dto.Product;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                BrandId = productModel.BrandId,
                Color = productModel.Color,
                Name = productModel.Name,
                Discount = productModel.Discount,
                Price = productModel.Price,
                Remain = productModel.Remain,
                Total = productModel.Total,
                Type = productModel.Type,
                Created = productModel.Created,
                Details = new ProductDetailsDto
                {
                    Weight = productModel.Details.Weight,
                    Image_Urls = productModel.Details.Image_Urls,
                    Audio = productModel.Details.Audio,
                    Bluetooth = productModel.Details.Bluetooth,
                    Cpu = productModel.Details.Cpu,
                    Disk = productModel.Details.Disk,
                    Keyboard = productModel.Details.Keyboard,
                    Lan = productModel.Details.Lan,
                    Monitor = productModel.Details.Monitor,
                    Os = productModel.Details.Os,
                    Pin = productModel.Details.Pin,
                    Ram = productModel.Details.Ram,
                    Size = productModel.Details.Size,
                    Title = productModel.Details.Title,
                    Vga = productModel.Details.Vga,
                    Webcam = productModel.Details.Webcam,
                    Wifi = productModel.Details.Wifi,
                    Port = productModel.Details.Port
                },
                CategoryIds = productModel.ProductCategories.Select(pc => pc.CategoryId).ToList()
            };
        }

        public static Product ToProductModel(this ProductDto productDto)
        {
            return new Product
            {
                BrandId = productDto.BrandId,
                Name = productDto.Name,
                Color = productDto.Color,
                Discount = productDto.Discount,
                Price = productDto.Price,
                Remain = productDto.Remain,
                Total = productDto.Total,
                Type = productDto.Type,
                Created = productDto.Created,
                Details = new ProductDetails
                {
                    Weight = productDto.Details.Weight,
                    Image_Urls = productDto.Details.Image_Urls,
                    Audio = productDto.Details.Audio,
                    Bluetooth = productDto.Details.Bluetooth,
                    Cpu = productDto.Details.Cpu,
                    Disk = productDto.Details.Disk,
                    Keyboard = productDto.Details.Keyboard,
                    Lan = productDto.Details.Lan,
                    Monitor = productDto.Details.Monitor,
                    Os = productDto.Details.Os,
                    Pin = productDto.Details.Pin,
                    Ram = productDto.Details.Ram,
                    Size = productDto.Details.Size,
                    Title = productDto.Details.Title,
                    Vga = productDto.Details.Vga,
                    Webcam = productDto.Details.Webcam,
                    Wifi = productDto.Details.Wifi,
                    Port = productDto.Details.Port,
                },
                ProductCategories = productDto.CategoryIds.Select(categoryId => new ProductCategory
                {
                    CategoryId = categoryId
                }).ToList()
            };
        }
        public static ProductResponseDto ToProdResponse(this Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                BrandId = product.BrandId,
                Name = product.Name,
                Color = product.Color,
                Discount = product.Discount,
                Price = product.Price,
                Remain = product.Remain,
                Total = product.Total,
                Type = product.Type,
                Created = product.Created,
                Details = new ProductDetailsDto
                {
                    Weight = product.Details.Weight,
                    Image_Urls = product.Details.Image_Urls,
                    Audio = product.Details.Audio,
                    Bluetooth = product.Details.Bluetooth,
                    Cpu = product.Details.Cpu,
                    Disk = product.Details.Disk,
                    Keyboard = product.Details.Keyboard,
                    Lan = product.Details.Lan,
                    Monitor = product.Details.Monitor,
                    Os = product.Details.Os,
                    Pin = product.Details.Pin,
                    Ram = product.Details.Ram,
                    Size = product.Details.Size,
                    Title = product.Details.Title,
                    Vga = product.Details.Vga,
                    Webcam = product.Details.Webcam,
                    Wifi = product.Details.Wifi,
                    Port = product.Details.Port,
                },
                Categories = product.ProductCategories
            .Select(pc => new CategoryDto
            {
                Name = pc.Category.Name
            }).ToList(),
                Brand = new BrandDto
                {
                    Name = product.Brand.Name
                }
            };
        }
    }
}