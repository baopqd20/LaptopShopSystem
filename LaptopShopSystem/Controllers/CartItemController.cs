using AutoMapper;
using LaptopShopSystem.Data;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : Controller
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
            if (cartItemCreate.Quantity == 0)
            {
                return BadRequest("Invalid Quantity");
            }
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
            return Ok(JsonConvert.SerializeObject(_cartItemRepository.GetCartItemsByCartId(cartId), Formatting.Indented));
        }


        [HttpPut("{cartId:int}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] int cartId, [FromBody] CartItemUpdateDto cartItemUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cartItemModel = _mapper.Map<CartItem>(cartItemUpdateDto);
            var updatedCartItem = await _cartItemRepository.UpdateCartItem(cartId, cartItemModel);

            if (updatedCartItem == null)
            {
                return BadRequest("Something went wrong while updating cart item");
            }

            return Ok(updatedCartItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCartItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null) return NotFound("cart item not found!");
            if (!_cartItemRepository.DeleteCartItem(cartItem))
            {
                return BadRequest("Something went wrong while delete cart item");
            }
            return Ok(cartItem);
        }
    }
}
