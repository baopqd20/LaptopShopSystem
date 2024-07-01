using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LaptopShopSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId {  get; set; }
        public string? Color { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public int Remain { get; set; }
        public int Total { get; set; }
        public string? Type { get; set; } 
        
        public required ProductDetails Details { get; set; }
        public DateTime Created { get; set; }
        public required ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }
        public Brand? Brand { get; set; }
    }
}