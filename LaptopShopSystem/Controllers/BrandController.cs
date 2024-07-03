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
    public class BrandController : Controller
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;

        public BrandController(IBrandRepository brandRepository, IMapper mapper, IJwtRepository jwtRepository)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        // [Authorize]
        public IActionResult CreateBrand([FromBody] BrandDto brandCreate)
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
            var brandMap = _mapper.Map<Brand>(brandCreate);
            if (!_brandRepository.CreateBrand(brandMap))
            {
                return BadRequest("Something wrong while creating");
            }
            return Ok(brandMap);
        }
        [HttpPut("{brandId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult UpdateBrand(int brandId, [FromBody] BrandDto brandUpdate)
        {
            var isAdmin = User.IsInRole("admin");
            if (!isAdmin)
            {
                return Forbid();
            }
            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userDb = _brandRepository.GetBrandById(brandId);
            var userMap = _mapper.Map(brandUpdate, userDb);
            if (!_brandRepository.UpdateBrand(userMap))
            {
                return BadRequest("Something went wrong while update brand");
            }
            return Ok(userMap);

        }

        [HttpDelete("{brandId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult DeletaBrand(int brandId)
        {
            var isAdmin = User.IsInRole("admin");
            if (!isAdmin)
            {
                return Forbid();
            }
            if (!_brandRepository.BrandExists(brandId))
            {
                return NotFound();
            }

            var userDb = _brandRepository.GetBrandById(brandId);
            if (!_brandRepository.UpdateBrand(userDb))
            {
                return BadRequest("Something went wrong while update brand");
            }
            return Ok("Delete Success");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var brands = await _brandRepository.GetBrands();
            var brandDtos = _mapper.Map<List<BrandDto>>(brands);
            return Ok(brandDtos);
        }
    }
}
