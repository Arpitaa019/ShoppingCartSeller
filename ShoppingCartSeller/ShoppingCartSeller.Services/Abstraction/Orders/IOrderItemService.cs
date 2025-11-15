using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCartSeller.Services.Abstraction.Orders
{
    public interface IOrderItemService
    {
        Task<OrderItemDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(Guid orderId);
        Task<IEnumerable<OrderItemDTO>> GetAllAsync();
        Task<OrderItemDTO> CreateAsync(OrderItemDTO orderItemDto);
        Task<OrderItemDTO> UpdateAsync(OrderItemDTO orderItemDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
