
using LaptopShopSystem.Helper;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IVoucherRepository
    {
        Task<List<Voucher>> GetAllAsync(QueryObjectForVoucher queryObjectForVoucher );
        Task<Voucher?> GetByIdAsync(int Id);
        Task<Voucher> CreateAsync(Voucher voucher);
        Task<Voucher?> UpdateAsync(int Id, Voucher voucherModel);
        Task<Voucher?> DeleteAsync(int Id);
        Task<bool> VoucherExists(int Id);
    }
}