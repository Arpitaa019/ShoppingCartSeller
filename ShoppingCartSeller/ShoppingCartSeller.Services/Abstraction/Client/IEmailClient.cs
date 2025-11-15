namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface IEmailClient
    {
        Task<bool> SendEmailAsync(string to = "Raghuveer.krishna02@gmail.com", string subject = "test subject", string body = "mail from saloni");
    }
}
