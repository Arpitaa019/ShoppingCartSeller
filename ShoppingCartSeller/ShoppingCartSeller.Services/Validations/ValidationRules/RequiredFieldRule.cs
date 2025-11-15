

using ProjectsLibrary.Models.Product;

namespace ShoppingCartSeller.Services.Validations.ValidationRules
{
    public class RequiredFieldRule : IValidationRule<ProductUploadModel>
    {
        public ValidationResult Validate(ProductUploadModel entity)
        {
            if (entity == null)
            {
                return new ValidationResult() { Success = false };
            }

            if (string.IsNullOrEmpty(entity.ProductModel.ModelName))
                return new ValidationResult() { Success = false };

            if (string.IsNullOrEmpty(entity.CategoryModel.CategoryName))
                return new ValidationResult() { Success = false };


            return new ValidationResult() { Success = true };
        }
    }
}
