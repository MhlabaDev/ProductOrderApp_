using Microsoft.EntityFrameworkCore;
using ProductOrderApi.Models;

namespace ProductOrderApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for each model
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for Product.Price and OrderItem.UnitPrice
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");

            // Optional: Seed 10 sample products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Title = "Widget A", Description = "Small widget", Price = 9.99m, ImageUrl = "/images/widget-a.jpg" },
                new Product { ProductId = 2, Title = "Widget B", Description = "Medium widget", Price = 19.99m, ImageUrl = "/images/widget-b.jpg" },
                new Product { ProductId = 3, Title = "Widget C", Description = "Large widget", Price = 29.99m, ImageUrl = "/images/widget-c.jpg" },
                new Product { ProductId = 4, Title = "Gadget A", Description = "Useful gadget", Price = 14.99m, ImageUrl = "/images/gadget-a.jpg" },
                new Product { ProductId = 5, Title = "Gadget B", Description = "Advanced gadget", Price = 24.99m, ImageUrl = "/images/gadget-b.jpg" },
                new Product { ProductId = 6, Title = "Tool A", Description = "Handy tool", Price = 12.99m, ImageUrl = "/images/tool-a.jpg" },
                new Product { ProductId = 7, Title = "Tool B", Description = "Professional tool", Price = 34.99m, ImageUrl = "/images/tool-b.jpg" },
                new Product { ProductId = 8, Title = "Accessory A", Description = "Cool accessory", Price = 7.99m, ImageUrl = "/images/accessory-a.jpg" },
                new Product { ProductId = 9, Title = "Accessory B", Description = "Premium accessory", Price = 17.99m, ImageUrl = "/images/accessory-b.jpg" },
                new Product { ProductId = 10, Title = "Accessory C", Description = "Luxury accessory", Price = 27.99m, ImageUrl = "/images/accessory-c.jpg" }
            );
        }
    }
}
