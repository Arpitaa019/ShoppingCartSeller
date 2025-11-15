namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderTrackingDTO
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public string Courier { get; set; }
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public OrderDTO Order { get; set; }
    }
}
