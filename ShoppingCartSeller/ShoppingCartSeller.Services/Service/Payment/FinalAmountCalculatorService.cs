

using ShoppingCartSeller.DTO.Payments;
using ShoppingCartSeller.Services.Abstraction.ProductTypePayment;

namespace ShoppingCartSeller.Services.Service.Payment
{
    public class FinalAmountCalculatorService : IFinalAmountCalculatorService
    {
        private const decimal PlatformFee = 57.00m;

        public FinalAmountBreakdown CalculateFinalAmount(
            decimal baseAmount,
            string paymentMethod,
            List<ChargeDTO> additionalCharges = null)
        {
            var paymentCharge = GetPaymentMethodCharge(paymentMethod);
            var cashback = GetCashback(paymentMethod, baseAmount);
            var discountAmount = 1;// CalculateDiscount(baseAmount);
            var otherCharges = additionalCharges?.Sum(c => c.Amount) ?? 0;

            var finalAmount = baseAmount + PlatformFee + paymentCharge + otherCharges - discountAmount - cashback;

            return new FinalAmountBreakdown
            {
                BaseAmount = baseAmount,
                PlatformFee = PlatformFee,
                PaymentCharge = paymentCharge,
                DiscountAmount = discountAmount,
                CashbackAmount = cashback,
                OtherChargesTotal = otherCharges,
                FinalAmount = Math.Round(finalAmount, 2),
                Summary = $"Base: ₹{baseAmount}, Platform: ₹{PlatformFee}, Payment: ₹{paymentCharge}, " +
                          $"Discount: -₹{discountAmount}, Cashback: -₹{cashback}, Other: ₹{otherCharges}, " +
                          $"Final: ₹{Math.Round(finalAmount, 2)}"
            };
        }

        private decimal GetPaymentMethodCharge(string method)
        {
            return method?.ToUpper() switch
            {
                "UPI" => 0,
                "CARD" => 10,
                "NETBANKING" => 5,
                "COD" => 20,
                "EMI" => 15,
                _ => 0
            };
        }

        private decimal GetCashback(string method, decimal amount)
        {
            return method?.ToUpper() switch
            {
                "UPI" => Math.Round(amount * 0.05m, 2),
                "CARD" => Math.Round(amount * 0.02m, 2),
                _ => 0
            };
        }

    }
}
