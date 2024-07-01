using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int User_Id { get; set; }
        public int Rating { get; set; }
        public string? Text { get; set; }
    }
}