using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Discounts;
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Discount
{

    public class DiscountRepository : IDiscountRepository
    {
        private readonly ShoppingCartSellerDbContext _context;
        public DiscountRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IDiscount>> GetActiveDiscountsAsync()
        {
            return await _context.Discounts.Where(d => d.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<IDiscount>> GetApplicableDiscountsAsync(int productId, DateTime currentDate)
        {
            return await _context.Discounts.Where(d => d.IsActive && d.IsApplicable(currentDate)).ToListAsync();
        }

        public async Task SaveDiscountAsync(IDiscount discount)
        {
            _context.Discounts.Add((DiscountBase)discount);
            await _context.SaveChangesAsync();
        }
    }
}
