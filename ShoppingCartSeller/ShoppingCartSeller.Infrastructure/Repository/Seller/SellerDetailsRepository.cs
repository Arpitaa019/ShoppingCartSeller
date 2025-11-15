using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{

    public class SellerDetailsRepository : ISellerDetailsRepository
    {
        private readonly DbHelper _db;
        public SellerDetailsRepository(DbHelper db)
        {
            _db = db;
        }
        public async Task<int> AddSellerDetailsAsync(SellerDetails seller)
        {
            _db.BeginTransaction();
            try
            {
                var sellerIdParam = new SqlParameter("@SellerId", SqlDbType.Int) { Direction = ParameterDirection.Output };

                await _db.ExecuteNonQueryAsync(SellerSql.InsertSellerDetails, new[]
                {
                    new SqlParameter("@FullName",seller.FullName),
                     new SqlParameter("@Email", seller.Email),
                    new SqlParameter("@Phone", seller.Phone),
                    new SqlParameter("@Role", seller.Role),
                    new SqlParameter("@CreatedOn", DateTime.UtcNow),
                    new SqlParameter("@IsAdmin", seller.IsAdmin ?? false),
                    sellerIdParam
                }, CommandType.StoredProcedure);

                if (sellerIdParam.Value == DBNull.Value)
                    throw new Exception("SellerId was not returned from stored procedure.");

                int sellerId = (int)sellerIdParam.Value;
                _db.Commit();
                return sellerId;
            }
            catch
            {
                _db.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteSellerAsync(int sellerId)
        {
            var parameters = new[] { new SqlParameter("@SellerId", sellerId) };

            int rowsAffected = await _db.ExecuteNonQueryAsync(SellerSql.DeleteSellerById, parameters, CommandType.StoredProcedure);
            System.Diagnostics.Debug.WriteLine($"Seller delete rows affected: {rowsAffected}");
            return true;

        }

        public async Task<IEnumerable<SellerDetails>> GetAllSellerAsync()
        {
            var sellers = new List<SellerDetails>();
            var dt = await _db.ExecuteReader(SellerSql.GetAllSellerDetails, null, CommandType.StoredProcedure);

            foreach (DataRow row in dt.Rows)
            {
                sellers.Add(new SellerDetails
                {
                    SellerId = row["SellerId"] != DBNull.Value ? Convert.ToInt32(row["SellerId"]) : 0,
                    FullName = row["FullName"]?.ToString(),
                    Email = row["Email"]?.ToString(),
                    Phone = row["Phone"]?.ToString(),
                    Role = row["Role"]?.ToString(),
                    CreatedOn = DateTime.UtcNow,
                    IsActive = true

                });
            }
            return sellers;
        }

        public async Task<SellerDetails> GetSellerByIdAsync(int sellerId)
        {
            var dt = await _db.ExecuteReader(SellerSql.GetSellerDetailsById,
                new[] { new SqlParameter("@SellerId", sellerId) }, CommandType.StoredProcedure);

            if (dt.Rows.Count == 0) return null;
            var row = dt.Rows[0];

            return new SellerDetails
            {
                SellerId = Convert.ToInt32(row["SellerId"]),
                FullName = row["FullName"]?.ToString(),
                Email = row["Email"]?.ToString(),
                Phone = row["Phone"]?.ToString(),
                Role = row["Role"]?.ToString(),
                CreatedOn = DateTime.UtcNow,
                IsActive = true

            };
        }
        public async Task<bool> UpdateSellerAsync(SellerDetails seller)
        {
            var parameters = new[]
            {
                    new SqlParameter("@SellerId", seller.SellerId),
                    new SqlParameter("@FullName", seller.FullName),
                    new SqlParameter("@Email", seller.Email),
                    new SqlParameter("@Phone", seller.Phone),
                    new SqlParameter("@Role", seller.Role)
             };

            return await _db.ExecuteNonQueryAsync(SellerSql.UpdateSellerDetails, parameters, CommandType.StoredProcedure) > 0;
        }
    }
}
