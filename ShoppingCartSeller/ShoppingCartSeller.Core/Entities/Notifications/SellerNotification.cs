namespace ShoppingCartSeller.Core.Entities.Notifications
{
    public class SellerNotification
    {
        public int Id { get; set; }
        public string SellerId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
