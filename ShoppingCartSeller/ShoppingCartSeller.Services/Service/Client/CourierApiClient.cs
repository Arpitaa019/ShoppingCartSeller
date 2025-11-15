using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.Services.Abstraction.Client;
using System.Net.Http.Json;

namespace ShoppingCartSeller.Services.Service.Client
{
    public class CourierApiClient : ICourierApiClient
    {
        private readonly HttpClient _http;

        public CourierApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> SchedulePickupAsync(Guid orderId, OrderAddressDTO address)
        {
            var payload = new
            {
                orderId,
                pickupAddress = address.AddressLine,
                city = address.City,
                state = address.State,
                contactNumber = address.ContactNumber
            };

            var response = await _http.PostAsJsonAsync("https://courierapi.example.com/schedule", payload);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> TrackShipmentAsync(Guid orderId)
        {
            var response = await _http.GetAsync($"https://courierapi.example.com/track/{orderId}");
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : "Tracking failed";
        }
    }
}
