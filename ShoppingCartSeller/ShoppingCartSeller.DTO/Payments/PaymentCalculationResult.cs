namespace ShoppingCartSeller.DTO.Payments
{
    public class PaymentCalculationResult
    {
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public string PaymentOption { get; set; }
    }
}
