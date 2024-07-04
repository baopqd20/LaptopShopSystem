using LaptopShopSystem.Data;
using LaptopShopSystem.Interfaces;

namespace LaptopShopSystem.Helper
{
    public class VoucherExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IServiceProvider _serviceProvider;

        public VoucherExpirationService(IServiceScopeFactory scopeFactory, IServiceProvider serviceProvider)
        {
            _scopeFactory = scopeFactory;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var _context = serviceProvider.GetRequiredService<DataContext>();
                    var _voucherRepo = serviceProvider.GetRequiredService<IVoucherRepository>();

                    var currentTime = DateTime.Now;
                    var expiredVoucher = _context.Vouchers
                        .Where(v => v.EndDate <= currentTime && v.Status == "Active")
                        .ToList();

                    foreach (var voucher in expiredVoucher)
                    {
                        await _voucherRepo.DeleteAsync(voucher.Id);
                    }
                    Console.WriteLine("Update Voucher success!");
                }
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Chạy mỗi 1 phút
            }
        }
    }
}