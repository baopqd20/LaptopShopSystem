using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShopSystem.Helper
{
    public class QueryObject
    { 
        public string? ProductName{get;set;}=null;
        public string? BrandName{get;set;} = null;
        public string? CategoryName{get;set;} = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}