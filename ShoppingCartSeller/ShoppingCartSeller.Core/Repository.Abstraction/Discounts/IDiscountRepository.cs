using ShoppingCartSeller.Core.Entities.Discounts;

namespace ShoppingCartSeller.Core.Repository.Abstraction.ProductType
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<IDiscount>> GetActiveDiscountsAsync();
        Task SaveDiscountAsync(IDiscount discount);
        Task<IEnumerable<IDiscount>> GetApplicableDiscountsAsync(int productId, DateTime dateTime);
    }
}
