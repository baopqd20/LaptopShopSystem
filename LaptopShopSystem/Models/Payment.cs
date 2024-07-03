namespace LaptopShopSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PayTime { get; set; }
        public int Status { get; set; }
        public Order Order { get; set; }
    }
}
