namespace LaptopShopSystem.Models
{
    public class ProductDetails
    {
        public int ProductId { get; set; }
        public string? Weight { get; set; }
        public string? Image_Urls { get; set; }
        public string? Audio { get; set; }
        public string? Bluetooth { get; set; }
        public string? Cpu { get; set; }
        public string? Disk { get; set; }
        public string? Keyboard { get; set; }
        public string? Lan { get; set; }
        public string? Monitor { get; set; }
        public string? Os { get; set; }
        public string? Pin { get; set; }
        public string? Ram { get; set; }
        public string? Size { get; set; }
        public string? Title { get; set; }
        public string? Vga { get; set; }
        public string? Webcam { get; set; }
        public string? Wifi { get; set; }
        public string? Port { get; set; }
        public Product Product { get; set; }
    }
}
