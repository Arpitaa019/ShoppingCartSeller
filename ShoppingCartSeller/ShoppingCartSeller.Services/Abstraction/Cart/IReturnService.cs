
namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface IReturnService
    {
        Task<bool> InitiateReturnAsync(Guid orderId, string reason);
        Task<bool> ScheduleReturnPickupAsync(Guid orderId);
        Task<bool> ProcessRefundAsync(Guid orderId);
        Task<bool> CompleteExchangeAsync(Guid orderId);
    }

}
