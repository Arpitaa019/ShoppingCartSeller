using ShoppingCartSeller.Services.Abstraction.Cart;
namespace ShoppingCartSeller.Services.Service.Client
{
    public class SmsGatewayClient : ISmsGatewayClient
    {
        private readonly HttpClient _http;

        public SmsGatewayClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> SendSmsAsync(string mobile, string message)
        {
            var payload = new Dictionary<string, string>
            {
                { "to", mobile },
                { "message", message },
                { "sender", "FLPKRT" }
            };

            var response = await _http.PostAsync("https://smsapi.example.com/send", new FormUrlEncodedContent(payload));
            return response.IsSuccessStatusCode;
        }
    }
}

