

namespace LaptopShopSystem.Models
{
    public class Cart
    {        
        public int UserId  { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}