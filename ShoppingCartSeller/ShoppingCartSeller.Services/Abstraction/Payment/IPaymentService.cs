

using ShoppingCartSeller.DTO.Payments;

namespace ShoppingCartSeller.Services.Abstraction.Payment
{
    public interface IPaymentService
    {
        Task<PaymentResult> CreatePaymentAsync(Guid orderId, decimal amount);
        Task<bool> AttachPaymentAsync(Guid orderId, string paymentTxnId);
        Task<bool> VerifyThirdPartyPaymentAsync(string payload, string signature);
    }

}
