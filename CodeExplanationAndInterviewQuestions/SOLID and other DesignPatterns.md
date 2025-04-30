# SOLID Principles & Design Patterns – ShoppingCartApp

---


    IDiscountStrategy, RegularDiscount, PremiumDiscount (Open-Closed & Strategy Pattern)

    CartService to compute total (Dependency Inversion)

    IMessageSender + EmailSender / SmsSender (Interface Segregation & DIP)

Use these services via constructor injection in your API or services layer.

## 1. Explain SOLID Principles with Examples

**S – Single Responsibility**  
Each class should have only one responsibility.

```csharp
public class InvoiceGenerator
{
    public void Generate() { /* generate invoice logic */ }
}

public class EmailService
{
    public void SendEmail(string to, string subject) { /* send email */ }
}
```

**O – Open/Closed**  
Software should be open for extension but closed for modification.

```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal total);
}

public class RegularDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal total) => total * 0.95m;
}

public class PremiumDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal total) => total * 0.90m;
}
```

**L – Liskov Substitution**  
Derived classes must be substitutable for base classes.

```csharp
public class Product { public virtual decimal GetPrice() => 100; }
public class DiscountedProduct : Product { public override decimal GetPrice() => 80; }
```

**I – Interface Segregation**  
Clients should not be forced to implement interfaces they don’t use.

```csharp
public interface IOnlineOrder
{
    void ProcessOrder();
}

public interface ICashOnDelivery
{
    void PayOnDelivery();
}
```

**D – Dependency Inversion**  
Depend on abstractions, not concretions.

```csharp
public interface IMessageSender
{
    void Send(string message);
}

public class EmailSender : IMessageSender
{
    public void Send(string message) => Console.WriteLine("Email: " + message);
}
```

---

## 2. What is Factory Design Pattern and Code

Used to delegate object creation logic to factory classes.

```csharp
public interface IProductFactory
{
    Product CreateProduct();
}

public class ElectronicsFactory : IProductFactory
{
    public Product CreateProduct() => new Product { Name = "Laptop", Price = 1000 };
}
```

---

## 3. What is Open-Close Principle?

Your class should be extendable without changing existing code.

✅ Add a new class that implements an interface  
❌ Don't modify the existing class to add logic

---

## 4. Interface Segregation Principle

Split large interfaces into smaller ones.

```csharp
public interface IPrintable { void Print(); }
public interface IScannable { void Scan(); }

public class Printer : IPrintable
{
    public void Print() { }
}
```

---

## 5. Strategy Pattern Explanation (Used in ShoppingCartApp)

Behavior can be selected at runtime.

```csharp
public interface IDiscountStrategy { decimal ApplyDiscount(decimal total); }

public class NoDiscount : IDiscountStrategy { public decimal ApplyDiscount(decimal t) => t; }

public class RegularDiscount : IDiscountStrategy { public decimal ApplyDiscount(decimal t) => t * 0.95m; }

public class Cart
{
    private readonly IDiscountStrategy _discount;
    public Cart(IDiscountStrategy discount) { _discount = discount; }
    public decimal Checkout(decimal amount) => _discount.ApplyDiscount(amount);
}
```

---

## 6. Most Used Design Patterns in Projects

- **Strategy** – for dynamic behaviors (discounts)
- **Factory** – for object creation
- **Repository** – for data access
- **CQRS** – separation of read/write logic
- **Decorator** – for layering (e.g., logging)
- **Mediator** – like MediatR for commands/queries

---

## 7. CQRS, BFF, Choreography Explanation

### CQRS – Command Query Responsibility Segregation

- Commands (Write): `CreateOrderCommand`
- Queries (Read): `GetOrdersQuery`
- Mediator separates intent

### BFF – Backend For Frontend

- Custom API per UI
- Mobile App ↔ `MobileBFF`
- Admin Panel ↔ `AdminBFF`

### Choreography – Event-Based Communication

- Services respond to events without a central orchestrator
- e.g., `OrderPlaced` event triggers `Inventory`, `Notification`, `Billing` services independently

---