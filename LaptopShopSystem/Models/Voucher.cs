
namespace LaptopShopSystem.Models
{
    public class Voucher
    {
        public int Id { get; set; }
        public float Discount { get; set; }
        public string? Status { get; set; }
        public string? Code { get; set; }
        public string? End_date { get; set; }
        public string? Start_date { get; set; }
        public string? Title { get; set; }
        public int Total { get; set; }
        public int Remain { get; set; }
        public string? Type { get; set; }
    }
}