using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface ICartRepository
    {
        bool CreateCart(Cart cart);
        bool save();
        Cart GetCartByUserId(int userId);
        bool DeleteCart(Cart cart);
    }
}
