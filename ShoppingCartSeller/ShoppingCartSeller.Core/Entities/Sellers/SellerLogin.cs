namespace ShoppingCartSeller.Core.Entities.Sellers
{
    public class SellerLogin
    {
        public int LoginId { get; set; }
        public int SellerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
