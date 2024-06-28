using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Repository
{
    public class CartRepository:ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCart(Cart cart)
        {
            _context.Add(cart);
            return save();
        }

        public bool DeleteCart(Cart cart)
        {
            _context.Remove(cart);
            return save();
        }

        public Cart GetCartByUserId(int userId)
        {
            return _context.Carts.Where(p => p.UserId == userId).FirstOrDefault();
        }

        public bool save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
