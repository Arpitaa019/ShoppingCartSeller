namespace ShoppingCartSeller.Shared.Enum
{
    public enum ProductStatus
    {
        New = 0,                 // Product created but incomplete
        ReadyToReview = 1,       // Awaiting internal review
        UnderReview = 2,         // Actively being reviewed
        NeedsCorrection = 3,     // Sent back with validation errors
        ReadyToView = 4,         // Approved for preview/staging
        ApprovedForPublish = 5,  // Final approval granted
        Published = 6,           // Live on storefront
        Unpublished = 7,         // Removed but retained for audit
        Archived = 8             // Permanently retired or deprecated
    }
}
