namespace ShoppingCartSeller.DTO.Orders 
{ 
    public class OrderOfferDTO
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public string OfferCode { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }

        public OrderDTO Order { get; set; }
    }
}
