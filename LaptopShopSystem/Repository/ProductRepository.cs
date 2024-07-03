using Microsoft.EntityFrameworkCore;
using LaptopShopSystem.Data;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using LaptopShopSystem.Dto.Product;
using Newtonsoft.Json.Serialization;
using LaptopShopSystem.Mapper;

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

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products
        .Include(p => p.Details)
        .Include(p => p.ProductCategories)
        .FirstOrDefaultAsync(p => p.Id == id);
            if (productModel == null)
            {
                return null;
            }
            if(productModel.Details !=null){
                // _context.ProductDetails.Remove(productModel.Details);
                productModel.Remain =0;
            }
            // //Remove reviews
            // var reviews = _context.Reviews.Where(r=> r.ProductId == id);
            // _context.Reviews.RemoveRange(reviews);

            // _context.ProductCategories.RemoveRange(productModel.ProductCategories);
            // // Remove from Wishlists
            // var wishlists = _context.Wishlist.Where(w => w.ProductId == id);
            // _context.Wishlist.RemoveRange(wishlists);
            // // Remove producgtModel
            // _context.Products.Remove(productModel);

            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await _context.Products
        .Include(p => p.Details)
        .Include(p => p.Brand)
        .Include(p => p.ProductCategories)
        .ThenInclude(pc => pc.Category)
        .FirstOrDefaultAsync(p => p.Id == id);

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
            return _context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, ProductDto productDto)
        {
            var existingProduct = await _context.Products
         .Include(p => p.Details)
         .Include(p => p.Brand)
         .Include(p => p.ProductCategories)
         .ThenInclude(pc => pc.Category)
         .FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.BrandId = productDto.BrandId;
            existingProduct.Color = productDto.Color;
            existingProduct.Discount = productDto.Discount;
            existingProduct.Price = productDto.Price;
            existingProduct.Rate = productDto.Rate;
            existingProduct.Remain = productDto.Remain;
            existingProduct.Total = productDto.Total;
            existingProduct.Type = productDto.Type;
            existingProduct.Created = productDto.Created;


            // Update ProductDetails
            if (existingProduct.Details != null)
            {
                existingProduct.Details.Weight = productDto.Details.Weight;
                existingProduct.Details.Image_Urls = productDto.Details.Image_Urls;
                existingProduct.Details.Audio = productDto.Details.Audio;
                existingProduct.Details.Bluetooth = productDto.Details.Bluetooth;
                existingProduct.Details.Cpu = productDto.Details.Cpu;
                existingProduct.Details.Disk = productDto.Details.Disk;
                existingProduct.Details.Keyboard = productDto.Details.Keyboard;
                existingProduct.Details.Lan = productDto.Details.Lan;
                existingProduct.Details.Monitor = productDto.Details.Monitor;
                existingProduct.Details.Os = productDto.Details.Os;
                existingProduct.Details.Pin = productDto.Details.Pin;
                existingProduct.Details.Ram = productDto.Details.Ram;
                existingProduct.Details.Size = productDto.Details.Size;
                existingProduct.Details.Title = productDto.Details.Title;
                existingProduct.Details.Vga = productDto.Details.Vga;
                existingProduct.Details.Webcam = productDto.Details.Webcam;
                existingProduct.Details.Wifi = productDto.Details.Wifi;
                existingProduct.Details.Port = productDto.Details.Port;
            }

            // Update ProductCategories
            existingProduct.ProductCategories.Clear();
            foreach (var categoryId in productDto.CategoryIds)
            {
                existingProduct.ProductCategories.Add(new ProductCategory { CategoryId = categoryId });
            }

            try
            {
                await _context.SaveChangesAsync();
                return existingProduct;
            }
            catch (DbUpdateException ex)
            {
                // Handle exception
                throw new Exception("Failed to update product", ex);
            }
        }
    }
}