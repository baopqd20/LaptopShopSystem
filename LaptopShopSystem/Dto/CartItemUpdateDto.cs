using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Dto
{
    public class CartItemUpdateDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}