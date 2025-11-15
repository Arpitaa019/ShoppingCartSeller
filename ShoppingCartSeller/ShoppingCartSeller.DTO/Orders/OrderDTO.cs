namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatusDTO Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        //public DateTime ExpectedDelivery { get; set; }
        public string PaymentTxnId { get; set; }

        public List<OrderItemDTO> items { get; set; } = new List<OrderItemDTO>();
        public OrderAddressDTO Address { get; set; }
        public List<OrderStatusLogDTO> StatusLogs { get; set; } = new List<OrderStatusLogDTO>();
        public List<OrderOfferDTO> Offers { get; set; } = new List<OrderOfferDTO>();

        //public List<OrderReviewDTO> Reviews { get; set; } = new List<OrderReviewDTO>();
        public List<OrderTrackingDTO> TrackingEvents { get; set; } = new List<OrderTrackingDTO>();
    }
}
