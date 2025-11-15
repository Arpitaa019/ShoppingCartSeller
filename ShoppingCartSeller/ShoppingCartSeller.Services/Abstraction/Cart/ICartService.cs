using ShoppingCartSeller.DTO.Cart;
using ShoppingCartSeller.DTO.Payments;


namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface ICartService
    {
        Task<bool> ValidateCartAsync(string userId);
        Task<bool> CheckInventoryAsync(List<CartItemDTO> items);
        Task<PaymentCalculationResult> CalculatePaymentAsync(List<CartItemDTO> items, string paymentOption);
    }

}
