namespace ShoppingCartSeller.Services.Abstraction.Payment
{
    public interface IPaymentChargeService
    {
        decimal GetPlatformFee();
        decimal GetPaymentMethodCharge(string paymentMethod);
        decimal GetCashback(string paymentMethod, decimal amount);
        decimal CalculateFinalAmount(decimal baseAmount, string paymentMethod);

    }
}
