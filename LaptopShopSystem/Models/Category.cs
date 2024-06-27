using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Models
{
    public class Category
    {
        public int Id  { get; set; }
        public string? Name { get; set; }
        ICollection<ProductCategory> ProductCategories { get; set; }

    }
}