using ShoppingCartSeller.DTO.Seller;

namespace ShoppingCartSeller.Services.Abstraction.Seller
{
    public interface ISellerDetailsService
    {
        Task<int> CreateSellerAsync(SellerDetailsModel model);
        Task<IEnumerable<SellerDetailsModel>> GetAllSellersAsync();
        Task<SellerDetailsModel> GetSellerByIdAsync(int sellerId);
        Task<bool> UpdateSellerAsync(SellerDetailsModel model);
        Task<bool> DeleteAsync(int sellerId);
    }
}
