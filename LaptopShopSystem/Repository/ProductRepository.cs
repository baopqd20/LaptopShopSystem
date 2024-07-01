using Microsoft.EntityFrameworkCore;
using LaptopShopSystem.Data;
using LaptopShopSystem.Helper;
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



        public async Task<List<Product>> GetProducts(QueryObject queryObject)
        {
            var query = _context.Products
            .Include(p => p.Details)
            .Include(p => p.Brand)
            .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
            .AsQueryable();
                
            if (!string.IsNullOrEmpty(queryObject.ProductName))
            {
                query = query.Where(p => p.Name.Contains(queryObject.ProductName));
            }

            if (!string.IsNullOrEmpty(queryObject.BrandName))
            {
                query = query.Where(p => p.Brand.Name.Contains(queryObject.BrandName));
            }

            if (!string.IsNullOrEmpty(queryObject.CategoryName))
            {
                query = query.Where(p => p.ProductCategories.Any(pc => pc.Category.Name.Contains(queryObject.CategoryName)));
            }

            if (!string.IsNullOrEmpty(queryObject.SortBy))
            {
                if (queryObject.IsDecsending)
                {
                    query = query.OrderByDescending(p => EF.Property<object>(p, queryObject.SortBy));
                }
                else
                {
                    query = query.OrderBy(p => EF.Property<object>(p, queryObject.SortBy));
                }
            }

            query = query.Skip((queryObject.PageNumber - 1) * queryObject.PageSize)
                         .Take(queryObject.PageSize);

            return await query.ToListAsync();
        }


        public Task<bool> ProductExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}