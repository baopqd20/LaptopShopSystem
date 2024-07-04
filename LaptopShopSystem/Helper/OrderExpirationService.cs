using LaptopShopSystem.Data;

namespace LaptopShopSystem.Helper
{
    public class OrderExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<DataContext>();

                    var currentTime = DateTime.Now;
                    var expiredOrders = _context.Orders
                        .Where(order => order.ExpireTime <= currentTime && order.Status == "Pending")
                        .ToList();

                    foreach (var order in expiredOrders)
                    {
                        var orderItems = _context.OrderItems.Where(p => p.OrderId == order.Id).ToList();
                        foreach (var orderitem in orderItems)
                        {
                            var product = _context.Products.Where(p => p.Id == orderitem.ProductId).FirstOrDefault();
                            product.Remain = product.Remain + orderitem.Quantity;
                            product.Total = product.Total - orderitem.Quantity;
                            _context.Update(product);
                        }
                        order.Status = "Expired";
                        _context.Orders.Update(order);
                    }
                    await _context.SaveChangesAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Chạy mỗi 1 phút
            }
        }
    }
}
