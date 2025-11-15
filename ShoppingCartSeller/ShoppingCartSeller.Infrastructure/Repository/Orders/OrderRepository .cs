using ShoppingCartSeller.Core.Entities.Orders;
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Infrastructure.Sql;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbHelper _db;
        public OrderRepository(DbHelper db)
        {
            _db = db;
        }
        public async Task<bool> AddOrderItemAsync(Guid orderId, IEnumerable<OrderItem> items)
        {
            try
            {

                foreach (var item in items)
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@OrderId", orderId),
                        new SqlParameter("@ProductId", item.ProductId),
                        new SqlParameter("@Title", item.Title ?? string.Empty),
                        new SqlParameter("@Quantity", item.Quantity),
                        new SqlParameter("@Price", item.Price),
                        new SqlParameter("@Discount", item.Discount),
                        new SqlParameter("@Seller", item.Seller ?? string.Empty),
                        new SqlParameter("@DeliveryStatus", item.DeliveryStatus)
                    };

                    await _db.ExecuteNonQueryAsync(SellerSql.InsertOrderItem, parameters, CommandType.StoredProcedure);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw; // Re-throw to let controller see it
            }
        }

        public async Task<Guid> CreateOrderAsync(Order order)
        {
            try
            {
                var parameters = new[]
                 {
                    new SqlParameter("@UserId", order.UserId ?? throw new ArgumentNullException(nameof(order.UserId))),
                    new SqlParameter("@OrderNumber", order.OrderNumber),
                    new SqlParameter("@Status", (int)order.Status),
                    new SqlParameter("@TotalAmount", order.TotalAmount),
                    new SqlParameter("@PaymentTxnId", (object)order.PaymentTxnId ?? DBNull.Value)
                };
                var dt = await _db.ExecuteReader(SellerSql.InsertOrder, parameters, CommandType.StoredProcedure);

                if (dt.Rows.Count > 0 && Guid.TryParse(dt.Rows[0][0].ToString(), out Guid newId))
                    return newId;
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var dt = await _db.ExecuteReader(SellerSql.GetAllOrders, null, CommandType.StoredProcedure);
            var orders = new List<Order>();

            foreach (DataRow row in dt.Rows)
            {
                orders.Add(new Order
                {
                    Id = Guid.Parse(row["Id"].ToString()),
                    UserId = row["UserId"].ToString(),
                    OrderNumber = row["OrderNumber"].ToString(),
                    Status = (OrderStatus)Convert.ToInt32(row["Status"]),
                    TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                    PlacedAt = Convert.ToDateTime(row["PlacedAt"]),
                    PaymentTxnId = row["PaymentTxnId"].ToString()
                });
            }

            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            var parameters = new[]
             {
                new SqlParameter("@OrderId", orderId)
            };

            var dt = await _db.ExecuteReader(SellerSql.GetOrderById, parameters, CommandType.StoredProcedure);

            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new Order
            {
                UserId = row["UserId"].ToString(),
                OrderNumber = row["OrderNumber"].ToString(),
                TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                PaymentTxnId = row["PaymentTxnId"] == DBNull.Value ? null : row["PaymentTxnId"].ToString(),
                Status = (OrderStatus)Convert.ToInt32(row["Status"])
            };

        }

        public Task<bool> UpdateOrderStatusAsync(Guid id, int status)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdatePaymentTransactionAsync(Guid orderId, string txnId)
        {
            var parameters = new[]
            {
                new SqlParameter("@OrderId", orderId),
                new SqlParameter("@PaymentTxnId", txnId)
            };

            int rowsAffected = await _db.ExecuteNonQueryAsync(SellerSql.UpdatePaymentTransaction, parameters, CommandType.StoredProcedure);
            return rowsAffected > 0;
        }
    }
}
