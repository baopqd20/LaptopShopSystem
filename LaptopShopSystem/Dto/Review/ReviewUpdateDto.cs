using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Dto.Review
{
    public class ReviewUpdateDto
    {
        public int Rating { get; set; }
        public string? Text  { get; set; }
    }
}