using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int id);
        ICollection<ProductDetails> GetProductDetailsByProductId(int product_Id);

        ICollection<Product> GetProductsByName(string Name);
        ICollection<Product> GetProductsByPrice(int Price);
        Task<Product> CreateAsync(Product ProductModel);
        // TODO 
        // Task<Product?> UpdateAsync(int id, UpdateProductRequestDto ProductDto); 
        Task<Product?> DeleteAsync(int id);
        Task<bool> ProductExists(int id);
    }
}