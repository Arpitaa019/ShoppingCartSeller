using ShoppingCartSeller.DTO.Cart;
using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCartSeller.Services.Abstraction.Orders
{
    public interface IOrderManagerService
    {
        Task<OrderDTO> PlaceOrderAsync(CartOrderCreateDTO request);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusDTO newStatus, string remarks = null);
        Task<OrderDTO> GetOrderDetailsAsync(Guid orderId);
        Task<bool> CancelOrderAsync(Guid orderId, string reason);


    }
}
