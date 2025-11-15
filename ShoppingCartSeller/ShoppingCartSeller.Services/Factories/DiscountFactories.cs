using ShoppingCartSeller.Core.Entities.Discounts;

namespace ShoppingCartSeller.Service.Factories
{

    public abstract class DiscountFactory
    {
        public abstract IDiscount CreateDiscount();
    }

    public class BrandDiscountFactory : DiscountFactory
    {
        public override IDiscount CreateDiscount()
        {
            return new BrandWiseDiscount
            {
                DiscountName = "Brand Bonanza",
                BrandName = "Nike",
                DiscountPercent = 15,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(10),
                IsActive = true
            };
        }


    }
}
