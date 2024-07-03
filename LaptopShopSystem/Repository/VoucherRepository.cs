using System.Net.Http.Headers;
using LaptopShopSystem.Data;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Repository
{
    public class VoucherRepository : IVoucherRepository
    {

        private readonly DataContext _context;
        public VoucherRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Voucher> CreateAsync(Voucher voucher)
        {
            voucher.Code = GenerateVoucherCode();

            if (voucher.StartDate.HasValue && voucher.EndDate.HasValue)
            {
                var currentDate = DateTime.Now;
                Console.WriteLine(currentDate);
                Console.WriteLine(voucher.StartDate.Value+"========="+voucher.EndDate.Value);
                if (currentDate >= voucher.StartDate.Value && currentDate <= voucher.EndDate.Value)
                {
                    voucher.Status = "Active";
                }
                else
                {
                    voucher.Status = "Inactive";
                }
            }
            else
            {
                voucher.Status = "Onactive";
            }

            await _context.Vouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
            return voucher;
        }

        //Chage to remain = 0 and status = inactive
        public async Task<Voucher?> DeleteAsync(int Id)
        {
            var voucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == Id);
            if (voucher == null) return null;
            voucher.Remain = 0;
            voucher.Status = "Inactive";
            await _context.SaveChangesAsync();
            return voucher;
        }

        public async Task<List<Voucher>> GetAllAsync(QueryObjectForVoucher queryObjectForVoucher)
        {
            var vouchers = _context.Vouchers.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(queryObjectForVoucher.VoucherName))
            {
                vouchers = vouchers.Where(s => s.Title.Contains(queryObjectForVoucher.VoucherName));
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(queryObjectForVoucher.SortBy))
            {
                switch (queryObjectForVoucher.SortBy.ToLower())
                {
                    case "discount":
                        vouchers = queryObjectForVoucher.IsDecsending ? vouchers.OrderByDescending(s => s.Discount) : vouchers.OrderBy(s => s.Discount);
                        break;
                    case "enddate":
                        vouchers = queryObjectForVoucher.IsDecsending ? vouchers.OrderByDescending(s => s.EndDate) : vouchers.OrderBy(s => s.EndDate);
                        break;
                    default:
                        vouchers = queryObjectForVoucher.IsDecsending ? vouchers.OrderByDescending(s => s.Title) : vouchers.OrderBy(s => s.Title);
                        break;
                }
            }

            // Pagination
            var skipNumber = (queryObjectForVoucher.PageNumber - 1) * queryObjectForVoucher.PageSize;
            vouchers = vouchers.Skip(skipNumber).Take(queryObjectForVoucher.PageSize);

            return await vouchers.ToListAsync();
        }

        public async Task<Voucher?> GetByIdAsync(int Id)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == Id);

        }

        public async Task<Voucher?> UpdateAsync(int Id, Voucher voucherModel)
        {
            var existingVoucher = await _context.Vouchers.FindAsync(Id);

            if (existingVoucher == null)
            {
                return null;
            }

            existingVoucher.Discount = voucherModel.Discount;
            existingVoucher.Status = voucherModel.Status;
            existingVoucher.EndDate = voucherModel.EndDate;
            existingVoucher.StartDate = voucherModel.StartDate;
            existingVoucher.Title = voucherModel.Title;
            existingVoucher.Total = voucherModel.Total;
            existingVoucher.Remain = voucherModel.Remain;
            existingVoucher.Type = voucherModel.Type;

            await _context.SaveChangesAsync();
            return existingVoucher;
        }

        public Task<bool> VoucherExists(int Id)
        {
            return _context.Vouchers.AnyAsync(x => x.Id == Id);
        }
        private static string GenerateVoucherCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        }
    }
}