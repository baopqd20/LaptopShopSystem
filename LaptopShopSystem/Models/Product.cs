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
        public int brandId {  get; set; }
        public string? Color { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public int Remain { get; set; }
        public int Total { get; set; }
        public string? Type { get; set; }        
        public ProductDetails Details { get; set; }
        public DateTime Created { get; set; }
        public Category Category  { get; set; }
        ICollection<ProductCategory> ProductCategories { get; set; }
        ICollection<Wishlist> Wishlists { get; set; }
        public Brand Brand { get; set; }
    }
}