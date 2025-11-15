using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.DTO.Seller
{
    public class SellerDetailsModel
    {
        // Identity
        //public int PersonnelId { get; set; }

        public int SellerId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; } // Owner, Manager, Accountant, etc.
        public string? Department { get; set; }


        // Contact Info
        [Required]
        public string? Email { get; set; }

        [Required]
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
        public DateTime? CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public CompanyModel Company { get; set; } = new CompanyModel();

        public LoginModel Login { get; set; } = new LoginModel();

    }
}
