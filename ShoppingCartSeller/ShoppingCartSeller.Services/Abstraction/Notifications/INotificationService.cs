using ShoppingCartSeller.DTO.Notification;

namespace ShoppingCartSeller.Services.Abstraction.Notifications
{
    public interface INotificationServices
    {
        Task<bool> CreateNotification(string sellerId, string title, string message);
        Task<List<SellerNotificationModel>> GetUnreadNotifications(string sellerId);
        Task<List<SellerNotificationModel>> GetAllNotifications(string sellerId);
        Task<SellerNotificationModel> GetLatestNotification(string sellerId);
        Task MarkAsRead(int Id);

    }
}
