
using Microsoft.Extensions.Configuration;
using ShoppingCartSeller.DTO.Payments;
using ShoppingCartSeller.Services.Abstraction.Cart;

namespace ShoppingCart.Services.Service.Client
{
    public class RazorpayClient : IRazorpayClient
    {
        private readonly string _key;
        private readonly string _secret;

        public RazorpayClient(IConfiguration config)
        {
            _key = config["Razorpay:Key"];
            _secret = config["Razorpay:Secret"];
        }

        public async Task<PaymentResult> CreatePaymentAsync(Guid orderId, decimal amount)
        {
            //var client = new Razorpay.Api.RazorpayClient(_key, _secret);
            var options = new Dictionary<string, object>
        {
            { "amount", (int)(amount * 100) },
            { "currency", "INR" },
            { "receipt", orderId.ToString() },
            { "payment_capture", 1 }
        };

            // var order = client.Order.Create(options);

            return new PaymentResult
            {
                // PaymentTxnId = order["id"].ToString(),
                Gateway = "Razorpay",
                IsSuccess = true,
                Message = "Order created"
            };
        }

        public async Task<bool> VerifySignatureAsync(string payload, string signature)
        {
            try
            {
                // Razorpay.Api.Utils.verifyWebhookSignature(payload, signature, _secret);
                return true;
            }
            //catch (Razorpay.Api.Errors.SignatureVerificationError)
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
