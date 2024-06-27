

namespace LaptopShopSystem.Models
{
    public class Cart
    {        
        public int User_Id  { get; set; }
        public required User User  { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}