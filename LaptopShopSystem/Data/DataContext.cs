using LaptopShopSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LaptopShopSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductDetails> ProductDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
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
                .HasKey(k => new { k.ProductId, k.UserId });
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
