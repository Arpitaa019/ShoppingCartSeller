namespace ShoppingCartSeller.DTO.Discount
{
    public class DiscountRuleDTO
    {

        public string Type { get; set; } // "Product", "Amount", "PaymentMethod"
        public int? ProductId { get; set; } // For product-based discounts
        public decimal? MinAmount { get; set; } // For cart total threshold
        public string PaymentMethod { get; set; } // For method-specific discounts
        public string DiscountMode { get; set; } // "Flat" or "Percentage"
        public decimal Value { get; set; } // ₹ or %
        public string Description { get; set; }
    }
}
