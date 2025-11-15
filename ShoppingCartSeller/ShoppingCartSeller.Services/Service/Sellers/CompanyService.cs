using ShoppingCartSeller.Core.Entities;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.DTO.Seller;
using ShoppingCartSeller.Services.Abstraction.Seller;


namespace ShoppingCartSeller.Services.Service.Sellers
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        public CompanyService(ICompanyRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> CreateCompanyAsync(CompanyModel company, int sellerId)
        {
            var companyEntity = new Company
            {
                SellerId = sellerId,
                Name = company.Name,
                GSTIN = company.GSTIN,
                City = company.City,
                State = company.State,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "System",
                IsActive = true
            };
            return await _repo.AddCompanyAsync(companyEntity, sellerId);
        }

        public async Task<bool> DeleteCompanyBySellerIdAsync(int sellerId)
        {
            return await _repo.DeleteAsync(sellerId);
        }

        public async Task<IEnumerable<CompanyModel>> GetAllAsync()
        {
            var entity = await _repo.GetAllAsync();
            var companyModels = entity.Select(c => new CompanyModel
            {
                CompanyId = c.CompanyId,
                SellerId = c.SellerId,
                Name = c.Name,
                GSTIN = c.GSTIN,
                City = c.City,
                State = c.State

            }).ToList();
            return companyModels;
        }

        public async Task<CompanyModel> GetCompanyBySellerIdAsync(int sellerId)
        {
            //   var company = await _repo.GetAllAsync(sellerId);
            return null;
        }

        public async Task<bool> UpdateCompanyAsync(CompanyModel company, int sellerId)
        {
            var entity = new Company
            {
                CompanyId = company.CompanyId,
                SellerId = company.SellerId,
                Name = company.Name,
                GSTIN = company.GSTIN,
                City = company.City,
                State = company.State
            };

            return await _repo.UpdateCompanyAsync(entity, sellerId);
        }
    }
}
