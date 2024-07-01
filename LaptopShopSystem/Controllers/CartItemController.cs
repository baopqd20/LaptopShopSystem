using AutoMapper;
using LaptopShopSystem.Data;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.AspNetCore.Mvc;


namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController:Controller
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;
        private readonly DataContext _context;

        public CartItemController(ICartItemRepository cartItemRepository, IMapper mapper, IJwtRepository jwtRepository, DataContext context)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCartItem([FromQuery] int productId, [FromQuery] int cartId, [FromBody] CartItemDto cartItemCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var CartItem = _cartItemRepository.CreateCartItem(productId, cartId, cartItemCreate);
            if (!CartItem)
            {
                return BadRequest("Something wrong while adding product to cart");
            }
            return Ok(CartItem);

        }

        [HttpGet("{cartId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetCartItemInACart(int cartId)
        {
            return Ok(_cartItemRepository);
        }

    }
}
