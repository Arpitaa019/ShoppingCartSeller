namespace ShoppingCartSeller.DTO.Payments
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } // UPI, Card, COD
        public string Provider { get; set; } // Razorpay, Stripe
        public bool IsActive { get; set; }
        public ICollection<PaymentTransaction> Transactions { get; set; }
    }
}
