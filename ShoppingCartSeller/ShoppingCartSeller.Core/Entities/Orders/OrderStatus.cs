namespace ShoppingCartSeller.Core.Entities.Orders
{
    public enum OrderStatus
    {
        Delivered = 0,
        Confirmed = 1,
        Shipped = 2,
        Placed = 3,
        Cancelled = 4,
        Returned = 5,
        Pending = 6
    }
}
