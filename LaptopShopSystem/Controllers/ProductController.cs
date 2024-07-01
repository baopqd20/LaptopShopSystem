
using LaptopShopSystem.Data;
using LaptopShopSystem.Dto.Product;
using Microsoft.AspNetCore.Mvc;
using LaptopShopSystem.Models;
using LaptopShopSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using LaptopShopSystem.Mapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Helper;
namespace LaptopShopSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;
        public ProductController(DataContext context, IProductRepository productRepo, IMapper mapper, IJwtRepository jwtRepository)
        {
            _context = context;
            _productRepo = productRepo;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            // var isAdmin = User.IsInRole("admin");
            // if (!isAdmin)
            // {
            //     return Forbid();
            // }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brand = await _context.Brands.FindAsync(productDto.BrandId);
            if (brand == null)
            {
                return BadRequest("Invalid BrandId");
            }

            var product = productDto.ToProductModel();

            _context.ProductDetails.Add(product.Details);
            await _productRepo.CreateAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            var product = await _context.Products
         .Include(p => p.Details)
         .Include(p => p.Brand)
         .Include(p => p.ProductCategories)
         .ThenInclude(pc => pc.Category)
         .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            var productDto = product.ToProdResponse();
            return Ok(productDto);
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var products = await _productRepo.GetProducts(queryObject);
            var productDtos = products.Select(p => p.ToProdResponse());
            return Ok(productDtos);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok("done");
        }
    }
}