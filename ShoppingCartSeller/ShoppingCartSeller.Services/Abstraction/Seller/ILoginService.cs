using ShoppingCartSeller.DTO.Seller;
using System;

namespace ShoppingCartSeller.Services.Abstraction.Seller
{
    public interface ILoginService
    {
        Task CreateLoginAsync(LoginModel model, int sellerId);
        Task<IEnumerable<LoginModel>> GetAllLoginAsync();
        Task<LoginModel> GetLoginBySellerIdAsync(int sellerId);
        Task<bool> UpdateLoginAsync(LoginModel model, int sellerId);
    }

}
