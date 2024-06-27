using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public Product? Product { get; set; }
        public int User_Id { get; set; }
        public User? User { get; set; }
        public int Rating { get; set; }
        public string? Text  { get; set; }
    }
}