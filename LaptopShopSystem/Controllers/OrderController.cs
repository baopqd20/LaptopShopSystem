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

        [HttpGet("/user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetOrderByUserId(int userId)
        {
            var order = _orderRepository.GetOrderByUserId(userId);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("/order/{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetOrderByOrderId(int orderId)
        {
            var order = _orderRepository.GetOrderByOrderId(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

    }
}
