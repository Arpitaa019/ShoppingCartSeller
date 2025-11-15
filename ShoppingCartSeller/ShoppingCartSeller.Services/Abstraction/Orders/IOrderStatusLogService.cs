
using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCartSeller.Services.Abstraction.Orders
{
    public interface IOrderStatusLogService
    {
        Task<OrderStatusLogDTO> GetByIdAsync(int id);
        Task<IEnumerable<OrderStatusLogDTO>> GetByOrderIdAsync(Guid orderId);
        Task<IEnumerable<OrderStatusLogDTO>> GetAllAsync();
        Task<OrderStatusLogDTO> CreateAsync(OrderStatusLogDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
