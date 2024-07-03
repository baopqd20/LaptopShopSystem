using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using AutoMapper;
using LaptopShopSystem.Data;
using LaptopShopSystem.Dto.Review;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LaptopShopSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;
        public ReviewController(IReviewRepository reviewRepo, IMapper mapper, IProductRepository productRepo)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        [HttpGet("rfp/{productId:int}")]
        public async Task<IActionResult> GetReviewsByProductId([FromRoute] int productId)
        {
            var reviews = await _reviewRepo.GetReviewsByProductId(productId);
            if (reviews == null) return NotFound();
            var reviewDtos = _mapper.Map<List<ReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null) return NotFound();
            var reviewDto = _mapper.Map<ReviewDto>(review);
            return Ok(reviewDto);
        }

        [HttpPost("{productId:int}")]
        public async Task<IActionResult> CreateAsync([FromRoute] int productId, [FromBody] ReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _productRepo.ProductExists(productId))
            {
                return BadRequest("Product does not exist!");
            }
            var reviewModel = _mapper.Map<Review>(reviewDto);
            reviewModel.ProductId = productId;

            var reviewCreated = await _reviewRepo.CreateAsync(reviewModel);
            var reviewCreatedDto = _mapper.Map<ReviewDto>(reviewCreated);
            return CreatedAtAction(nameof(GetById), new { id = reviewModel.Id }, reviewCreatedDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var review = await _reviewRepo.DeleteAsync(id);
            if (review == null)
            {
                return NotFound("review does not exist!");
            }

            return Ok(review);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] ReviewUpdateDto reviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewModel = _mapper.Map<Review>(reviewDto);
            var review = await _reviewRepo.UpdateAsync(id, reviewModel);
            if (review == null)
            {
                return NotFound("review not found!");
            }
            return Ok("Sửa thành công!");
        }

    }
}