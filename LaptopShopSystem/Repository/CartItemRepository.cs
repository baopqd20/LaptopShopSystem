using LaptopShopSystem.Data;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataContext _context;

        public CartItemRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateCartItem(int productId, int cartId, CartItemDto cartItemCreate)
        {
            var cartItem = new CartItem
            {
                Quantity = cartItemCreate.Quantity,
                Cart = _context.Carts.Where(p => p.UserId == cartId).FirstOrDefault(),
                Product = _context.Products.Where(p => p.Id == productId).FirstOrDefault(),
                UnitPrice = _context.Products.Where(p => p.Id == productId).FirstOrDefault().Price,
                Amount = cartItemCreate.Quantity * _context.Products.Where(p => p.Id == productId).FirstOrDefault().Price
            };
            _context.Add(cartItem);
            return Save();
        }

        public bool DeleteCartItem(CartItem cartItem)
        {
            _context.Remove(cartItem);
            return Save();
        }

        public ICollection<CartItem> GetCartItems()
        {
            return _context.CartItems.ToList();
        }

        public ICollection<CartItem> GetCartItemsByCartId(int cartId)
        {
            return _context.CartItems
            .Include(ci => ci.Cart) // Nạp thông tin của Cart
            .Include(ci => ci.Product) // Nạp thông tin của Product
            .Where(ci => ci.Cart.UserId == cartId)
            .ToList();
            
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCartItem(CartItem cartItem)
        {
            _context.Update(cartItem);
            return Save();
        }
    }
}
