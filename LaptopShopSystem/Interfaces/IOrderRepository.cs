using LaptopShopSystem.Dto;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IOrderRepository
    {
        bool CreateOrder (int userId, OrderDto order);
        bool Save();
        
    }
}
