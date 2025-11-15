namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class QuantityWiseDiscount : DiscountBase
    {
        public int MinQuantity { get; set; }
        public int DiscountPercent { get; set; }

        public decimal ApplyDiscount(decimal originalPrice, int quantity)
            => quantity >= MinQuantity ? originalPrice * (1 - DiscountPercent / 100m) : originalPrice;

        public override decimal ApplyDiscount(decimal originalPrice) => originalPrice;
    }
}
