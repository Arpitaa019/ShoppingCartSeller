
using ShoppingCartSeller.Core.Entities.Orders;
using ShoppingCartSeller.DTO.Delivery;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Services.Abstraction.Orders;


namespace ShoppingCartSeller.Services.Service.Client
{

    public class OrderItemService : IOrderItemService
    {

        public OrderItemService()
        {

        }

        public async Task<OrderItemDTO> GetByIdAsync(Guid id)
        {
            return null;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetByOrderIdAsync(Guid orderId)
        {
            return null;
        }

        public async Task<IEnumerable<OrderItemDTO>> GetAllAsync()
        {
            return null;
        }

        public async Task<OrderItemDTO> CreateAsync(OrderItemDTO dto)
        {
            var entity = MapToEntity(dto);
            entity.Id = Guid.NewGuid();


            return MapToDTO(entity);
        }

        public async Task<OrderItemDTO> UpdateAsync(OrderItemDTO dto)
        {
            return null;
            //var entity = await _context.OrderItems.FindAsync(dto.Id);
            //if (entity == null) return null;

            //entity.ProductId = dto.ProductId;
            //entity.Title = dto.Title;
            //entity.Quantity = dto.Quantity;
            //entity.Price = dto.Price;
            //entity.Discount = dto.Discount;
            //entity.Seller = dto.Seller;
            //entity.DeliveryStatus = (int)dto.DeliveryStatus;
            //entity.InventoryID = dto.InventoryID;

            //await _context.SaveChangesAsync();
            //return MapToDTO(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {

            return true;
        }

        private static OrderItemDTO MapToDTO(OrderItem entity)
        {
            return new OrderItemDTO
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Title = entity.Title,
                Quantity = entity.Quantity,
                Price = entity.Price,
                Discount = entity.Discount,
                Seller = entity.Seller,
                DeliveryStatus = (DeliveryStatus)entity.DeliveryStatus,
                InventoryID = entity.InventoryID,
            };
        }

        private static OrderItem MapToEntity(OrderItemDTO dto)
        {
            return new OrderItem
            {
                Id = dto.Id,
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Title = dto.Title,
                Quantity = dto.Quantity,
                Price = dto.Price,
                Discount = dto.Discount,
                Seller = dto.Seller,
                DeliveryStatus = (int)dto.DeliveryStatus,
                InventoryID = dto.InventoryID
            };
        }
    }

}
