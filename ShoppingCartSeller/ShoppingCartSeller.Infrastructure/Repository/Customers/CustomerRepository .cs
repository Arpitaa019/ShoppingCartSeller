using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Customers;
using ShoppingCartSeller.Core.Repository.Abstraction.Customers;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;

namespace ShoppingCartSeller.Infrastructure.Repository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShoppingCartSellerDbContext _context;
        public CustomerRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerInteraction>> GetAllInteractionsAsync()
        {
            return await _context.CustomerInteractions.ToListAsync();
        }
    }
}
