using LaptopShopSystem.Data;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

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
                CreateTime = orderCreate.CreateTime,
            };
           
            _context.Add(order);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
