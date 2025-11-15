using ShoppingCartSeller.Core.Entities.Notifications;

namespace ShoppingCartSeller.Core.Repository.Abstraction.Notifications
{
    public interface INotificationRepository
    {
        Task<List<SellerNotification>> GetUnreadNotification(string sellerId);
        Task<bool> AddNotification(SellerNotification notification);
        Task<List<SellerNotification>> GetAllNotifications(string sellerId);
        Task<SellerNotification> GetLatestNotification(string sellerId);
        Task MarkNotificationsAsRead(int Id);
    }
}
