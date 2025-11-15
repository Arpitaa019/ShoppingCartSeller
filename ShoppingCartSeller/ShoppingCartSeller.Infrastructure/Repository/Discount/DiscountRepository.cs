using ShoppingCartSeller.Core.Entities.Discounts;
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Discount
{

    public class DiscountRepository : IDiscountRepository
    {
        // Assume EF Core or Dapper context injected here

        private readonly DbHelper _db;
        public DiscountRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task<IEnumerable<IDiscount>> GetActiveDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IDiscount>> GetApplicableDiscountsAsync(int productId, DateTime currentDate)
        {
            // Simulated DB call
            var productDiscounts = new List<IDiscount>
            {
                new ProductWiseDiscount
                {
                    DiscountName = "Clearance Sale",
                    ProductID = productId,
                    DiscountPercent = 20,
                    StartDate = DateTime.Today.AddDays(-1),
                    EndDate = DateTime.Today.AddDays(5),
                    IsActive = true
                }
            };

            return await Task.FromResult(productDiscounts.Where(d => d.IsApplicable(currentDate)));
        }

        public async Task SaveDiscountAsync(IDiscount discount)
        {
            throw new NotImplementedException();
        }



        Task<IEnumerable<IDiscount>> IDiscountRepository.GetActiveDiscountsAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IDiscount>> IDiscountRepository.GetApplicableDiscountsAsync(int productId, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }

}
