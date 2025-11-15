using Microsoft.AspNetCore.Mvc;
using ShoppingCartSeller.Services.Abstraction.Notifications;

namespace ShoppingCartSeller.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationServices _notificationService;

        public NotificationController(INotificationServices notificationService)
        {
            _notificationService = notificationService;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index(string? sellerId)
        {
            sellerId ??= "seller001";

            if (string.IsNullOrEmpty(sellerId))
                return BadRequest("Seller ID is required.");

            var allNotifications = await _notificationService.GetAllNotifications(sellerId);
            return View("Index", allNotifications); 

           //  var notifications = await _notificationService.GetUserNotifications(userId);
           // await _notificationService.MarkUserNotificationsAsRead(userId);
           // return View(notifications);
       
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(string sellerId, string title, string message)
        {
            if (string.IsNullOrWhiteSpace(sellerId) || string.IsNullOrWhiteSpace(message))
                return BadRequest("User ID and message are required.");

            await _notificationService.CreateNotification(sellerId, title ?? "Notification", message);
            TempData["Success"] = "Notification added successfully!";

            return RedirectToAction("Index", new { sellerId });
        }

        [HttpGet]
        public async Task<IActionResult> CheckNewNotification(string? sellerId, int lastSeenId)
        {
            sellerId ??= "seller001"; 
            var notifications = await _notificationService.GetAllNotifications(sellerId);
            var latest = notifications.OrderByDescending(n => n.Id).FirstOrDefault();

            if (latest != null && latest.Id > lastSeenId)
            {
                return Json(new
                {
                    hasNew = true,
                    id = latest.Id,
                    message = latest.Message,
                    createdAt = latest.CreatedAt.ToString("g")
                });
            }

            return Json(new { hasNew = false });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsRead(id);
            return Ok();
        }
    }
}
