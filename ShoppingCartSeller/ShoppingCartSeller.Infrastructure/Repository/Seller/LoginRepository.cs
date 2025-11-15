using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ShoppingCartSellerDbContext _context;
        public LoginRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }
        public async Task AddLoginAsync(SellerLogin login, int sellerId)
        {
            login.SellerId = sellerId;
            _context.SellerLogins.Add(login);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SellerLogin>> GetAllAsync()
        {
            return await _context.SellerLogins.ToListAsync();
        }

        public async Task<SellerLogin> GetLoginBySellerIdAsync(int sellerId)
        {
            return await _context.SellerLogins.FirstOrDefaultAsync(l => l.SellerId == sellerId);
        }
        public async Task<bool> UpdateLoginAsync(SellerLogin login, int sellerId)
        {
            login.SellerId = sellerId;
            _context.SellerLogins.Update(login);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
