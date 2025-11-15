using ShoppingCartSeller.Services.Abstraction.Payment;

namespace ShoppingCartSeller.Services.Service.Payment
{
    public class PaymentChargeService : IPaymentChargeService
    {
        private const decimal PlatformFee = 57.00m;

        public decimal GetPlatformFee() => PlatformFee;

        public decimal GetPaymentMethodCharge(string paymentMethod)
        {
            return paymentMethod?.ToUpper() switch
            {
                "UPI" => 0.00m,
                "CARD" => 10.00m,
                "NETBANKING" => 5.00m,
                "COD" => 20.00m,
                "EMI" => 15.00m,
                "GIFTCARD" => 0.00m,
                _ => 0.00m
            };
        }

        public decimal GetCashback(string paymentMethod, decimal amount)
        {
            return paymentMethod?.ToUpper() switch
            {
                "UPI" => Math.Round(amount * 0.05m, 2), // 5% cashback
                "CARD" => Math.Round(amount * 0.02m, 2), // 2% cashback
                _ => 0.00m
            };
        }

        public decimal CalculateFinalAmount(decimal baseAmount, string paymentMethod)
        {
            var methodCharge = GetPaymentMethodCharge(paymentMethod);
            var cashback = GetCashback(paymentMethod, baseAmount);
            var finalAmount = baseAmount + PlatformFee + methodCharge - cashback;
            return Math.Round(finalAmount, 2);
        }
    }
}
