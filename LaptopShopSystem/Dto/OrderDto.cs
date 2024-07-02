namespace LaptopShopSystem.Dto
{
    public class OrderDto
    {
        public string PayMethod { get; set; }

        public int ShipFee = 20000;

        public DateTime CreateTime = DateTime.Now;
    }
}
