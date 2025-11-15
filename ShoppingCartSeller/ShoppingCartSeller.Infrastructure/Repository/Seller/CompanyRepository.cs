using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ShoppingCartSellerDbContext _context;
        public CompanyRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCompanyAsync(Company company, int sellerId)
        {
            company.SellerId = sellerId;
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company.CompanyId;
        }

        public async Task<bool> DeleteAsync(int sellerId)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.SellerId == sellerId);
            if (company == null) return false;
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyBySellerIdAsync(int sellerId)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.SellerId == sellerId);
        }

        public async Task<bool> UpdateCompanyAsync(Company company, int sellerId)
        {
            company.SellerId = sellerId;
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
