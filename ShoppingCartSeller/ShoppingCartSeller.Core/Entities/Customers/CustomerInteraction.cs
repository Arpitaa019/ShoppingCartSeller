using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.Core.Entities.Customers
{
    public class CustomerInteraction
    {
        [Key]
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProductName { get; set; }
        public int Rating { get; set; }
        public string? Feedback { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}


