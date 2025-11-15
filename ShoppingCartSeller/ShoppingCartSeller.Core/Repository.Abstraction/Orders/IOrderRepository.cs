using ShoppingCartSeller.Core.Entities.Orders;

namespace ShoppingCartSeller.Core.Repository.Abstraction.ProductType
{
    public interface IOrderRepository
    {
        Task<Guid> CreateOrderAsync(Order order);
        Task<bool> AddOrderItemAsync(Guid orderId, IEnumerable<OrderItem> items);
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<bool> UpdateOrderStatusAsync(Guid id, int status);
        Task<bool> UpdatePaymentTransactionAsync(Guid orderId, string txnId);
        Task<IEnumerable<Order>> GetAllAsync();

    }
}
