namespace ShoppingCartSeller.Core.Entities.Discounts
{

    public interface IDiscount
    {
        string GetDiscountName();
        decimal ApplyDiscount(decimal originalPrice);
        bool IsApplicable(DateTime currentDate);
    }

    public abstract class DiscountBase : IDiscount
    {
        public string DiscountName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public virtual bool IsApplicable(DateTime currentDate)
            => IsActive && currentDate >= StartDate && currentDate <= EndDate;

        public abstract decimal ApplyDiscount(decimal originalPrice);
        public virtual string GetDiscountName() => DiscountName;
    }
}
