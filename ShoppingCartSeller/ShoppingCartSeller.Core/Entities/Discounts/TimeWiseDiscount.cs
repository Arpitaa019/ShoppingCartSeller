namespace ShoppingCartSeller.Core.Entities.Discounts
{
    public class TimeWiseDiscount : DiscountBase
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int DiscountPercent { get; set; }

        public override bool IsApplicable(DateTime currentDate)
        {
            var time = currentDate.TimeOfDay;
            return base.IsApplicable(currentDate) && time >= StartTime && time <= EndTime;
        }

        public override decimal ApplyDiscount(decimal originalPrice)
            => originalPrice * (1 - DiscountPercent / 100m);
    }
}
