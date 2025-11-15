namespace ShoppingCartSeller.DTO.Payments
{
    public class PaymentTransaction
    {
        public Guid Id { get; set; }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public int PaymentMethodId { get; set; }
        public string GatewayTxnId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<PaymentStatusLog> StatusLogs { get; set; }
    }
}
