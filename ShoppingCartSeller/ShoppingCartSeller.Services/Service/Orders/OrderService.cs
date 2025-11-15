using ShoppingCartSeller.Core.Entities.Orders;
using ShoppingCartSeller.Core.Repository.Abstraction.ProductType;
using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Services.Abstraction.Orders;

namespace ShoppingCart.Abstraction.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> CancelOrderAsync(Guid orderId, string reason)
        {
            return await _repo.UpdateOrderStatusAsync(orderId, (int)OrderStatus.Cancelled);
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderNumber = orderDto.OrderNumber,
                Status = (OrderStatus)(int)orderDto.Status,
                TotalAmount = orderDto.TotalAmount,
                PlacedAt = DateTime.Now,
                PaymentTxnId = orderDto.PaymentTxnId
            };
            var orderId = await _repo.CreateOrderAsync(order);
            if (orderId == Guid.Empty)
                throw new Exception("Failed to create order");

            if (orderDto.items != null && orderDto.items.Any())
            {
                var entityItems = orderDto.items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Title = i.Title,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Discount = i.Discount,
                    Seller = i.Seller,
                    DeliveryStatus = (int)i.DeliveryStatus
                });

                await _repo.AddOrderItemAsync(orderId, entityItems);
            }

            orderDto.Id = orderId;
            return orderDto;
        }

        public async Task<bool> CreateOrderItemAsync(Guid orderId, IEnumerable<OrderItemDTO> items)
        {
            var entityItems = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Title = i.Title,
                Quantity = i.Quantity,
                Price = i.Price,
                Discount = i.Discount,
                Seller = i.Seller,
                DeliveryStatus = (int)i.DeliveryStatus
            });
            return await _repo.AddOrderItemAsync(orderId, entityItems);

        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _repo.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            return new  OrderDTO
            {
                Id = orderId,
                UserId = order.UserId,
                OrderNumber = order.OrderNumber,
                TotalAmount = order.TotalAmount,
                PaymentTxnId = order.PaymentTxnId,
                PlacedAt = order.PlacedAt,
                Status = (OrderStatusDTO)order.Status
            };
        }

        public async Task<bool> UpdateOrderAsync(Guid orderId, OrderDTO order)
        {
            var existing = await _repo.GetOrderByIdAsync(orderId);
            if (existing == null)
                throw new Exception("Order not found.");

            return await _repo.UpdateOrderStatusAsync(orderId, (int)order.Status);
        }

        public Task<bool> UpdateOrderStatusAsync(Guid orderId, OrderStatusDTO    newStatus, string remarks = null)
        {
            throw new NotImplementedException();
        }

        public async Task  UpdatePaymentTransaction(Guid orderId, string txnId)
        {
            await _repo.UpdatePaymentTransactionAsync(orderId, txnId);
        }
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var all = await _repo.GetAllAsync(); 
            return all.Select(o => new OrderDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderNumber = o.OrderNumber,
                Status = (OrderStatusDTO)o.Status,
                TotalAmount = o.TotalAmount,
                PlacedAt = o.PlacedAt,
                PaymentTxnId = o.PaymentTxnId
            });
        }
    }
}
