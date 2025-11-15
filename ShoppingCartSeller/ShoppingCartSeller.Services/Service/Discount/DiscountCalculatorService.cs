using ShoppingCartSeller.DTO.Cart;
using ShoppingCartSeller.DTO.Discount;
using ShoppingCartSeller.DTO.Payments;

namespace ShoppingCartSeller.Services.Service.Discount
{
    public interface IDiscountCalculatorService
    {
        decimal CalculateDiscount(List<CartItemDTO> items,
           decimal baseAmount,
           PaymentInformationDTO paymentInformation,
           List<DiscountRuleDTO> discountRules);
    }
    public class DiscountCalculatorService : IDiscountCalculatorService
    {
        public decimal CalculateDiscount(
            List<CartItemDTO> items,
        decimal baseAmount,
            PaymentInformationDTO paymentInformation,
            List<DiscountRuleDTO> discountRules)
        {
            decimal totalDiscount = 0;

            foreach (var rule in discountRules)
            {
                switch (rule.Type?.ToUpper())
                {
                    case "PRODUCT":
                        totalDiscount += CalculateProductDiscount(items, rule);
                        break;

                    case "AMOUNT":
                        if (baseAmount >= rule.MinAmount)
                            totalDiscount += ApplyDiscount(baseAmount, rule);
                        break;
                    ///make change of status 
                    case "PAYMENTMETHOD":
                        if (paymentInformation.Status?.ToUpper() == rule.PaymentMethod?.ToUpper())
                            totalDiscount += ApplyDiscount(baseAmount, rule);
                        break;
                }
            }

            return Math.Round(totalDiscount, 2);
        }

        private decimal CalculateProductDiscount(List<CartItemDTO> items, DiscountRuleDTO rule)
        {
            var matchingItems = items.Where(i => i.ProductId == rule.ProductId);
            decimal discount = 0;

            foreach (var item in matchingItems)
            {
                var itemTotal = item.UnitPrice * item.Quantity;
                discount += ApplyDiscount(itemTotal, rule);
            }

            return discount;
        }

        private decimal ApplyDiscount(decimal amount, DiscountRuleDTO rule)
        {
            return rule.DiscountMode?.ToUpper() switch
            {
                "FLAT" => rule.Value,
                "PERCENTAGE" => Math.Round(amount * (rule.Value / 100), 2),
                _ => 0
            };
        }
    }

}
