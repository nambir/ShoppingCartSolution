// O: Implements a premium discount strategy (10%)
using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Services
{
    public class PremiumDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.90m;
    }
}