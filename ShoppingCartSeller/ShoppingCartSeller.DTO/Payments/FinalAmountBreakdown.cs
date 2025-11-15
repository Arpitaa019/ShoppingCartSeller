namespace ShoppingCartSeller.DTO.Payments
{
    public class FinalAmountBreakdown
    {
        public decimal BaseAmount { get; set; }
        public decimal PlatformFee { get; set; }
        public decimal PaymentCharge { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CashbackAmount { get; set; }
        public decimal OtherChargesTotal { get; set; }
        public decimal FinalAmount { get; set; }
        public string Summary { get; set; }

    }
}
