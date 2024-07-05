namespace LaptopShopSystem.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string Start {  get; set; }
        public string End { get; set; }
        public string IntentTime { get; set; }
        public int ShipFee { get; set; }
        public string Status { get; set; }
    }
}
