namespace ShoppingCartSeller.DTO.Discount
{
    public class DiscountDTO
    {
        public string Type { get; set; } // "Flat" or "Percentage"
        public decimal Value { get; set; } // ₹ or %
        public string Description { get; set; }

    }
}
