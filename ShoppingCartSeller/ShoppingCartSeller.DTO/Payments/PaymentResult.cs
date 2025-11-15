namespace ShoppingCartSeller.DTO.Payments
{
    public class PaymentResult
    {
        public string PaymentTxnId { get; set; }
        public string Gateway { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
