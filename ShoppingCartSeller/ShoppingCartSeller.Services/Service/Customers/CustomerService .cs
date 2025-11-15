
using ShoppingCartSeller.Core.Repository.Abstraction.Customers;
using ShoppingCartSeller.DTO.Customers;
using ShoppingCartSeller.Services.Abstraction.Customers;

namespace ShoppingCartSeller.Services.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerInteractionModel>> GetAllInteractionsAsync()
        {
             var entity = await _repo.GetAllInteractionsAsync();

            return entity.Select(c => new CustomerInteractionModel
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Rating = c.Rating,
                Feedback = c.Feedback,
                CreatedAt = c.CreatedAt,
                ProductName = c.ProductName

            }).ToList();
        }
    }
}
