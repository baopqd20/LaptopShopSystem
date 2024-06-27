namespace LaptopShopSystem.Models
{
    public class Wishlist
    {
        public int UserId { get; set; }
        public int ProductId {  get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
