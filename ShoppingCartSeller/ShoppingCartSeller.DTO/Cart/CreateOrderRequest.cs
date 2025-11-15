using ShoppingCartSeller.DTO.Orders;

namespace ShoppingCartSeller.DTO.Cart
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public List<CartItemDTO> Items { get; set; }
        public OrderAddressDTO Address { get; set; }
        public string PaymentOption { get; set; }
    }
}
