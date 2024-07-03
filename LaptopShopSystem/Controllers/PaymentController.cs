using LaptopShopSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController:Controller
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpPost("{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreatePayment(int orderId)
        {
            if (!_paymentRepository.CreatePaymentOfOrder(orderId))
            {
                return BadRequest("Something went wrong while payment");
            };
            return Ok("Paid Success");
        }

        [HttpGet("/userpayment/{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetPaymentByUserId(int userId)
        {
            var payment = _paymentRepository.GetPaymentByUserId(userId);
            if (payment == null)
            {
                return NotFound("User Payment not found");
            }
            return Ok(payment);
        }
        [HttpGet("/orderpayment/{orderId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetPaymentByOrderId(int orderId)
        {
            var payment = _paymentRepository.GetPaymentByOrderId(orderId);
            if (payment == null)
            {
                return NotFound("Order Payment not found");
            }
            return Ok(payment);
        }
    }
}
