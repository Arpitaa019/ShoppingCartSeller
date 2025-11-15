namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderSummaryFilter
    {
        public string Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

}
