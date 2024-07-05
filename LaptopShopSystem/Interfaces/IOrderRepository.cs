using LaptopShopSystem.Dto;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder (int userId, OrderDto order);
        ICollection<Order> GetOrderByUserId (int UserId);
        Order GetOrderByOrderId(int OrderId);
        bool CancelOrder (int OrderId);
        bool Save();
        
    }
}
