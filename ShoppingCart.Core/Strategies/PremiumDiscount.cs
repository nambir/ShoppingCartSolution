namespace ShoppingCart.Core.Strategies
{
    public class PremiumDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.90m;
    }
}