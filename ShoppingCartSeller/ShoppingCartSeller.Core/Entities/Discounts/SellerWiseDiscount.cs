namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class SellerWiseDiscount : DiscountBase
    {
        public int SellerID { get; set; }
        public int DiscountPercent { get; set; }
        public override decimal ApplyDiscount(decimal originalPrice)
            => originalPrice * (1 - DiscountPercent / 100m);
    }
}
