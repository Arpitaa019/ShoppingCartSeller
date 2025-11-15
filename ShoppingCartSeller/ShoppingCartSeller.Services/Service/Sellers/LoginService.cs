using ShoppingCartSeller.Core.Entities;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.DTO.Seller;
using ShoppingCartSeller.Infrastructure.Repository;
using ShoppingCartSeller.Services.Abstraction.Seller;
using System;


namespace ShoppingCartSeller.Services.Service.Sellers
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;
        public LoginService(ILoginRepository repo)
        {
            _repo = repo;
        }
        public async Task CreateLoginAsync(LoginModel model, int sellerId)
        {

            var loginEntity = new SellerLogin
            {
                SellerId = sellerId,
                Email = model.Email,
                Password = model.Password

            };
            await _repo.AddLoginAsync(loginEntity, sellerId);

        }

        public async Task<IEnumerable<LoginModel>> GetAllLoginAsync()
        {
            var loginEntities = await _repo.GetAllAsync();

            var loginModels = loginEntities.Select(login => new LoginModel
            {
                LoginId = login.LoginId,
                SellerId = login.SellerId,
                Email = login.Email,
                Password = login.Password
            });

            return loginModels;
        }
        public Task<LoginModel> GetLoginBySellerIdAsync(int sellerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateLoginAsync(LoginModel model, int sellerId)
        {
            var entity = new SellerLogin
            {
                LoginId = model.LoginId,
                SellerId = model.SellerId,
                Email = model.Email,
                Password = model.Password
            };

            return await _repo.UpdateLoginAsync(entity, sellerId);
        }
    }
}
