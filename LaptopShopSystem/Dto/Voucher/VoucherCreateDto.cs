using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Dto.Voucher
{
    public class VoucherCreateDto
    {
        public int Discount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required string Title { get; set; }
        public int Total { get; set; }
        public int Remain { get; set; }
        public string? Type { get; set; }
    }
}