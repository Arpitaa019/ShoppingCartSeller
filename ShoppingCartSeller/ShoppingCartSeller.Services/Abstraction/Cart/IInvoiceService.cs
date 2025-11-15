
namespace ShoppingCartSeller.Services.Abstraction.Cart
{
    public interface IInvoiceService
    {
        Task<string> GenerateInvoiceAsync(Guid orderId);
        Task<bool> AttachInvoiceAsync(Guid orderId, string invoiceUrl);
    }

}
