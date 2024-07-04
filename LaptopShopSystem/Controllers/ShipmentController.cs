using LaptopShopSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController:Controller
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentController(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        /*[HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Test()
        {
            try
            {
                var distance = await _shipmentRepository.CreateShipment();
                return Ok(distance); // Trả về kết quả dưới dạng JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Xử lý lỗi nếu có
            }
        }*/
    }
}
