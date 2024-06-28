using LaptopShopSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController:Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IJwtRepository _jwtRepository;

        public CartController(ICartRepository cartRepository, IJwtRepository jwtRepository)
        {
            _cartRepository = cartRepository;
            _jwtRepository = jwtRepository;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetCart(int userId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var isAdmin = User.IsInRole("admin");

            if (!isAdmin && (userIdClaim == null || int.Parse(userIdClaim) != userId))
            {
                return Forbid(); // Trả về 403 nếu không phải chính user
            }
            if (_cartRepository.GetCartByUserId(userId) == null)
            {
                return NotFound();
            }
            return Ok(_cartRepository.GetCartByUserId(userId));
        }
    }
}
