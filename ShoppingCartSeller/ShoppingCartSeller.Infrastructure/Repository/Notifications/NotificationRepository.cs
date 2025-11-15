using Microsoft.EntityFrameworkCore;
using ShoppingCartSeller.Core.Entities.Notifications;
using ShoppingCartSeller.Core.Repository.Abstraction.Notifications;
using ShoppingCartSeller.Infrastructure.Sql;

namespace ShoppingCartSeller.Infrastructure.Repository.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ShoppingCartSellerDbContext _context;

        public NotificationRepository(ShoppingCartSellerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNotification(SellerNotification notification)
        {
            _context.SellerNotifications.Add(notification);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<SellerNotification>> GetUnreadNotification(string sellerId)
        {
            return await _context.SellerNotifications.Where(n => n.SellerId == sellerId && !n.IsRead).ToListAsync();
        }
        public async Task MarkNotificationsAsRead(int id)
        {
            var notification = await _context.SellerNotifications.FindAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<SellerNotification>> GetAllNotifications(string sellerId)
        {
            return await _context.SellerNotifications.Where(n => n.SellerId == sellerId).ToListAsync();
        }

        public async Task<SellerNotification> GetLatestNotification(string sellerId)
        {
            return await _context.SellerNotifications.Where(n => n.SellerId == sellerId).OrderByDescending(n => n.CreatedAt).FirstOrDefaultAsync();
        }
    }
}
