namespace ShoppingCartSeller.DTO.Delivery
{
    public class ReturnRequestDTO
    {
        public Guid OrderId { get; set; }
        public string Reason { get; set; }
        public bool IsExchange { get; set; }
    }
}
