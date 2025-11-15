using ShoppingCartSeller.Core.Entities.Notifications;
using ShoppingCartSeller.Core.Repository.Abstraction.Notifications;
using ShoppingCartSeller.DTO.Notification;
using ShoppingCartSeller.Services.Abstraction.Notifications;

namespace ShoppingCartSeller.Services.Service.Notification
{
    public class NotificationService : INotificationServices
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> CreateNotification(string sellerId, string title, string message)
        {
            var notification = new SellerNotification
            {
                SellerId = sellerId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            return await _notificationRepository.AddNotification(notification);
        }
        public async Task<List<SellerNotificationModel>> GetUnreadNotifications(string sellerId)
        {
            var entities = await _notificationRepository.GetUnreadNotification(sellerId);
            return entities.Select(n => new SellerNotificationModel
            {
                Id = n.Id,
                SellerId = n.SellerId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToList();
        }
        public async Task MarkAsRead(int Id)
        {
            await _notificationRepository.MarkNotificationsAsRead(Id);
        }
        public async Task<List<SellerNotificationModel>> GetAllNotifications(string sellerId)
        {
            var entities = await _notificationRepository.GetAllNotifications(sellerId);

            return entities.Select(n => new SellerNotificationModel
            {
                Id = n.Id,
                SellerId = n.SellerId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            }).ToList();
        }

        public async Task<SellerNotificationModel> GetLatestNotification(string sellerId)
        {
            var n = await _notificationRepository.GetLatestNotification(sellerId);

            if (n == null) return null;

            return new SellerNotificationModel
            {
                Id = n.Id,
                SellerId = n.SellerId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            };
        }
    }
}
