# SOLID Principles – Explained with Code and Comments

---

## S – Single Responsibility Principle (SRP)
Each class should have only one reason to change. One class = One job.

### ✅ Good Example:

```csharp
// Responsible only for generating invoice
public class InvoiceGenerator
{
    public void GenerateInvoice()
    {
        // Logic to generate invoice
    }
}

// Responsible only for sending email
public class EmailService
{
    public void SendEmail(string recipient, string content)
    {
        // Logic to send email
    }
}
```

---

## O – Open/Closed Principle (OCP)
Software entities should be open for extension, but closed for modification.

### ✅ In ShoppingCartApp:

```csharp
// Interface allows for extending different strategies without changing existing logic
public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal totalAmount);
}

// Extends discount functionality without touching base code
public class RegularDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.95m;
}

public class PremiumDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.90m;
}
```

---

## L – Liskov Substitution Principle (LSP)
Subtypes must be substitutable for their base types.

### ✅ Example:

```csharp
public class Product
{
    public virtual decimal GetPrice() => 100;
}

public class DiscountedProduct : Product
{
    public override decimal GetPrice() => 80;
}

// Works with both Product and DiscountedProduct
Product p = new DiscountedProduct();
Console.WriteLine(p.GetPrice());
```

---

## I – Interface Segregation Principle (ISP)
Clients should not be forced to implement interfaces they don’t use.

### ✅ Example:

```csharp
public interface IEmailSender
{
    void SendEmail(string to, string content);
}

public interface ISmsSender
{
    void SendSms(string to, string content);
}

public class EmailService : IEmailSender
{
    public void SendEmail(string to, string content) { /* send email */ }
}
```

---

## D – Dependency Inversion Principle (DIP)
High-level modules should not depend on low-level modules. Use abstractions.

### ✅ Example from ShoppingCartApp:

```csharp
// Abstraction
public interface IMessageSender
{
    void Send(string to, string content);
}

// Concrete Implementation
public class EmailSender : IMessageSender
{
    public void Send(string to, string content)
    {
        Console.WriteLine($"Sending Email to {to}: {content}");
    }
}

// High-level class depends on abstraction, not concrete class
public class NotificationService
{
    private readonly IMessageSender _sender;

    public NotificationService(IMessageSender sender)
    {
        _sender = sender;
    }

    public void NotifyUser(string to, string message)
    {
        _sender.Send(to, message);
    }
}
```

---

By applying these principles:
- Your code becomes **modular**, **testable**, and **scalable**
- Changes won't break existing functionality
- Encourages **clean architecture** and **maintainability**

---