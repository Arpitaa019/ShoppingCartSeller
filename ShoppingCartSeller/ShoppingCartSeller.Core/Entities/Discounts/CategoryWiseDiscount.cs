

namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class CategoryWiseDiscount : DiscountBase
    {
        public string CategoryName { get; set; }
        public int DiscountPercent { get; set; }
        public override decimal ApplyDiscount(decimal originalPrice)
            => originalPrice * (1 - DiscountPercent / 100m);
    }
}
