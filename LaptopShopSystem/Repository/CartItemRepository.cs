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
            };
            _context.Add(cartItem);
            return Save();
        }

        public bool DeleteCartItem(CartItem cartItem)
        {
            _context.Remove(cartItem);
            return Save();
        }

        public bool DeleteCartItemOfACart(int cartId)
        {
            var cartItems = _context.CartItems.Where(p => p.Cart.UserId == cartId);
            foreach (var cartItem in cartItems) {
                _context.Remove(cartItem);
            }
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
            .Include(ci => ci.Product).ThenInclude(pd => pd.Details) // Nạp thông tin của Product
            .Where(ci => ci.Cart.UserId == cartId)
            .ToList();

        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<CartItem?> UpdateCartItem(int cartId, CartItem cartItem)
        {
            var cartItemExist = await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId&& ci.ProductId == cartItem.ProductId);
            Console.WriteLine(cartItemExist);
            if (cartItemExist == null)
            {
                return null;
            }

            cartItemExist.ProductId = cartItem.ProductId;
            cartItemExist.Quantity = cartItem.Quantity;
            Console.WriteLine(cartItem);
            await _context.SaveChangesAsync();
      
            return cartItemExist;
        }

        public OrderItem ConvertCartItemToOrderItem(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException($"Product with Id {cartItem.ProductId} does not exist.");
            }

            return new OrderItem
            {
                Quantity = cartItem.Quantity,
                ProductId = cartItem.ProductId,
                UnitPrice = product.Price,
                Amount = product.Price * cartItem.Quantity
            };
        }

    }
}
