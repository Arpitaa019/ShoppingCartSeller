using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Customers;
using ShoppingCartSeller.Core.Entities.Notifications;
using ShoppingCartSeller.Core.Entities.Orders;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.DTO.Customers;

namespace ShoppingCartSeller.Infrastructure.Sql
{
 public class ShoppingCartSellerDbContext : DbContext
 {
 public ShoppingCartSellerDbContext(DbContextOptions<ShoppingCartSellerDbContext> options)
 : base(options)
 {
 }


 public DbSet<CustomerInteraction> CustomerInteractions { get; set; }
 public DbSet<Order> Orders { get; set; }
 public DbSet<OrderItem> OrderItems { get; set; }
 public DbSet<Company> Companies { get; set; }
 public DbSet<SellerDetails> SellerDetails { get; set; }
 public DbSet<SellerLogin> SellerLogins { get; set; }
 public DbSet<SellerNotification> SellerNotifications { get; set; }

 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
 
 


            base.OnModelCreating(modelBuilder);
 
 }
 }
}
