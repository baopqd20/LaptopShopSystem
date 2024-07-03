using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IPaymentRepository
    {
        
        bool CreatePaymentOfOrder(int orderId);
        ICollection<Payment> GetPaymentByUserId(int userId);
        ICollection<Payment> GetPaymentByOrderId(int orderId);
        bool Save();
    }
}
