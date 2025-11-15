using ShoppingCartSeller.Core.Entities.Notifications;
using ShoppingCartSeller.Core.Repository.Abstraction.Notifications;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartSeller.Infrastructure.Repository.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DbHelper _db;

        public NotificationRepository(DbHelper db)
        {
            _db = db;
        }

        public async Task<bool> AddNotification(SellerNotification notification)
        {
            var parameters = new[]
            {
                 new SqlParameter("@SellerId", notification.SellerId),
                new SqlParameter("@Title", notification.Title),
                new SqlParameter("@Message", notification.Message),
            };

            // Stored procedure name
            return await _db.ExecuteNonQueryAsync(SellerSql.AddSellerNotification, parameters, CommandType.StoredProcedure) > 0;
        }
        public async Task<List<SellerNotification>> GetUnreadNotification(string sellerId)
        {
            var parameters = new[]
            {
                new SqlParameter("@SellerId", sellerId)
            };

            var dt = await _db.ExecuteReader(SellerSql.GetUnreadSellerNotifications, parameters, CommandType.StoredProcedure);
            var list = new List<SellerNotification>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SellerNotification
                {
                    Id = Convert.ToInt32(row["Id"]),
                    SellerId = row["SellerId"].ToString(),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }

            return list;
        }
        public async Task MarkNotificationsAsRead(int id)
        {
            var parameters = new[] 
            { 
                new SqlParameter("@Id", id) 
            };
            await _db.ExecuteNonQueryAsync(SellerSql.MarkSellerNotificationAsRead, parameters, CommandType.StoredProcedure);
        }

        public async Task<List<SellerNotification>> GetAllNotifications(string sellerId)
        {
            var parameters = new[]
            {
                 new SqlParameter("@SellerId", sellerId)
             };

            var dt = await _db.ExecuteReader(SellerSql.GetAllSellerNotifications, parameters, CommandType.StoredProcedure);
            var list = new List<SellerNotification>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SellerNotification
                {
                    Id = Convert.ToInt32(row["Id"]),
                    SellerId = row["SellerId"].ToString(),
                    Title = row["Title"].ToString(),
                    Message = row["Message"].ToString(),
                    IsRead = Convert.ToBoolean(row["IsRead"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }

        public async Task<SellerNotification> GetLatestNotification(string sellerId)
        {
            var parameters = new[]
            {
                new SqlParameter("@SellerId", sellerId)
            };

            var dt = await _db.ExecuteReader(SellerSql.GetSellerLatestNotification, parameters, CommandType.StoredProcedure);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new SellerNotification
            {
                Id = Convert.ToInt32(row["Id"]),
                SellerId = row["SellerId"].ToString(),
                Title = row["Title"].ToString(),
                Message = row["Message"].ToString(),
                IsRead = Convert.ToBoolean(row["IsRead"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            };
        }
    }
}
