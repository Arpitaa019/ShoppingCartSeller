
using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCartSeller.Services.Abstraction.Orders
{
    public interface IOrderService
    {
        Task<bool> CreateOrderItemAsync(Guid orderId, IEnumerable<OrderItemDTO> Items);
        Task<OrderDTO> CreateOrderAsync(OrderDTO order);
        Task<bool> UpdateOrderAsync(Guid orderId, OrderDTO order);
        Task<OrderDTO> GetOrderByIdAsync(Guid orderId);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusDTO newStatus, string remarks = null);
        Task<bool> CancelOrderAsync(Guid orderId, string reason);
        Task UpdatePaymentTransaction(Guid orderId, string txnId);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();

    }

}
