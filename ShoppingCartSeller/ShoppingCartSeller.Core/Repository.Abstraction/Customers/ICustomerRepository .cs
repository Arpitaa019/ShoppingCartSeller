
using ShoppingCartSeller.Core.Entities.Customers;

namespace ShoppingCartSeller.Core.Repository.Abstraction.Customers
{
    public interface ICustomerRepository
    {
        Task<List<CustomerInteraction>> GetAllInteractionsAsync();
    }
}
