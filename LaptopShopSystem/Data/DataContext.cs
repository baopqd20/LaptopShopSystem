using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(k => new { k.ProductId, k.CategoryId });
            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Wishlist>()
                .HasKey(k => new {k.ProductId, k.UserId});
            modelBuilder.Entity<Wishlist>()
                .HasOne(p => p.User)
                .WithMany(pc => pc.Wishlists)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Wishlist>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.Wishlists)
                .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<ProductDetails>()
                .HasKey(k => k.ProductId);
            modelBuilder.Entity<Cart>()
                .HasKey(k => k.UserId);
           
        }

    }
}
