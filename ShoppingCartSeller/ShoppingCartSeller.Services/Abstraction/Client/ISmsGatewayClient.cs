namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface ISmsGatewayClient
    {
        Task<bool> SendSmsAsync(string mobile, string message);
    }
}
