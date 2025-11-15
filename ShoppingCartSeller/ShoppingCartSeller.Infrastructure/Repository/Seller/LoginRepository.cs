using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbHelper _db;
        public LoginRepository(DbHelper db)
        {
            _db = db;
        }
        public async Task AddLoginAsync(SellerLogin login, int sellerId)
        {
            var loginIdParam = new SqlParameter("@LoginId", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _db.ExecuteNonQueryAsync(SellerSql.InsertSellerLogin, new[]
            {
                     new SqlParameter("@Email", login.Email),
                        new SqlParameter("@Password", login.Password),
                        new SqlParameter("@SellerId", sellerId),
                        new SqlParameter("@CreatedDate", DateTime.UtcNow),
                        loginIdParam
               }, CommandType.StoredProcedure);
            login.LoginId = (int)loginIdParam.Value;
        }

        public async Task<IEnumerable<SellerLogin>> GetAllAsync()
        {
            var logins = new List<SellerLogin>();
            var dt = await _db.ExecuteReader(SellerSql.GetAllSellerLogins, null, CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                logins.Add(new SellerLogin
                {
                    LoginId = Convert.ToInt32(row["LoginId"]),
                    SellerId = Convert.ToInt32(row["SellerId"]),
                    Email = row["Email"]?.ToString(),
                    Password = row["Password"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return logins;

        }

        public async Task<SellerLogin> GetLoginBySellerIdAsync(int sellerId)
        {

            var dt = await _db.ExecuteReader(SellerSql.GetLoginBySellerId,
                new[] { new SqlParameter("@SellerId", sellerId) }, CommandType.StoredProcedure);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new SellerLogin
            {
                LoginId = Convert.ToInt32(row["LoginId"]),
                SellerId = Convert.ToInt32(row["SellerId"]),
                Email = row["Email"]?.ToString(),
                Password = row["Password"]?.ToString(),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }
        public async Task<bool> UpdateLoginAsync(SellerLogin login, int sellerId)
        {
            var parameters = new[]
            {
                  new SqlParameter("@LoginId", login.LoginId),
                new SqlParameter("@SellerId", sellerId),
                new SqlParameter("@Email", login.Email),
                new SqlParameter("@Password", login.Password),
                new SqlParameter("@LastModifiedOn", DateTime.UtcNow),
                new SqlParameter("@ModifiedBy", "System")
             };
            return await _db.ExecuteNonQueryAsync(SellerSql.UpdateSellerLoginDetails, parameters, CommandType.StoredProcedure) > 0;
        }
    }
}
