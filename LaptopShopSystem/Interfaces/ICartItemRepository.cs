using LaptopShopSystem.Dto;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface ICartItemRepository
    {
        bool CreateCartItem(int productId, int cartId, CartItemDto cartItem);
        bool UpdateCartItem(CartItem cartItem);
        bool DeleteCartItem(CartItem cartItem);
        ICollection<CartItem> GetCartItems();
        ICollection<CartItem> GetCartItemsByCartId(int cartId);
        bool Save();
    }
}
