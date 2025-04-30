namespace ShoppingCart.Core.Strategies
{
    public class RegularDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.95m;
    }
}