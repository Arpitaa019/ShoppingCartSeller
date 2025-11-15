using ShoppingCartSeller.Core.Entities.Sellers;

namespace ShoppingCartSeller.Core.Repository.Abstraction.Sellers
{
    public interface ISellerDetailsRepository
    {
        Task<int> AddSellerDetailsAsync(SellerDetails seller);
        Task<IEnumerable<SellerDetails>> GetAllSellerAsync();
        Task<SellerDetails> GetSellerByIdAsync(int sellerId);
        Task<bool> UpdateSellerAsync(SellerDetails model);
        Task<bool> DeleteSellerAsync(int sellerId);
    }
}
