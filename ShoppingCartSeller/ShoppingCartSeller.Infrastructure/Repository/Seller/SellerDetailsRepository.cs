using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class SellerDetailsRepository : ISellerDetailsRepository
    {
        private readonly ShoppingCartSellerDbContext _context;
        public SellerDetailsRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddSellerDetailsAsync(SellerDetails seller)
        {
            _context.SellerDetails.Add(seller);
            await _context.SaveChangesAsync();
            return seller.SellerId;
        }

        public async Task<bool> DeleteSellerAsync(int sellerId)
        {
            var seller = await _context.SellerDetails.FindAsync(sellerId);
            if (seller == null) return false;
            _context.SellerDetails.Remove(seller);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SellerDetails>> GetAllSellerAsync()
        {
            return await _context.SellerDetails.ToListAsync();
        }

        public async Task<SellerDetails> GetSellerByIdAsync(int sellerId)
        {
            return await _context.SellerDetails.FindAsync(sellerId);
        }
        public async Task<bool> UpdateSellerAsync(SellerDetails seller)
        {
            _context.SellerDetails.Update(seller);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
