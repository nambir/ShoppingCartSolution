// O: Open/Closed Principle - Interface for discount strategy to allow easy extension
namespace ShoppingCart.Core.Interfaces
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal totalAmount);
    }
}