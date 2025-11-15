using ShoppingCartSeller.Core.Entities.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSeller.Core.Repository.Abstraction.Sellers
{
    public interface ICompanyRepository
    {
        Task<int> AddCompanyAsync(Company company, int sellerId);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetCompanyBySellerIdAsync(int sellerId);
        Task<bool> UpdateCompanyAsync(Company company, int sellerId);
        Task<bool> DeleteAsync(int sellerId);
    }
}
