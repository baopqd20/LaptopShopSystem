using AutoMapper;
using LaptopShopSystem.Dto.Voucher;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {

        private readonly IVoucherRepository _voucherRepo;
        private readonly IMapper _mapper;
        public VoucherController(IVoucherRepository voucherRepo, IMapper mapper)
        {
            _voucherRepo = voucherRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVoucher([FromQuery] QueryObjectForVoucher queryObjectForVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vouchers = await _voucherRepo.GetAllAsync(queryObjectForVoucher);
           
            return Ok(vouchers);
        }


        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucher = await _voucherRepo.GetByIdAsync(Id);
            if (voucher == null) return NotFound();
            var voucherDto = _mapper.Map<VoucherDto>(voucher);
            return Ok(voucherDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromBody] VoucherCreateDto voucherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucherModel = _mapper.Map<Voucher>(voucherDto);
            var voucherCreated = await _voucherRepo.CreateAsync(voucherModel);
            var voucherCreatedDto = _mapper.Map<VoucherDto>(voucherCreated);
            return CreatedAtAction(nameof(GetById), new { id = voucherModel.Id }, voucherCreatedDto);
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateVoucher([FromRoute] int Id, [FromBody] VoucherUpdateDto voucherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucherModel = _mapper.Map<Voucher>(voucherDto);
            var voucher = await _voucherRepo.UpdateAsync(Id, voucherModel);
            if (voucher == null) return NotFound();
            return Ok("Update voucher success!");
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> CancelVoucher([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucher = await _voucherRepo.DeleteAsync(Id);
            if (voucher == null) return NotFound();
            return Ok("Voucher was cancel!");
        }
    }
}