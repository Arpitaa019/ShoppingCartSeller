using ShoppingCartSeller.DTO.Discount;
using ShoppingCartSeller.DTO.Payments;

namespace ShoppingCartSeller.Services.Abstraction.ProductTypePayment
{
    public interface IFinalAmountCalculatorService
    {
        FinalAmountBreakdown CalculateFinalAmount(
            decimal baseAmount,
            string paymentMethod,
            DiscountDTO discount = null,
            List<ChargeDTO> additionalCharges = null
        );

    }
}
