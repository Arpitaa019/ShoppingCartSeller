
namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface ICourierService
    {
        Task<bool> SchedulePickupAsync(Guid orderId);
        Task<bool> TrackShipmentAsync(Guid orderId);
    }

}
