using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.Core.Entities.Orders
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public string PaymentTxnId { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
