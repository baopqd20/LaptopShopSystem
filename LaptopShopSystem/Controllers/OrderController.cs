using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> CreateOrder([FromQuery] int userId, [FromBody] OrderDto orderCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var status = await _orderRepository.CreateOrder(userId, orderCreate);
            if (status == 0)
            {
                return BadRequest("Empty Cart");
            }
            if (status == 2)
            {
                return BadRequest("Not enough product quantity to order");
            }
            if (status == 3)
            {
                return BadRequest("Voucher is invalid or has expired!");
            }
            if (status != 1)
            {
                return BadRequest("Something wrong");
            }

            return Ok("Create Success");
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetOrderByUserId(int userId)
        {
            var order = _orderRepository.GetOrderByUserId(userId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet("order/{orderId}")]
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

        [HttpPut("cancel/{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CancelOrder(int orderId)
        {

            if (!_orderRepository.CancelOrder(orderId))
            {
                return BadRequest("Something went wrong while cancel order");
            }
            return Ok("Cancel order success");
        }
    }
}
