namespace ShoppingCartSeller.Services.Validations
{
    public abstract class ValidationEngine<T>
    {

        public ValidationEngine()
        {

        }

        public abstract List<(T, List<string>)> RunValidation(IEnumerable<T> lIst);
    }
}
