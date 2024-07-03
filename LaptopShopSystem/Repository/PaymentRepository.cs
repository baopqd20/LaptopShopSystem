using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Repository
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly DataContext _context;

        public PaymentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePaymentOfOrder(int orderId)
        {
            var order = _context.Orders.Where(p => p.Id == orderId).FirstOrDefault();
            if (order.Status != "Pending")
            {
                return false;
            }
            var payment = new Payment
            {
                OrderId = orderId,
                PayTime = DateTime.Now,
                Status = "Success",
                UserId = order.UserId,
            };
            _context.Add(payment);
            order.Status = "Paid";
            _context.Update(order);
            return Save();
        }

        public ICollection<Payment> GetPaymentByOrderId(int orderId)
        {
            return _context.Payments.Where(p => p.OrderId == orderId).ToList();
        }

        public ICollection<Payment> GetPaymentByUserId(int userId)
        {
            return _context.Payments.Where(p => p.UserId == userId).ToList();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
