using ShoppingCartSeller.DTO.Customers;

namespace ShoppingCartSeller.Services.Abstraction.Customers
{
    public interface ICustomerService
    {
        Task<List<CustomerInteractionModel>> GetAllInteractionsAsync();
    }

}
