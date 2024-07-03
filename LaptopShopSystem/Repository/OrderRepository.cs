using LaptopShopSystem.Data;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly DataContext _context;
        private readonly ICartItemRepository _cartItemRepository;

        public OrderRepository(DataContext context, ICartItemRepository cartItemRepository)
        {
            _context = context;
            _cartItemRepository = cartItemRepository;
        }

        public bool CreateOrder(int userId, OrderDto orderCreate)
        {
            ICollection<OrderItem> OrderItems = new List<OrderItem>();
            var cartItems = _context.CartItems.Where(p => p.Cart.UserId == userId).ToList();
            foreach(var cartItem in cartItems)
            {
                OrderItems.Add(_cartItemRepository.ConvertCartItemToOrderItem(cartItem));
                _cartItemRepository.DeleteCartItem(cartItem);
            }
            var ProductPrice = 0;
            foreach(var orderItem in OrderItems)
            {
                ProductPrice = ProductPrice + orderItem.Amount;
            }
            var order = new Order
            {
                UserId = userId,
                OrderItems = OrderItems,
                ProductPrice = ProductPrice,  
                PayMethod = orderCreate.PayMethod,
                Total = orderCreate.ShipFee + ProductPrice,
                ShipFee = orderCreate.ShipFee,
                Status = orderCreate.Status,
                CreateTime = orderCreate.CreateTime,
                ExpireTime = orderCreate.CreateTime.AddDays(3),
            };
           
            _context.Add(order);
            return Save();
        }

        public Order GetOrderByOrderId(int OrderId)
        {
            return _context.Orders.Where(p => p.Id == OrderId)
                .Include(p => p.OrderItems)
                .Include(p => p.User)
                .FirstOrDefault();
        }

        public ICollection<Order> GetOrderByUserId(int UserId)
        {
            return _context.Orders
                   .Include(o => o.OrderItems)
                   .Where(o => o.UserId == UserId)
                   .ToList();
                       
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
