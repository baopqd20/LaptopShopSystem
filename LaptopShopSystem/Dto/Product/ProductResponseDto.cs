using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Dto.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public double Rate { get; set; }
        public int Remain { get; set; }
        public int Total { get; set; }
        public string? Type { get; set; }
        public required ProductDetailsDto Details { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public BrandDto? Brand { get; set; }
        public DateTime Created { get; set; }
    }
}