using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Review> CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            await UpdateProductRate(review.ProductId);
            return review;
        }

        public async Task<Review?> DeleteAsync(int Id)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingReview == null)
            {
                return null;
            }
            _context.Remove(existingReview);
            await _context.SaveChangesAsync();
            await UpdateProductRate(existingReview.ProductId);
            return existingReview;
        }

        public async Task<List<Review>?> GetReviewsByProductId(int productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();
            if (reviews == null || reviews.Count == 0)
            {
                return null;
            }

            return reviews;
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<Review?> UpdateAsync(int Id, Review reviewModel)
        {
            var existingReview = await _context.Reviews.FindAsync(Id);
            if (existingReview == null) return null;

            existingReview.Text = reviewModel.Text;
            existingReview.Rating = reviewModel.Rating;

            await _context.SaveChangesAsync();
            await UpdateProductRate(existingReview.ProductId);
            return existingReview;
        }

        //Update rate for product
        private async Task UpdateProductRate(int productId)
        {

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .ToListAsync();


            if (product != null)
            {
                if (reviews.Count!=0)
                {
                    product.Rate = reviews.Average(r => r.Rating);
                }
                else
                {
                    product.Rate = 0; 
                }
                // Update product in database
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}