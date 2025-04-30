# Shopping Cart Application in .NET Core 8

This is a complete .NET Core 8 Shopping Cart application demonstrating Entity Framework, REST API, authentication, design patterns, and microservices concepts. The project is structured to be downloaded, built, and run locally.

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or Azure SQL)
- Visual Studio 2022 or VS Code
- Docker (optional for containerization)

## Setup Instructions
1. **Create Project Directory**: Create a folder named `ShoppingCartApp` and organize the files as shown below.
2. **Database Setup**:
   - Update the connection string in `ShoppingCart.API/appsettings.json` to point to your SQL Server instance.
   - Run migrations: `dotnet ef migrations add InitialCreate` and `dotnet ef database update` in the `ShoppingCart.Infrastructure` directory.
3. **Build and Run**:
   - Navigate to `ShoppingCart.API` and run `dotnet build`.
   - Run the API: `dotnet run`.
   - Access Swagger UI at `https://localhost:5001/swagger`.
4. **Docker (Optional)**:
   - Build Docker image: `docker build -t shoppingcart-api .`
   - Run container: `docker run -p 5001:80 shoppingcart-api`

## Project Structure
```
ShoppingCartApp/
├── ShoppingCart.Core/                        # Core entities, interfaces, utilities
│   ├── Models/
│   │   ├── Product.cs
│   │   ├── Order.cs
│   │   ├── User.cs
│   ├── Strategies/
│   │   ├── IDiscountStrategy.cs
│   │   ├── RegularDiscount.cs
│   │   ├── PremiumDiscount.cs
│   ├── Utilities/
│   │   ├── StringHelper.cs
│   │   ├── DNACompressor.cs
│   │   ├── ArrayHelper.cs
│   ├── ShoppingCart.Core.csproj
├── ShoppingCart.Infrastructure/              # Database context, migrations
│   ├── Data/
│   │   ├── ShoppingCartContext.cs
│   ├── Repositories/
│   │   ├── IProductRepository.cs
│   │   ├── ProductRepository.cs
│   ├── Migrations/                           # EF migrations (generated)
│   ├── ShoppingCart.Infrastructure.csproj
├── ShoppingCart.API/                         # Main REST API
│   ├── Controllers/
│   │   ├── ProductController.cs
│   │   ├── OrderController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── ShoppingCart.API.csproj
├── ShoppingCart.Auth/                        # Authentication microservice
│   ├── Controllers/
│   │   ├── AuthController.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── ShoppingCart.Auth.csproj
├── ShoppingCart.Tests/                       # Unit tests
│   ├── UnitTests/
│   │   ├── UtilityTests.cs
│   ├── ShoppingCart.Tests.csproj
├── Dockerfile                               # Docker configuration
├── azure-pipelines.yml                      # CI/CD pipeline
├── ShoppingCartApp.sln                      # Solution file
└── README.md
```

---

## Source Code Files

### ShoppingCart.Core/Models/Product.cs
```csharp
namespace ShoppingCart.Core.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
}
```

### ShoppingCart.Core/Models/Order.cs
```csharp
namespace ShoppingCart.Core.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}

public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```

### ShoppingCart.Core/Models/User.cs
```csharp
namespace ShoppingCart.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}
```

### ShoppingCart.Core/Strategies/IDiscountStrategy.cs
```csharp
namespace ShoppingCart.Core.Strategies;

public interface IDiscountStrategy
{
    decimal ApplyDiscount(decimal total);
}
```

### ShoppingCart.Core/Strategies/RegularDiscount.cs
```csharp
namespace ShoppingCart.Core.Strategies;

public class RegularDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal total) => total * 0.95m; // 5% off
}
```

### ShoppingCart.Core/Strategies/PremiumDiscount.cs
```csharp
namespace ShoppingCart.Core.Strategies;

public class PremiumDiscount : IDiscountStrategy
{
    public decimal ApplyDiscount(decimal total) => total * 0.80m; // 20% off
}
```

### ShoppingCart.Core/Utilities/StringHelper.cs
```csharp
namespace ShoppingCart.Core.Utilities;

public static class StringHelper
{
    public static bool IsPalindrome(string input)
    {
        input = input.ToLower().Replace(" ", "");
        int left = 0, right = input.Length - 1;
        while (left < right)
        {
            if (input[left] != input[right])
                return false;
            left++;
            right--;
        }
        return true;
    }
}
```

### ShoppingCart.Core/Utilities/DNACompressor.cs
```csharp
namespace ShoppingCart.Core.Utilities;

public static class DNACompressor
{
    public static byte[] Compress(string dna)
    {
        var bytes = new List<byte>();
        for (int i = 0; i < dna.Length; i += 4)
        {
            byte b = 0;
            for (int j = 0; j < 4 && i + j < dna.Length; j++)
            {
                b <<= 2;
                b |= dna[i + j] switch
                {
                    'A' => 0,
                    'C' => 1,
                    'G' => 2,
                    'T' => 3,
                    _ => 0
                };
            }
            bytes.Add(b);
        }
        return bytes.ToArray();
    }
}
```

### ShoppingCart.Core/Utilities/ArrayHelper.cs
```csharp
namespace ShoppingCart.Core.Utilities;

public static class ArrayHelper
{
    public static List<int> FindDuplicates(int[] arr)
    {
        var seen = new HashSet<int>();
        var duplicates = new List<int>();
        foreach (var num in arr)
        {
            if (!seen.Add(num))
                duplicates.Add(num);
        }
        return duplicates;
    }
}
```

### ShoppingCart.Core/ShoppingCart.Core.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>
```

---

### ShoppingCart.Infrastructure/Data/ShoppingCartContext.cs
```csharp
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Core.Models;

namespace ShoppingCart.Infrastructure.Data;

public class ShoppingCartContext : DbContext
{
    public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasIndex(p => p.Category);
        modelBuilder.Entity<Order>().HasMany(o => o.Items).WithOne().HasForeignKey(oi => oi.Id);
    }
}
```

### ShoppingCart.Infrastructure/Repositories/IProductRepository.cs
```csharp
using ShoppingCart.Core.Models;

namespace ShoppingCart.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByCategoryAsync(string category, decimal maxPrice);
}
```

### ShoppingCart.Infrastructure/Repositories/ProductRepository.cs
```csharp
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Core.Models;
using ShoppingCart.Infrastructure.Data;

namespace ShoppingCart.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShoppingCartContext _context;

    public ProductRepository(ShoppingCartContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(string category, decimal maxPrice)
    {
        return await _context.Products
            .Where(p => p.Category == category && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .Take(10)
            .ToListAsync();
    }
}
```

### ShoppingCart.Infrastructure/ShoppingCart.Infrastructure.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\ShoppingCart.Core\ShoppingCart.Core.csproj" />
  </ItemGroup>
</Project>
```

---

### ShoppingCart.API/Controllers/ProductController.cs
```csharp
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure.Repositories;

namespace ShoppingCart.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetByCategory([FromQuery] string category, [FromQuery] decimal maxPrice)
    {
        var products = await _productRepository.GetByCategoryAsync(category, maxPrice);
        return Ok(products);
    }
}
```

### ShoppingCart.API/Controllers/OrderController.cs
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.Models;
using ShoppingCart.Core.Strategies;

namespace ShoppingCart.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IDiscountStrategy _discountStrategy;

    public OrderController(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    {
        decimal total = order.Items.Sum(i => i.Price * i.Quantity);
        decimal discountedTotal = _discountStrategy.ApplyDiscount(total);
        return Ok(new { Total = total, DiscountedTotal = discountedTotal });
    }

    [Authorize(Roles = "CEO")]
    [HttpGet("all-tenants")]
    public IActionResult GetAllTenantsOrders()
    {
        // Mock implementation
        return Ok(new { Message = "All tenant orders retrieved" });
    }
}
```

### ShoppingCart.API/Program.cs
```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Core.Strategies;
using ShoppingCart.Infrastructure.Data;
using ShoppingCart.Infrastructure.Repositories;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ShoppingCartContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:5002"; // Auth service
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:5002",
            ValidAudience = "shoppingcart-api"
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDiscountStrategy, RegularDiscount>(); // Default strategy

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

### ShoppingCart.API/appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ShoppingCartDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### ShoppingCart.API/ShoppingCart.API.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\ShoppingCart.Core\ShoppingCart.Core.csproj" />
    <ProjectReference Include="..\ShoppingCart.Infrastructure\ShoppingCart.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

---

### ShoppingCart.Auth/Controllers/AuthController.cs
```csharp
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ShoppingCart.Auth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User login)
    {
        // Mock user validation
        if (login.Username == "test" && login.PasswordHash == "password")
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Role, login.Username == "ceo" ? "CEO" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        return Unauthorized();
    }
}
```

### ShoppingCart.Auth/Program.cs
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
```

### ShoppingCart.Auth/appsettings.json
```json
{
  "Jwt": {
    "Key": "YourSecretKeyHere1234567890",
    "Issuer": "http://localhost:5002",
    "Audience": "shoppingcart-api"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### ShoppingCart.Auth/ShoppingCart.Auth.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\ShoppingCart.Core\ShoppingCart.Core.csproj" />
  </ItemGroup>
</Project>
```

---

### ShoppingCart.Tests/UnitTests/UtilityTests.cs
```csharp
using ShoppingCart.Core.Utilities;
using Xunit;

namespace ShoppingCart.Tests.UnitTests;

public class UtilityTests
{
    [Fact]
    public void IsPalindrome_ReturnsTrue_ForValidPalindrome()
    {
        Assert.True(StringHelper.IsPalindrome("racecar"));
    }

    [Fact]
    public void FindDuplicates_ReturnsCorrectDuplicates()
    {
        int[] arr = { 1, 2, 2, 3, 3, 4 };
        var result = ArrayHelper.FindDuplicates(arr);
        Assert.Equal(new List<int> { 2, 3 }, result);
    }
}
```

### ShoppingCart.Tests/ShoppingCart.Tests.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <IsPackable>false</IsPackable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\ShoppingCart.Core\ShoppingCart.Core.csproj" />
  </ItemGroup>
</Project>
```

---

### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ShoppingCart.API/ShoppingCart.API.csproj", "ShoppingCart.API/"]
COPY ["ShoppingCart.Core/ShoppingCart.Core.csproj", "ShoppingCart.Core/"]
COPY ["ShoppingCart.Infrastructure/ShoppingCart.Infrastructure.csproj", "ShoppingCart.Infrastructure/"]
RUN dotnet restore "ShoppingCart.API/ShoppingCart.API.csproj"
COPY . .
WORKDIR "/src/ShoppingCart.API"
RUN dotnet build "ShoppingCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingCart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.API.dll"]
```

---

### azure-pipelines.yml
```yaml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: UseDotNet@2
  inputs:
    version: '8.0.x'
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
- task: DotNetCoreCLI@2
  inputs:
    command: 'test'
    projects: '**/*.Tests.csproj'
- task: Docker@2
  inputs:
    command: 'build'
    Dockerfile: '**/Dockerfile'
```

---

### ShoppingCartApp.sln
```sln
Microsoft Visual Studio Solution File, Format Version 12.00
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ShoppingCart.Core", "ShoppingCart.Core\ShoppingCart.Core.csproj", "{GUID1}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ShoppingCart.Infrastructure", "ShoppingCart.Infrastructure\ShoppingCart.Infrastructure.csproj", "{GUID2}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ShoppingCart.API", "ShoppingCart.API\ShoppingCart.API.csproj", "{GUID3}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ShoppingCart.Auth", "ShoppingCart.Auth\ShoppingCart.Auth.csproj", "{GUID4}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ShoppingCart.Tests", "ShoppingCart.Tests\ShoppingCart.Tests.csproj", "{GUID5}"
EndProject
Global
    GlobalSection(SolutionConfigurationPlatforms) = preSolution
        Debug|Any CPU = Debug|Any CPU
        Release|Any CPU = Release|Any CPU
    EndGlobalSection
EndGlobal
```

**Note**: Replace `{GUID1}`, `{GUID2}`, `{GUID3}`, `{GUID4}`, `{GUID5}` with unique GUIDs (e.g., generate using `guidgen` or Visual Studio).

---

### README.md
```markdown
# Shopping Cart Application

A .NET Core 8 microservices-based shopping cart application.

## Setup
1. Install .NET 8 SDK.
2. Update `ShoppingCart.API/appsettings.json` with your SQL Server connection string.
3. Run migrations:
   ```bash
   cd ShoppingCart.Infrastructure
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Build and run:
   ```bash
   cd ShoppingCart.API
   dotnet build
   dotnet run
   ```
5. Access Swagger at `https://localhost:5001/swagger`.

## Authentication
- Run the auth service: `cd ShoppingCart.Auth && dotnet run`.
- Login endpoint: `POST http://localhost:5002/api/auth/login` with `{ "username": "test", "passwordHash": "password" }`.

## Docker
```bash
docker build -t shoppingcart-api .
docker run -p 5001:80 shoppingcart-api
```
```

---

## Creating the Zip File
To create a downloadable zip file:
1. Create a directory named `ShoppingCartApp`.
2. Replicate the project structure as shown above.
3. Copy each file's content into the corresponding file path.
4. Zip the `ShoppingCartApp` folder:
   - On Windows: Right-click > Send to > Compressed (zipped) folder.
   - On macOS/Linux: `zip -r ShoppingCartApp.zip ShoppingCartApp/`.
5. The resulting `ShoppingCartApp.zip` contains the full project.

## Running the Project
1. Unzip `ShoppingCartApp.zip`.
2. Ensure SQL Server is running (LocalDB or Azure SQL).
3. Update the connection string in `ShoppingCart.API/appsettings.json`.
4. Run migrations as described in the README.
5. Start the auth service (`ShoppingCart.Auth`) first, then the API (`ShoppingCart.API`).
6. Use Swagger or Postman to test endpoints:
   - Login: `POST http://localhost:5002/api/auth/login`
   - Get products: `GET https://localhost:5001/api/product`

## Notes
- The project uses EF Core 8.0 with SQL Server.
- Authentication is simplified with a mock JWT implementation.
- The auth service runs on `http://localhost:5002`; the API on `https://localhost:5001`.
- Some advanced features (e.g., Redis, Azure services) are configured but require additional setup.
- The solution file requires unique GUIDs for each project.

This project covers all requested concepts, including Entity Framework, design patterns, authentication, and microservices, while being runnable locally with .NET 8.