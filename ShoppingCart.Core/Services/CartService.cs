// D: Depends on the IDiscountStrategy abstraction (DIP)
// S: Only responsible for calculating the cart total
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Models;

namespace ShoppingCart.Core.Services
{
    public class CartService
    {
        private readonly IDiscountStrategy _discountStrategy;

        public CartService(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        public decimal CalculateTotal(List<Product> products)
        {
            var total = products.Sum(p => p.Price * p.Stock);
            return _discountStrategy.ApplyDiscount(total); // Apply discount without changing this class
        }
    }
}