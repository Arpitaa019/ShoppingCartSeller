namespace ShoppingCartSeller.Core.Entities.Sellers
{
    public class Company
    {
        // Identity
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string? LegalName { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? TaxIdentificationNumber { get; set; }
        public string? PAN { get; set; }
        public string GSTIN { get; set; }

        // Contact Info
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }

        // Address
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        // Financials
        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? Currency { get; set; }
        public decimal? PaidUpCapital { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public string? LogoUrl { get; set; }
        public string? Tagline { get; set; }
        public string? IndustryType { get; set; }
        public string? BusinessType { get; set; }
        public DateTime? IncorporationDate { get; set; }
        public string? Notes { get; set; }
        public int SellerId { get; set; }
        public Company()
        {
            CreatedOn = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
