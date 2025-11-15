
using ShoppingCartSeller.DTO.Payments;

namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface IRazorpayClient
    {
        Task<PaymentResult> CreatePaymentAsync(Guid orderId, decimal amount);
        Task<bool> VerifySignatureAsync(string payload, string signature);
    }
}
