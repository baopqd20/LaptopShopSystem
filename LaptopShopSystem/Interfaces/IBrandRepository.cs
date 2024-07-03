using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IBrandRepository
    {
        bool CreateBrand(Brand brand);
        bool UpdateBrand(Brand brand);
        bool DeleteBrand(Brand brand);
        Brand GetBrandById(int id);
        bool BrandExists(int id);
        bool Save();
        Task<List<Brand>> GetBrands();
    }
}
