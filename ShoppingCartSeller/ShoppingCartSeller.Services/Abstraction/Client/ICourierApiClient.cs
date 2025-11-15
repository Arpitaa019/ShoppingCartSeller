
using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCartSeller.Services.Abstraction.Client
{
    public interface ICourierApiClient
    {
        Task<bool> SchedulePickupAsync(Guid orderId, OrderAddressDTO address);
        Task<string> TrackShipmentAsync(Guid orderId);
    }

}
