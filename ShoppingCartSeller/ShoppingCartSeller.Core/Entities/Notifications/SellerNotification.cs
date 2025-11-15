using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.Core.Entities.Notifications
{
    public class SellerNotification
    {
        [Key]
        public int Id { get; set; }
        public string SellerId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
