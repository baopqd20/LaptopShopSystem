using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(QueryObject queryObject);
        Product GetProduct(int id);
        ICollection<ProductDetails> GetProductDetailsByProductId(int product_Id);

        Task<Product> CreateAsync(Product ProductModel);
        // TODO 
        // Task<Product?> UpdateAsync(int id, UpdateProductRequestDto ProductDto); 
        Task<Product?> DeleteAsync(int id);
        Task<bool> ProductExists(int id);
    }
}