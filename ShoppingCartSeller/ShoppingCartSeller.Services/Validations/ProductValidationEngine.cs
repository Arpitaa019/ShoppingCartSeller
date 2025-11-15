using ProjectsLibrary.Models.Product;
using ShoppingCartSeller.Services.Validations.ValidationRules;

namespace ShoppingCartSeller.Services.Validations
{
    public class ProductValidationEngine : ValidationEngine<ProductUploadModel>
    {
        //public ProductValidationEngine()
        //{

        //    List<IValidationRule<ProductUploadDTO>> rules=new List<IValidationRule<ProductUploadDTO>>();
        protected List<IValidationRule<ProductUploadModel>> _rules;
        //}
        public ProductValidationEngine()
        {
            _rules = new List<IValidationRule<ProductUploadModel>>()
            {
                new PriceRangeRule(),
                new PriceRangeRule()
            };
        }

        public override List<(ProductUploadModel, List<string>)> RunValidation(IEnumerable<ProductUploadModel> proeducts)
        {
            List<(ProductUploadModel, List<string>)> productValidated = new List<(ProductUploadModel, List<string>)>();

            foreach (var product in proeducts)
            {
                var errors = _rules.Select(rule => rule.Validate(product))
                         .Where(x => x.Success == false)
                         .Select(x => x.Message)
                         .ToList();
                if (errors.Count > 0)
                {
                    productValidated.Add((product, errors));
                }
            }
            return productValidated;
        }

    }
}

