

namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class CouponWiseDiscount : DiscountBase
    {
        public string CouponCode { get; set; }
        public int DiscountPercent { get; set; }
        public override decimal ApplyDiscount(decimal originalPrice)
            => originalPrice * (1 - DiscountPercent / 100m);
    }
}
