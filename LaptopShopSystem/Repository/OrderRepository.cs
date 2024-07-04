﻿using LaptopShopSystem.Data;
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

        public bool CancelOrder(int orderId)
        {
            var order = _context.Orders.Where(p => p.Id == orderId).FirstOrDefault();

            if(order.Status != "Pending")
            {
                return false;
            }
            var orderItems = _context.OrderItems.Where(p => p.OrderId == order.Id).ToList();
            foreach (var orderitem in orderItems)
            {
                var product = _context.Products.Where(p => p.Id == orderitem.ProductId).FirstOrDefault();
                product.Remain = product.Remain + orderitem.Quantity;
                product.Total = product.Total - orderitem.Quantity;
                _context.Update(product);
            }
            order.Status = "Cancel";
            _context.Update(order);
            return Save();
        }

        public int CreateOrder(int userId, OrderDto orderCreate)
        {
            ICollection<OrderItem> OrderItems = new List<OrderItem>();
            var cartItems = _context.CartItems.Where(p => p.Cart.UserId == userId).ToList();
            if (cartItems.Count == 0)
            {
                return 0;
            }
            foreach(var cartItem in cartItems)
            {
                var product = _context.Products.Where(p => p.Id == cartItem.ProductId).FirstOrDefault();
                if (product.Remain < cartItem.Quantity)
                {
                    return 2;
                }
                product.Remain = product.Remain - cartItem.Quantity;
                product.Total = product.Total + cartItem.Quantity;
                _context.Update(product);
                OrderItems.Add(_cartItemRepository.ConvertCartItemToOrderItem(cartItem));
                _cartItemRepository.DeleteCartItem(cartItem);
            }
            var ProductPrice = 0;
            foreach(var orderItem in OrderItems)
            {
                ProductPrice = ProductPrice + orderItem.Amount;
            }
            if(orderCreate.PayMethod == "Offline")
            {
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
            }
            if(orderCreate.PayMethod == "Online")
            {
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
                    ExpireTime = orderCreate.CreateTime.AddDays(1),
                    
                };
           
                _context.Add(order);
            }
            Save();
            return 1;
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
