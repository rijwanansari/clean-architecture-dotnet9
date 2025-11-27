# Quick Start Guide - Products API

## 🚀 Getting Started

### 1. Build the Solution
```bash
dotnet build CleanArchitecture.sln
```

### 2. Update Database
```bash
dotnet ef database update --project src/CleanArchitecture.Infrastructure --startup-project src/Presentation/CleanArchitecture.API
```

### 3. Run the API
```bash
cd src/Presentation/CleanArchitecture.API
dotnet run
```

### 4. Access the API
- **Swagger UI**: https://localhost:7001
- **Health Check**: https://localhost:7001/health
- **API Base URL**: https://localhost:7001/api

## 📋 Testing the API

### Option 1: Swagger UI (Recommended for First-Time)
1. Open browser to `https://localhost:7001`
2. You'll see all available endpoints
3. Click "Try it out" on any endpoint
4. Fill in the parameters and click "Execute"

### Option 2: VS Code REST Client
1. Install "REST Client" extension in VS Code
2. Open `Products.http` file
3. Click "Send Request" above any HTTP request

### Option 3: cURL Examples

**Create a Product:**
```bash
curl -X POST https://localhost:7001/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Wireless Mouse",
    "description": "Ergonomic wireless mouse",
    "price": 29.99,
    "currency": "USD",
    "stockQuantity": 100,
    "category": "Electronics"
  }'
```

**Get All Products:**
```bash
curl https://localhost:7001/api/products?page=1&pageSize=10
```

**Get Product By ID:**
```bash
curl https://localhost:7001/api/products/{product-id}
```

**Search Products:**
```bash
curl "https://localhost:7001/api/products/search?searchTerm=mouse&page=1&pageSize=10"
```

**Update Product:**
```bash
curl -X PUT https://localhost:7001/api/products/{product-id} \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Updated Name",
    "description": "Updated Description",
    "price": 39.99,
    "currency": "USD",
    "category": "Electronics"
  }'
```

**Delete Product:**
```bash
curl -X DELETE https://localhost:7001/api/products/{product-id}
```

## 🏗️ Architecture Overview

```
┌─────────────────────────────────────────────┐
│          API Layer (Presentation)            │
│  Controllers, Request/Response Models        │
└──────────────────┬──────────────────────────┘
                   │ HTTP
┌──────────────────▼──────────────────────────┐
│         Application Layer (CQRS)             │
│  Commands, Queries, Handlers, Validators    │
└──────────────────┬──────────────────────────┘
                   │ MediatR
┌──────────────────▼──────────────────────────┐
│            Domain Layer                      │
│  Entities, Value Objects, Business Rules     │
└──────────────────┬──────────────────────────┘
                   │ Repositories
┌──────────────────▼──────────────────────────┐
│        Infrastructure Layer                  │
│  EF Core, SQL Server, External Services      │
└─────────────────────────────────────────────┘
```

## 📁 Project Structure

```
CleanArchitecture.API/
├── Controllers/
│   └── ProductsController.cs    # REST API endpoints
├── Program.cs                     # DI configuration & middleware
├── Products.http                  # API test file
└── appsettings.json              # Configuration

CleanArchitecture.Application/
├── Products/
│   ├── Commands/                 # Write operations
│   │   ├── CreateProduct/
│   │   ├── UpdateProduct/
│   │   ├── DeleteProduct/
│   │   ├── UpdateProductStock/
│   │   ├── ActivateProduct/
│   │   └── DeactivateProduct/
│   └── Queries/                  # Read operations
│       ├── GetProducts/
│       ├── GetProductById/
│       ├── SearchProducts/
│       └── GetProductsByCategory/
├── DTOs/                         # Data transfer objects
├── Behaviors/                    # Pipeline behaviors
└── DependencyInjection.cs       # Service registration

CleanArchitecture.Domain/
├── Entities/
│   └── Product.cs               # Rich domain model
├── ValueObjects/
│   └── Money.cs                 # Currency with amount
└── Repositories/
    └── IProductRepository.cs    # Repository contract

CleanArchitecture.Infrastructure/
├── Data/
│   ├── Repositories/
│   │   └── ProductRepository.cs # EF Core implementation
│   └── DataContext/
│       └── ApplicationDbContext.cs
└── DependencyInjection.cs      # Infrastructure services
```

## ✅ Available Operations

### Product Management
- ✅ Create Product
- ✅ Get All Products (Paginated)
- ✅ Get Product By ID
- ✅ Update Product
- ✅ Delete Product
- ✅ Search Products
- ✅ Get Products By Category
- ✅ Update Stock Quantity
- ✅ Activate Product
- ✅ Deactivate Product

### Features
- ✅ Clean Architecture
- ✅ CQRS Pattern with MediatR
- ✅ FluentValidation
- ✅ Repository Pattern
- ✅ Unit of Work
- ✅ Pipeline Behaviors (Logging, Validation)
- ✅ Domain Events
- ✅ Value Objects
- ✅ Global Exception Handling
- ✅ Swagger/OpenAPI Documentation
- ✅ Health Checks
- ✅ Dependency Injection
- ✅ Async/Await
- ✅ EF Core with SQL Server

## 🔧 Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🧪 Sample Data

Run these in Swagger UI or using `Products.http` file to create test data:

1. **Wireless Mouse** - Electronics, $29.99
2. **Mechanical Keyboard** - Electronics, $129.99
3. **USB-C Cable** - Accessories, $14.99
4. **Laptop Stand** - Accessories, $49.99
5. **Webcam HD** - Electronics, $79.99

## 📊 Validation Rules

### Product Creation
- **Name**: 3-200 characters, required
- **Description**: Max 2000 characters, required
- **Price**: > 0 and < 1,000,000
- **Currency**: 3-letter uppercase code (USD, EUR, GBP, etc.)
- **Stock**: >= 0 and < 1,000,000
- **Category**: Max 100 characters, required

## 🎯 Next Steps

1. **Explore Swagger UI** - Interactive API documentation
2. **Test with Products.http** - Pre-configured HTTP requests
3. **Check Health Endpoint** - `/health` for system status
4. **Review Logs** - Check console for detailed logging
5. **Read API-DOCUMENTATION.md** - Complete API documentation

## 🆘 Troubleshooting

### Port Already in Use
Change port in `Properties/launchSettings.json` or use:
```bash
dotnet run --urls "https://localhost:7002"
```

### Database Connection Issues
1. Check SQL Server is running
2. Verify connection string in `appsettings.json`
3. Run `dotnet ef database update` again

### Build Errors
```bash
dotnet clean
dotnet restore
dotnet build
```

## 📚 Additional Resources

- **API Documentation**: See `API-DOCUMENTATION.md`
- **HTTP Test File**: See `Products.http`
- **Swagger UI**: Available when app is running at root URL

---

**Happy Coding! 🎉**
