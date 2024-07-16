using LaptopShopSystem.Data;
using LaptopShopSystem.Models;
using LaptopShopSystem.Repository;
using Microsoft.EntityFrameworkCore;


namespace LaptopShopSystemTest
{
    public class BrandRepositoryTests
    {
        private DataContext _context;
        private BrandRepository _repository;

        public BrandRepositoryTests()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "LaptopShopSystem")
                .Options;

            _context = new DataContext(options);
            _context.Database.EnsureDeleted(); // Ensure the database is empty
            _context.Database.EnsureCreated();

            // Seed the in-memory database with test data
            _context.Brands.AddRange(new List<Brand>
        {
            new Brand { Id = 1, Name = "Brand1" },
            new Brand { Id = 2, Name = "Brand2" },
            new Brand { Id = 3, Name = "Brand3" }
        });

            _context.SaveChanges();

            _repository = new BrandRepository(_context);
        }

        [Fact]
        public async Task GetBrands_ReturnsAllBrands()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var result = await _repository.GetBrands();

            Assert.Equal(3, result.Count);
            Assert.Equal("Brand1", result[0].Name);
            Assert.Equal("Brand2", result[1].Name);
            Assert.Equal("Brand3", result[2].Name);
        }

        [Fact]
        public void GetBrandById_ReturnsCorrectBrand()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var result = _repository.GetBrandById(1);

            Assert.NotNull(result);
            Assert.Equal("Brand1", result.Name);
        }

        [Fact]
        public void BrandExists_ReturnsTrueIfBrandExists()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var result = _repository.BrandExists(1);

            Assert.True(result);
        }

        [Fact]
        public void BrandExists_ReturnsFalseIfBrandDoesNotExist()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var result = _repository.BrandExists(4);

            Assert.False(result);
        }

        [Fact]
        public void CreateBrand_AddsBrand()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var newBrand = new Brand { Id = 4, Name = "Brand4" };

            _repository.CreateBrand(newBrand);

            var result = _context.Brands.Find(4);
            Assert.NotNull(result);
            Assert.Equal("Brand4", result.Name);
        }

        [Fact]
        public void DeleteBrand_RemovesBrand()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var brand = _context.Brands.Find(1);

            _repository.DeleteBrand(brand);

            var result = _context.Brands.Find(1);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateBrand_UpdatesBrand()
        {
            InitializeDatabase(); // Reinitialize database for each test
            var brand = _context.Brands.Find(1);
            brand.Name = "UpdatedBrand";

            _repository.UpdateBrand(brand);

            var result = _context.Brands.Find(1);
            Assert.Equal("UpdatedBrand", result.Name);
        }
    }
}
