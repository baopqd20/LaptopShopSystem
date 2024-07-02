using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromQuery] int userId, [FromBody] OrderDto orderCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_orderRepository.CreateOrder(userId, orderCreate))
            {
                return BadRequest("Something wrong while create order");
            }
            return Ok("Create Success");
        }
    }
}
