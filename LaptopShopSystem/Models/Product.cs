using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LaptopShopSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
        public int Remain { get; set; }
        public int Total { get; set; }
        public string? Type { get; set; }
        public string? Weight { get; set; }
        public int Category_Id { get; set; }
        public string? Image_Urls { get; set; }
        public string? Audio { get; set; }
        public string? Blutooth { get; set; }
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
        public DateTime Created { get; set; }
        public Category Category  { get; set; }
    }
}