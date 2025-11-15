using ShoppingCartSeller.Core.Entities.Customers;
using ShoppingCartSeller.Core.Entities.Orders;
using ShoppingCartSeller.Core.Entities.Discounts;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Entities.Notifications;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartSeller.Infrastructure.Sql
{
 public class ShoppingCartSellerDbContext : DbContext
 {
 public ShoppingCartSellerDbContext(DbContextOptions<ShoppingCartSellerDbContext> options)
 : base(options)
 {
 }

 public DbSet<Customer> Customers { get; set; }
 public DbSet<CustomerInteraction> CustomerInteractions { get; set; }
 public DbSet<Order> Orders { get; set; }
 public DbSet<OrderItem> OrderItems { get; set; }
 public DbSet<DiscountBase> Discounts { get; set; }
 public DbSet<CouponWiseDiscount> CouponWiseDiscounts { get; set; }
 public DbSet<SellerWiseDiscount> SellerWiseDiscounts { get; set; }
 public DbSet<Company> Companies { get; set; }
 public DbSet<SellerDetails> SellerDetails { get; set; }
 public DbSet<SellerLogin> SellerLogins { get; set; }
 public DbSet<SellerNotification> SellerNotifications { get; set; }

 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
 base.OnModelCreating(modelBuilder);
 modelBuilder.Entity<DiscountBase>()
 .HasDiscriminator<string>("DiscountType")
 .HasValue<CouponWiseDiscount>("Coupon")
 .HasValue<SellerWiseDiscount>("Seller");
 }
 }
}
