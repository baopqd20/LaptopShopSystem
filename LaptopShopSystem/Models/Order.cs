namespace LaptopShopSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductPrice { get; set; }
        public int ShipFee { get; set; }
        public int Total {  get; set; }
        public DateTime CreateTime { get; set; }
        public ICollection<CartItem> OrderItems { get; set; }
        public string PayMethod { get; set; }
        public User User { get; set; }
    }
}
