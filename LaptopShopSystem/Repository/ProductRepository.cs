using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IBrandRepository _brandRepo;
        private readonly ICategoryRepository _categoryRepo;
        public ProductRepository(DataContext context, IBrandRepository brandRepo, ICategoryRepository categoryRepo)
        {
            _context = context;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;

        }
        public async Task<Product> CreateAsync(Product ProductModel)
        {
       
            await _context.Products.AddAsync(ProductModel);
            await _context.SaveChangesAsync();
            return ProductModel;
        }

        public Task<Product?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductDetails> GetProductDetailsByProductId(int product_Id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetProductsByName(string Name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetProductsByPrice(int Price)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProductExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}