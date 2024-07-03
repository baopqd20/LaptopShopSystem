using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;

        public BrandRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateBrand(Brand brand)
        {
            _context.Add(brand);
            return Save();
        }

        public bool DeleteBrand(Brand brand)
        {
            _context.Remove(brand);
            return Save();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBrand(Brand brand)
        {
            _context.Update(brand);
            return Save();
        }
        public Brand GetBrandById(int id)
        {
            return _context.Brands.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool BrandExists(int id)
        {
            var brand = _context.Brands.Where(p => p.Id == id).FirstOrDefault();
            if (brand == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Brand>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
