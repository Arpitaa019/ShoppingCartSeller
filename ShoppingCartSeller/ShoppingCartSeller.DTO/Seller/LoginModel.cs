using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.DTO.Seller
{
    public class LoginModel
    {
        public int LoginId { get; set; }
        public int SellerId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
