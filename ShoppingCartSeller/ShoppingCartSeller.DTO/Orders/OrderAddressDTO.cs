namespace ShoppingCartSeller.DTO.Orders
{
    public class OrderAddressDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string RecipientName { get; set; }
        public string ContactNumber { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
    }
}
