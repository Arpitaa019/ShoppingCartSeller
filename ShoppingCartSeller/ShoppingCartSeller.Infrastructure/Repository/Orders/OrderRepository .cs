using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Orders;
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Seller
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingCartSellerDbContext _context;
        public OrderRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrderItemAsync(Guid orderId, IEnumerable<OrderItem> items)
        {
            foreach (var item in items)
            {
                item.OrderId = orderId;
                _context.OrderItems.Add(item);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> CreateOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid();
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid id, int status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;
            order.Status = (OrderStatus)status;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePaymentTransactionAsync(Guid orderId, string txnId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            order.PaymentTxnId = txnId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
