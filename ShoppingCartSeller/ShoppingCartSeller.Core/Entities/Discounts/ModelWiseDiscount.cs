namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class ModelWiseDiscount : DiscountBase
    {
        public string BrandName { get; set; }
        public string ModelNumber { get; set; }
        public int DiscountPercent { get; set; }
        public override decimal ApplyDiscount(decimal originalPrice)
            => originalPrice * (1 - DiscountPercent / 100m);
    }
}
