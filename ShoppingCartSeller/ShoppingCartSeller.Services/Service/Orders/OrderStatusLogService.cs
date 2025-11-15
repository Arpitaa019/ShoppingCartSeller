using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Services.Abstraction.Orders;

namespace ShoppingCart.Services.Service.Order
{

    public class OrderStatusLogService : IOrderStatusLogService
    {

        public OrderStatusLogService()
        {

        }

        public async Task<OrderStatusLogDTO> GetByIdAsync(int id)
        {
            return null;
        }

        public async Task<IEnumerable<OrderStatusLogDTO>> GetByOrderIdAsync(Guid orderId)
        {
            return null;
        }

        public async Task<IEnumerable<OrderStatusLogDTO>> GetAllAsync()
        {
            return null;
        }

        public async Task<OrderStatusLogDTO> CreateAsync(OrderStatusLogDTO dto)
        {
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return false;
        }

        private static OrderStatusLogDTO MapToDTO(OrderStatusLogDTO entity)//create entity and replce with that
        {
            //return new OrderStatusLogDTO
            //{
            //    Id = entity.Id,
            //    OrderId = entity.OrderId,
            //    Status = (OrderStatus)entity.Status,
            //    ChangedAt = entity.ChangedAt,
            //    Remarks = entity.Remarks,
            //    Order = entity.Order != null ? new OrderDTO
            //    {
            //        Id = entity.Order.Id,
            //        // Map other OrderDTO fields as needed
            //    } : null
            return null;
        }

        private static OrderStatusLogDTO MapToEntity(OrderStatusLogDTO dto) //change return with entity 
        {
            return new OrderStatusLogDTO
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                Status = dto.Status,
                ChangedAt = dto.ChangedAt,
                Remarks = dto.Remarks
            };
        }
    }

}
