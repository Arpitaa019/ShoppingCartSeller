namespace ShoppingCartSeller.DTO.Payments
{
    public class PaymentStatusLog
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public string Status { get; set; }
        public DateTime ChangedAt { get; set; }
        public string Remarks { get; set; }
        public PaymentTransaction Transaction { get; set; }
    }
}
