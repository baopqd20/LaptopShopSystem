using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Dto.Review;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>?> GetReviewsByProductId(int productId);
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(Review review);
        Task<Review?> UpdateAsync(int Id,Review reviewModel);
        Task<Review?> DeleteAsync(int Id);

    }
}