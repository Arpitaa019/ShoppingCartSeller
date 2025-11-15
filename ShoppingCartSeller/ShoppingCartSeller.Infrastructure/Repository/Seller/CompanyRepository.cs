using ShoppingCartSeller.Core.Entities.Sellers;
using ShoppingCartSeller.Core.Repository.Abstraction.Sellers;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DbHelper _db;
        public CompanyRepository(DbHelper db)
        {
            _db = db;
        }
        public async Task<int> AddCompanyAsync(Company company, int sellerId)
        {
            var companyIdParam = new SqlParameter("@CompanyId", SqlDbType.Int) { Direction = ParameterDirection.Output };

            await _db.ExecuteNonQueryAsync(SellerSql.InsertCompany, new[]
            {
                new SqlParameter("@SellerId", sellerId),
                new SqlParameter("@Name", company.Name),
                new SqlParameter("@GSTIN", company.GSTIN),
                new SqlParameter("@City", company.City),
                new SqlParameter("@State", company.State),
                new SqlParameter("@CreatedOn", DateTime.UtcNow),
                new SqlParameter("@CreatedBy", "System"),
                new SqlParameter("@IsActive", true),
                companyIdParam
            }, CommandType.StoredProcedure);

            return (int)companyIdParam.Value;

        }

        public async Task<bool> DeleteAsync(int sellerId)
        {

            var parameters = new[] { new SqlParameter("@SellerId", sellerId) };

            int rowsAffected = await _db.ExecuteNonQueryAsync(SellerSql.DeleteCompanyBySellerId, parameters, CommandType.StoredProcedure);
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            var companies = new List<Company>();
            var dt = await _db.ExecuteReader(SellerSql.GetAllCompanies, null, CommandType.StoredProcedure);
            foreach (DataRow row in dt.Rows)
            {
                companies.Add(new Company
                {
                    CompanyId = Convert.ToInt32(row["CompanyId"]),
                    SellerId = Convert.ToInt32(row["SellerId"]),
                    Name = row["Name"]?.ToString(),
                    GSTIN = row["GSTIN"]?.ToString(),
                    City = row["City"]?.ToString(),
                    State = row["State"]?.ToString(),
                    CreatedOn = DateTime.UtcNow,
                    IsActive = true
                });
            }

            return companies;
        }

        public Task<Company> GetCompanyBySellerIdAsync(int sellerId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCompanyAsync(Company company, int sellerId)
        {
            var parameters = new[]
            {
                new SqlParameter("@CompanyId", company.CompanyId),
                new SqlParameter("@SellerId", sellerId),
                new SqlParameter("@Name", company.Name),
                new SqlParameter("@GSTIN", company.GSTIN),
                new SqlParameter("@City", company.City),
                new SqlParameter("@State", company.State),
                new SqlParameter("@LastModifiedOn", DateTime.UtcNow),
                new SqlParameter("@ModifiedBy", "System")
            };
            return await _db.ExecuteNonQueryAsync(SellerSql.UpdateCompanyDetails, parameters, CommandType.StoredProcedure) > 0;

        }
    }
}
