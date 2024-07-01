using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Dto.Product
{
    public class ProductCategoryDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Category? Category {get; set;}
    }
}