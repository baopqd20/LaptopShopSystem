using LaptopShopSystem.Dto;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IOrderRepository
    {
        bool CreateOrder (int userId, OrderDto order);
        ICollection<Order> GetOrderByUserId (int UserId);
        Order GetOrderByOrderId(int OrderId);

        bool Save();
        
    }
}
