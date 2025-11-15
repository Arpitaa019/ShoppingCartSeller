using ShoppingCartSeller.DTO.Discount;

namespace ShoppingCartSeller.DTO.Payments
{
    public class PaymentInformationDTO
    {
        public Guid TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public UpiDetailsDTO Upi { get; set; }
        public CardDetailsDTO Card { get; set; }
        public NetBankingDetailsDTO NetBanking { get; set; }
        public GiftCardDetailsDTO GiftCard { get; set; }
        public EmiDetailsDTO EMI { get; set; }
        public CodDetailsDTO COD { get; set; }
    }
}