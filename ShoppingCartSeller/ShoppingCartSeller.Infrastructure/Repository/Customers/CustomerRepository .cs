using ProjectsLibrary.Infrastructure.Sql;
using ShoppingCartSeller.Core.Entities.Customers;
using ShoppingCartSeller.Core.Repository.Abstraction.Customers;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;

namespace ShoppingCartSeller.Infrastructure.Repository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbHelper _db;
        public CustomerRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task<List<CustomerInteraction>> GetAllInteractionsAsync()
        {
            var list = new List<CustomerInteraction>();
            var dt = await _db.ExecuteReader(SellerSql.GetCustomerInteractions, null, System.Data.CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new CustomerInteraction
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    CustomerId = row["CustomerId"].ToString(),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Rating = Convert.ToInt32(row["Rating"]),
                    Feedback = row["Feedback"].ToString(),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    ProductName = row["ProductName"].ToString()

                });
            }
            return list;
        }
    }
}
