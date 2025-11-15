
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.Service.Factories;

namespace ShoppingCartSeller.Services.Service.Discount
{
    public class DiscountManager
    {
        private readonly IDiscountRepository _repository;
        private readonly IEnumerable<DiscountFactory> _fallbackFactories;

        public DiscountManager(IDiscountRepository repository, IEnumerable<DiscountFactory> fallbackFactories)
        {
            _repository = repository;
            _fallbackFactories = fallbackFactories;
        }

        public async Task<decimal> GetFinalPriceAsync(int productId, decimal originalPrice, DateTime currentDate)
        {
            var discounts = await _repository.GetApplicableDiscountsAsync(productId, currentDate);

            if (!discounts.Any())
            {
                discounts = _fallbackFactories
                    .Select(f => f.CreateDiscount())
                    .Where(d => d.IsApplicable(currentDate))
                    .ToList();
            }

            decimal finalPrice = originalPrice;
            foreach (var discount in discounts)
            {
                finalPrice = discount.ApplyDiscount(finalPrice);
            }

            return finalPrice;
        }
    }
}
