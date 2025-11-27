# Clean Architecture API - Products Documentation

## Overview
This API provides complete CRUD operations for Product management following Clean Architecture principles with CQRS pattern using MediatR.

## Architecture Layers

### 1. **API Layer (Presentation)**
- **ProductsController**: RESTful API endpoints
- **Request Models**: UpdateProductRequest, UpdateStockRequest
- **Global Exception Handling**
- **Swagger/OpenAPI Documentation**

### 2. **Application Layer**
- **Commands**: CreateProduct, UpdateProduct, DeleteProduct, UpdateStock, Activate, Deactivate
- **Queries**: GetProducts, GetProductById, SearchProducts, GetProductsByCategory
- **Handlers**: Command and query handlers implementing business logic
- **Validators**: FluentValidation for all commands
- **DTOs**: ProductDto, PagedResponse
- **Pipeline Behaviors**: ValidationBehavior, LoggingBehavior

### 3. **Domain Layer**
- **Entities**: Product (with business logic)
- **Value Objects**: Money
- **Repositories**: IProductRepository
- **Domain Events**: Product domain events
- **Exceptions**: Domain-specific exceptions

### 4. **Infrastructure Layer**
- **Repositories**: ProductRepository implementation
- **DbContext**: ApplicationDbContext with EF Core
- **Services**: EmailService, CacheService, EventBus
- **Interceptors**: AuditableEntityInterceptor, DomainEventDispatcherInterceptor

## API Endpoints

### Base URL
```
https://localhost:7001/api/products
```

### Endpoints

#### 1. Get All Products
```http
GET /api/products?page=1&pageSize=10
```
**Response**: Paginated list of products
```json
{
  "items": [
    {
      "id": "guid",
      "name": "Product Name",
      "description": "Description",
      "price": 29.99,
      "currency": "USD",
      "stockQuantity": 100,
      "category": "Electronics",
      "isActive": true
    }
  ],
  "page": 1,
  "pageSize": 10,
  "totalCount": 50,
  "totalPages": 5
}
```

#### 2. Get Product By ID
```http
GET /api/products/{id}
```
**Response**: Single product details

#### 3. Create Product
```http
POST /api/products
Content-Type: application/json

{
  "name": "Wireless Mouse",
  "description": "Ergonomic wireless mouse",
  "price": 29.99,
  "currency": "USD",
  "stockQuantity": 100,
  "category": "Electronics"
}
```
**Response**: 201 Created with product ID in Location header

#### 4. Update Product
```http
PUT /api/products/{id}
Content-Type: application/json

{
  "name": "Updated Name",
  "description": "Updated Description",
  "price": 39.99,
  "currency": "USD",
  "category": "Electronics"
}
```
**Response**: 204 No Content

#### 5. Update Stock
```http
PATCH /api/products/{id}/stock
Content-Type: application/json

{
  "quantity": 50
}
```
**Response**: 204 No Content

#### 6. Activate Product
```http
PATCH /api/products/{id}/activate
```
**Response**: 204 No Content

#### 7. Deactivate Product
```http
PATCH /api/products/{id}/deactivate
```
**Response**: 204 No Content

#### 8. Delete Product
```http
DELETE /api/products/{id}
```
**Response**: 204 No Content

#### 9. Search Products
```http
GET /api/products/search?searchTerm=mouse&page=1&pageSize=10
```
**Response**: Paginated search results

#### 10. Get Products By Category
```http
GET /api/products/category/Electronics?page=1&pageSize=10
```
**Response**: Paginated products in category

## Validation Rules

### CreateProduct & UpdateProduct
- **Name**: Required, 3-200 characters
- **Description**: Required, max 2000 characters
- **Price**: Greater than 0, less than 1,000,000
- **Currency**: Required, 3-letter uppercase ISO code (USD, EUR, etc.)
- **StockQuantity** (Create only): >= 0, < 1,000,000
- **Category**: Required, max 100 characters

### UpdateStock
- **Quantity**: Can be negative (for reducing stock), < 1,000,000 in absolute value

## Status Codes

- **200 OK**: Successful GET requests
- **201 Created**: Successful POST (resource created)
- **204 No Content**: Successful PUT/PATCH/DELETE
- **400 Bad Request**: Validation errors
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Unexpected errors

## Error Response Format
```json
{
  "error": "Error message",
  "errors": ["Validation error 1", "Validation error 2"]
}
```

## Health Check
```http
GET /health
```
Checks:
- Database connectivity
- Cache service
- EF DbContext

## Features

### Clean Architecture Benefits
✅ **Separation of Concerns**: Each layer has distinct responsibilities
✅ **Testability**: Easy to unit test with dependency injection
✅ **Maintainability**: Changes in one layer don't affect others
✅ **Scalability**: Easy to add new features
✅ **Flexibility**: Can swap implementations (e.g., database, cache)

### CQRS Pattern
✅ **Separation**: Commands (write) separate from Queries (read)
✅ **Optimization**: Queries optimized for reading
✅ **Clarity**: Clear intent of operations

### Pipeline Behaviors
✅ **Validation**: Automatic validation before handler execution
✅ **Logging**: Comprehensive logging of all operations
✅ **Exception Handling**: Centralized error handling

### Domain-Driven Design
✅ **Rich Domain Model**: Business logic in domain entities
✅ **Value Objects**: Money with currency
✅ **Domain Events**: Event-driven architecture
✅ **Repository Pattern**: Data access abstraction

## Running the API

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)

### Steps
1. **Update Connection String** in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

2. **Run Migrations**
```bash
dotnet ef database update --project src/CleanArchitecture.Infrastructure --startup-project src/Presentation/CleanArchitecture.API
```

3. **Run the API**
```bash
cd src/Presentation/CleanArchitecture.API
dotnet run
```

4. **Access Swagger UI**
```
https://localhost:7001
```

5. **Test with HTTP file**
Use `Products.http` file in VS Code with REST Client extension

## Testing

### Manual Testing
Use the provided `Products.http` file with VS Code REST Client extension

### Integration Testing
```bash
cd tests/CleanArchitecture.IntegrationTests
dotnet test
```

### Unit Testing
```bash
cd tests/CleanArchitecture.UnitTests
dotnet test
```

## Future Enhancements
- [ ] Authentication & Authorization (JWT)
- [ ] Rate Limiting
- [ ] Response Caching
- [ ] API Versioning
- [ ] Bulk Operations
- [ ] Export to Excel/PDF
- [ ] Product Images Support
- [ ] Product Reviews & Ratings
- [ ] Advanced Filtering & Sorting
- [ ] Real-time Notifications (SignalR)

## Support
For issues or questions, contact: support@cleanarchitecture.com
