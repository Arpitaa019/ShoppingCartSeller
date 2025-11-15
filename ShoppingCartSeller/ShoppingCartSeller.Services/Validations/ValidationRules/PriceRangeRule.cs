using ProjectsLibrary.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.Services.Validations.ValidationRules
{
    public class PriceRangeRule : IValidationRule<ProductUploadModel>
    {
        public ValidationResult Validate(ProductUploadModel entity)
        {
            if (entity == null)
            {
                return new ValidationResult() { Message = "", Success = false };
            }

            if (entity.ProductPriceModel.MRP <= 0 || entity.ProductPriceModel.SellingPrice <= 0)
            {
                return new ValidationResult() { Success = false, Message = "Either MRP or Selling price is less than 0." };
            }

            return new ValidationResult() { Success = true };

        }
    }
}
