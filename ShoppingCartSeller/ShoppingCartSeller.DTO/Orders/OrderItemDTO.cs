
using ShoppingCartSeller.DTO.Delivery;

namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Seller { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int InventoryID { get; set; }
    }
}
