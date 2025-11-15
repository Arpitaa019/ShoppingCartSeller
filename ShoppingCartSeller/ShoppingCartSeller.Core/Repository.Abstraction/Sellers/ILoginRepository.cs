using ShoppingCartSeller.Core.Entities.Sellers;

namespace ShoppingCartSeller.Core.Repository.Abstraction.Sellers
{
    public interface ILoginRepository
    {
        Task AddLoginAsync(SellerLogin login, int sellerId);
        Task<IEnumerable<SellerLogin>> GetAllAsync();
        Task<SellerLogin> GetLoginBySellerIdAsync(int sellerId);
        Task<bool> UpdateLoginAsync(SellerLogin login, int sellerId);
    }
}
