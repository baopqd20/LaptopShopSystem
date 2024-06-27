

namespace LaptopShopSystem.Models
{
    public class Cart
    {
        public int Id  { get; set; }
        public int User_Id  { get; set; }
        public required User User  { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}