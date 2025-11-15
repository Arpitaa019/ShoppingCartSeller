namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderReviewDTO
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public string ProductId { get; set; }
        public int Rating { get; set; } // 1–5
        public string Comment { get; set; }
        public DateTime ReviewedAt { get; set; }
        public OrderDTO Order { get; set; }
    }

}
