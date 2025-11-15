namespace ShoppingCartSeller.Core.Entities.Sellers
{
    public class SellerDetails
    {
        //public int PersonnelId { get; set; }
        public int SellerId { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // Owner, Manager, Accountant, etc.
        public string? Department { get; set; }

        // Contact Info
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? AlternatePhone { get; set; }
        public string? LinkedInProfile { get; set; }

        // Authentication & Access
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }

        // Compliance & KYC
        public string? PAN { get; set; }
        public string? AadharNumber { get; set; }
        public bool? IsKYCVerified { get; set; }
        public DateTime? KYCVerifiedOn { get; set; }

        // Metadata
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public SellerDetails()
        {
            CreatedOn = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
