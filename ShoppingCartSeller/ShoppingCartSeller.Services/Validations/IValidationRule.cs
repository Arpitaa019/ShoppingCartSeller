using System.ComponentModel.DataAnnotations;

namespace ShoppingCartSeller.Services.Validations
{
    public interface IValidationRule<T>
    {
        ValidationResult Validate(T entity);
    }
}
