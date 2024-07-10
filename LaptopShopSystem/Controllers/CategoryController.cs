using AutoMapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, IJwtRepository jwtRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [Authorize]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
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
            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                return BadRequest("Something wrong while creating");
            }
            return Ok(categoryMap);
        }
        [HttpPut("{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryUpdate)
        {
            var isAdmin = User.IsInRole("admin");
            if (!isAdmin)
            {
                return Forbid();
            }
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userDb = _categoryRepository.GetCategoryById(categoryId);
            var userMap = _mapper.Map(categoryUpdate, userDb);
            if (!_categoryRepository.UpdateCategory(userMap))
            {
                return BadRequest("Something went wrong while update category");
            }
            return Ok(userMap);

        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult DeletaCategory(int categoryId)
        {
            var isAdmin = User.IsInRole("admin");
            if (!isAdmin)
            {
                return Forbid();
            }
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }

            var userDb = _categoryRepository.GetCategoryById(categoryId);
            if (!_categoryRepository.UpdateCategory(userDb))
            {
                return BadRequest("Something went wrong while update category");
            }
            return Ok("Delete Success");

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
    }
}

