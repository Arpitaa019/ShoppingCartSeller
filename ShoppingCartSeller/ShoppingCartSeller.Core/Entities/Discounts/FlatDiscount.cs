namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class FlatDiscount : DiscountBase
    {
        public decimal DiscountAmount { get; set; }
        public override decimal ApplyDiscount(decimal originalPrice)
            => Math.Max(0, originalPrice - DiscountAmount);
    }
}
