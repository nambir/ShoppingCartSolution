# Entity Framework Core Interview Q&A – ShoppingCartApp

---

## 1. Difference between `First()` and `FirstOrDefault()`

| Feature            | `First()`                       | `FirstOrDefault()`                  |
|-------------------|----------------------------------|-------------------------------------|
| Return Behavior   | Returns the first element        | Returns the first element or `null` |
| Exception         | Throws exception if not found    | Returns `null` if not found         |
| Use When          | You’re sure data exists          | Data may or may not exist           |

```csharp
var product = _context.Products.First(); // Throws if empty
var productSafe = _context.Products.FirstOrDefault(); // Returns null if none
```

---

## 2. Difference between `IEnumerable` and `IQueryable`

| Feature           | `IEnumerable`                 | `IQueryable`                        |
|------------------|-------------------------------|-------------------------------------|
| Execution        | Client-side                   | Server-side                         |
| Performance      | Loads all data in memory      | Translates to SQL query             |
| Use Case         | In-memory processing (LINQ)   | Database queries (EF Core)          |

```csharp
IEnumerable<Product> local = _context.Products.ToList(); // All fetched to memory
IQueryable<Product> db = _context.Products.Where(p => p.Price > 100); // SQL generated
```

---

## 3. What is reflection in C#?

Reflection lets you inspect metadata (types, properties, methods) at runtime.

```csharp
var type = typeof(Product);
foreach (var prop in type.GetProperties())
{
    Console.WriteLine($"Property: {prop.Name}, Type: {prop.PropertyType}");
}
```

---

## 4. How to connect local DB using Azure?

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:yourserver.database.windows.net;Initial Catalog=ShoppingCartDb;User ID=admin;Password=yourPwd;"
}
```

---

## 5. How to improve performance of a stored procedure?

- Use indexed fields in `WHERE`
- Avoid `SELECT *`
- Use `SET NOCOUNT ON`
- Use `WITH (NOLOCK)` carefully

---

## 6. How do you optimize SQL performance?

- Indexing, joins, pagination
- Avoid nested queries
- Analyze with SQL Execution Plan

---

## 7. Difference: Clustered vs Non-Clustered Index

| Feature             | Clustered Index                | Non-Clustered Index               |
|--------------------|--------------------------------|-----------------------------------|
| Data Storage       | Data rows sorted with index    | Separate structure                |
| Count per table    | One                            | Many                              |
| Use Case           | Primary Key                    | Secondary search optimization     |

---

## 8. How to apply DB changes in EF Core?

```bash
dotnet ef migrations add AddProductTable
dotnet ef database update
```

---

## 9. What EF version have you used?

```bash
dotnet list package | grep Microsoft.EntityFrameworkCore
```

---

## 10. How to get data from SharePoint in .NET Core?

```csharp
var ctx = new ClientContext("https://yourdomain.sharepoint.com/sites/site");
ctx.Credentials = new SharePointOnlineCredentials(username, securePassword);
var list = ctx.Web.Lists.GetByTitle("Products");
ctx.Load(list);
ctx.ExecuteQuery();
```

---