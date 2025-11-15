
namespace ShoppingCartSeller.Services.Abstraction.Notifications
{
    public interface INotificationService
    {
        Task NotifyCustomerAsync(Guid orderId, string message);
        Task NotifySellerAsync(Guid orderId, string message);
    }

}
