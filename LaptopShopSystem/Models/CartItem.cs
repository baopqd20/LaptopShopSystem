using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace LaptopShopSystem.Models
{
    public class CartItem
    {
        public int Id { get; set; }
 
        public int Quantity  { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }

        /*public CartItem()
        {
            UnitPrice = Product.Price;
            Amount = UnitPrice * Quantity;
        }*/
    }
}