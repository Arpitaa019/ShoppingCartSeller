using ShoppingCartSeller.DTO.Orders;
using ShoppingCartSeller.DTO.Payments;

namespace ShoppingCartSeller.DTO.Cart
{
    public class CartOrderCreateDTO
    {
        public string UserId { get; set; }
        public List<CartItemDTO> items { get; set; }
        public OrderAddressDTO orderAddressDTO { get; set; }
        public PaymentInformationDTO paymentInformation { get; set; }
        public List<OrderOfferDTO> offerDTOs { get; set; }

    }
}