namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderStatusLogDTO
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatusDTO Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public string Remarks { get; set; }
        public OrderDTO Order { get; set; }
    }
}
