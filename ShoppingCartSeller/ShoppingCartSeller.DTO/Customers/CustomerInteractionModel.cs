namespace ShoppingCartSeller.DTO.Customers
{
    public class CustomerInteractionModel
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

}
