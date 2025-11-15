namespace ShoppingCartSeller.DTO.Payments
{
    public class EmiDetailsDTO
    {
        public string Provider { get; set; }
        public int TenureMonths { get; set; }
        public decimal MonthlyInstallment { get; set; }
    }

}
