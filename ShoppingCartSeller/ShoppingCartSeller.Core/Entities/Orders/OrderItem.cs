using ShoppingCartSeller.Core.Entities.Customers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCartSeller.Core.Entities.Orders
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Seller { get; set; }
        public int DeliveryStatus { get; set; }
        public int InventoryID { get; set; }
        public Order Order { get; set; }
    }
}
