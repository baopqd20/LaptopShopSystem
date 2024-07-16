using LaptopShopSystem.Data;
using LaptopShopSystem.Helper;
using LaptopShopSystem.Models;
using LaptopShopSystem.Repository;
using Microsoft.EntityFrameworkCore;


namespace LaptopShopSystemTest.RepositoryTest
{
    public class VoucherRepositoryTests
    {
        private readonly DbContextOptions<DataContext> _options;

        public VoucherRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
        private readonly Voucher newVoucher = new()
        {
            Title = "Test Voucher",
            Discount = 10,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            Total = 100,
            Remain = 100,
            Type = "Percentage",
            Status = "Active",
            Code = "ABC123"
        };

        [Fact]
        public async Task CreateAsync_ShouldCreateNewVoucher()
        {

            using var context = new DataContext(_options);
            var repository = new VoucherRepository(context);



            var result = await repository.CreateAsync(newVoucher);


            Assert.NotNull(result);
            Assert.Equal("Active", result.Status);
            Assert.NotNull(result.Code);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteExistingVoucher()
        {

            using var context = new DataContext(_options);
            var repository = new VoucherRepository(context);
            var existingVoucher = await repository.CreateAsync(newVoucher);


            var result = await repository.DeleteAsync(existingVoucher.Id);


            Assert.NotNull(result);
            Assert.Equal("Inactive", result.Status); 
        }

    
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllVouchers()
        {

            using var context = new DataContext(_options);
            var repository = new VoucherRepository(context);


            var result = await repository.GetAllAsync(new QueryObjectForVoucher());


            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnVoucherById()
        {

            using var context = new DataContext(_options);
            var repository = new VoucherRepository(context);
            var existingVoucher = await repository.CreateAsync(newVoucher);


            var result = await repository.GetByIdAsync(existingVoucher.Id);


            Assert.NotNull(result);
            Assert.Equal("Test Voucher", result.Title);
        }

      
        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingVoucher()
        {

            using var context = new DataContext(_options);
            var repository = new VoucherRepository(context);
            var existingVoucher = await repository.CreateAsync(newVoucher);

            var updatedVoucher = new Voucher
            {
                Id = existingVoucher.Id,
                Title = "Updated Voucher",
                Discount = 20,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                Total = 200,
                Remain = 200,
                Type = "FixedAmount",
                Status = "InActive",
                Code = "CBD123"
            };


            var result = await repository.UpdateAsync(existingVoucher.Id, updatedVoucher);


            Assert.NotNull(result);
            Assert.Equal("Updated Voucher", result.Title);
            Assert.Equal(20, result.Discount);
        }

     
        [Fact]
        public async Task VoucherExists_ShouldReturnTrueIfVoucherExists()
        {

            using var context = new DataContext(_options);
            var repository = new VoucherRepository(context);
            var existingVoucher = await repository.CreateAsync(newVoucher);


            var result = await repository.VoucherExists(existingVoucher.Id);


            Assert.True(result);
        }

    }
}