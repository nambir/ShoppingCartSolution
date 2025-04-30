// O: Implements a regular discount strategy (5%)
using ShoppingCart.Core.Interfaces;

namespace ShoppingCart.Core.Services
{
    public class RegularDiscount : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.95m;
    }
}