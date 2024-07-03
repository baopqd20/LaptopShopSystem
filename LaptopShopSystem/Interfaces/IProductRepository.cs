using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Dto.Product;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(QueryObject queryObject);
        Task<Product?> GetProduct(int id);
        Task<Product> CreateAsync(Product ProductModel);
        Task<Product?> UpdateAsync(int id, ProductDto ProductDto); 
        Task<Product?> DeleteAsync(int id);
        Task<bool> ProductExists(int id);

    }
}