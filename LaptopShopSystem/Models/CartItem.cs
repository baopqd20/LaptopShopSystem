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
        public int Product_Id  { get; set; }
        public required Product Product  { get; set; }
        public int Quantity  { get; set; }
        public int Cart_Id { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
    
    }
}