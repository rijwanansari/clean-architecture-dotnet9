# 🏗️ Clean Architecture with .NET 9

[![.NET Version](https://img.shields.io/badge/.NET-9.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()

A comprehensive **full-stack** implementation of **Clean Architecture** principles with **.NET 9** and **Blazor Server**, demonstrating enterprise-level software design patterns, CQRS, Domain-Driven Design (DDD), and best practices for building maintainable, scalable, and testable applications with modern web UI.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Clean Architecture Explained](#-clean-architecture-explained)
- [Project Structure](#-project-structure)
- [Technologies Used](#-technologies-used)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Database Schema](#-database-schema)
- [Design Patterns](#-design-patterns)
- [Features](#-features)
- [Testing](#-testing)
- [Roadmap](#-roadmap)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🎯 Overview

This project is a **production-ready full-stack** implementation of Clean Architecture that showcases how to build modern web applications with:

- **Separation of Concerns**: Each layer has a distinct responsibility
- **Testability**: Easy to write unit and integration tests
- **Maintainability**: Changes in one layer don't ripple to others
- **Scalability**: Easy to add new features without breaking existing code
- **Flexibility**: Swap implementations (e.g., databases, external services) with minimal changes
- **Modern UI**: Blazor Server with interactive components and real-time updates

### What You'll Find Here

#### **Backend (REST API)**
✅ **Complete E-Commerce Domain**: Products, Orders, Customers with full CRUD operations  
✅ **CQRS Pattern**: Commands and Queries separated using MediatR  
✅ **Domain-Driven Design**: Rich domain models with business logic encapsulation  
✅ **Repository Pattern**: Abstract data access with Unit of Work  
✅ **FluentValidation**: Comprehensive input validation  
✅ **Pipeline Behaviors**: Cross-cutting concerns (logging, validation, caching)  
✅ **Entity Framework Core**: Code-first approach with migrations  
✅ **RESTful API**: Best practices for API design  
✅ **Swagger/OpenAPI**: Interactive API documentation  
✅ **Health Checks**: Monitor application and dependencies  
✅ **Background Services**: Asynchronous processing  
✅ **Domain Events**: Event-driven architecture  

#### **Frontend (Blazor Web UI)**
✅ **Blazor Server**: Interactive server-side rendering with SignalR  
✅ **Component-Based Architecture**: Reusable Razor components  
✅ **Real-Time Updates**: Automatic UI synchronization  
✅ **Form Validation**: Client-side validation with DataAnnotations  
✅ **Responsive Design**: Bootstrap 5 styling  
✅ **API Client Pattern**: Typed HTTP client for backend communication  
✅ **Product Management UI**: Full CRUD operations with search and pagination  
✅ **Modal Dialogs**: Modern UX for create/edit operations  
✅ **Error Handling**: User-friendly error messages and feedback  

---

## 🏛️ Clean Architecture Explained

Clean Architecture, introduced by Robert C. Martin (Uncle Bob), is an architectural pattern that emphasizes separation of concerns and dependency inversion.

### The Core Principle

**"Dependency Rule"**: Source code dependencies must point inward. Inner circles know nothing about outer circles.

```
┌─────────────────────────────────────────────────────┐
│                   Frameworks & Drivers               │
│         (Web API, UI, External Interfaces)           │
│  ┌───────────────────────────────────────────────┐  │
│  │          Interface Adapters                   │  │
│  │    (Controllers, Presenters, Gateways)        │  │
│  │  ┌─────────────────────────────────────────┐ │  │
│  │  │         Application Business Rules       │ │  │
│  │  │    (Use Cases, Commands, Queries)        │ │  │
│  │  │  ┌───────────────────────────────────┐  │ │  │
│  │  │  │   Enterprise Business Rules       │  │ │  │
│  │  │  │  (Entities, Value Objects, DDD)   │  │ │  │
│  │  │  │                                   │  │ │  │
│  │  │  └───────────────────────────────────┘  │ │  │
│  │  └─────────────────────────────────────────┘ │  │
│  └───────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────┘
```

### Our Implementation Layers

```
┌──────────────────────────────────────────────────────────┐
│  📱 Presentation Layer                                    │
│  ┌──────────────────┐  ┌──────────────────────────────┐ │
│  │  Blazor Web UI   │  │      REST API                │ │
│  │  • Razor Pages   │  │  • REST Controllers          │ │
│  │  • Components    │  │  • Request/Response Models   │ │
│  │  • API Client    │  │  • Middleware & Filters      │ │
│  │  • Bootstrap 5   │  │  • Swagger/OpenAPI           │ │
│  └──────────────────┘  └──────────────────────────────┘ │
└──────────────────────────┬───────────────────────────────┘
                           │ Depends on ↓
┌────────────────────▼─────────────────────────────────────┐
│  📋 Application Layer                                     │
│  • Use Cases (CQRS Commands & Queries)                   │
│  • DTOs & ViewModels                                      │
│  • Validators (FluentValidation)                          │
│  • Pipeline Behaviors                                     │
│  • Interface Abstractions                                 │
└────────────────────┬─────────────────────────────────────┘
                     │ Depends on ↓
┌────────────────────▼─────────────────────────────────────┐
│  💼 Domain Layer (Core)                                   │
│  • Entities (Rich Domain Models)                          │
│  • Value Objects                                          │
│  • Domain Events                                          │
│  • Repository Interfaces                                  │
│  • Business Logic & Rules                                 │
│  • Domain Exceptions                                      │
└────────────────────▲─────────────────────────────────────┘
                     │ Implemented by ↑
┌────────────────────┴─────────────────────────────────────┐
│  🔧 Infrastructure Layer                                  │
│  • EF Core DbContext                                      │
│  • Repository Implementations                             │
│  • External Service Integrations                          │
│  • File Storage, Email, Cache                             │
│  • Background Services                                    │
└──────────────────────────────────────────────────────────┘
```

### Why Clean Architecture?

| Benefit | Description |
|---------|-------------|
| 🧪 **Testability** | Business logic isolated from frameworks, easy to unit test |
| 🔄 **Flexibility** | Swap databases, UI frameworks, or external services easily |
| 🎯 **Focus** | Business rules are clear and not mixed with infrastructure |
| 📈 **Scalability** | Add features without breaking existing functionality |
| 🛡️ **Maintainability** | Changes in outer layers don't affect inner layers |
| 👥 **Team Work** | Multiple developers can work on different layers simultaneously |

---

## 📂 Project Structure

```
CleanArchitecture.sln
│
├── src/
│   ├── CleanArchitecture.Domain/                    # 💼 Core Business Logic (Heart of the Application)
│   │   ├── Common/
│   │   │   ├── BaseEntity.cs                       # Base class for all entities
│   │   │   ├── BaseAuditableEntity.cs              # Auditing support
│   │   │   └── IDomainEvent.cs                     # Domain event interface
│   │   ├── Entities/
│   │   │   ├── Product.cs                          # Product aggregate root
│   │   │   ├── Order.cs                            # Order aggregate root
│   │   │   ├── OrderItem.cs                        # Order line items
│   │   │   └── Customer.cs                         # Customer aggregate root
│   │   ├── ValueObjects/
│   │   │   ├── Money.cs                            # Value object for currency
│   │   │   ├── Address.cs                          # Value object for addresses
│   │   │   └── Email.cs                            # Value object for emails
│   │   ├── Enumerators/
│   │   │   ├── OrderStatus.cs                      # Order state machine
│   │   │   └── PaymentMethod.cs                    # Payment types
│   │   ├── Repositories/                           # Repository contracts
│   │   │   ├── IProductRepository.cs
│   │   │   ├── IOrderRepository.cs
│   │   │   └── ICustomerRepository.cs
│   │   ├── DomainEvents/
│   │   │   ├── OrderCreatedEvent.cs
│   │   │   └── OrderCompletedEvent.cs
│   │   └── Exceptions/
│   │       ├── DomainException.cs
│   │       ├── ProductNotFoundException.cs
│   │       └── InsufficientStockException.cs
│   │
│   ├── CleanArchitecture.Application/              # 📋 Use Cases & Business Logic
│   │   ├── DTOs/                                   # Data Transfer Objects
│   │   │   ├── Common/
│   │   │   │   ├── PagedResponse.cs
│   │   │   │   ├── MoneyDto.cs
│   │   │   │   └── AddressDto.cs
│   │   │   ├── Products/
│   │   │   │   └── ProductDto.cs
│   │   │   ├── Orders/
│   │   │   │   ├── OrderDto.cs
│   │   │   │   ├── OrderDetailsDto.cs
│   │   │   │   └── OrderItemDetailsDto.cs
│   │   │   └── Customers/
│   │   │       ├── CustomerDto.cs
│   │   │       └── CustomerDetailsDto.cs
│   │   ├── Products/                               # Product Use Cases
│   │   │   ├── Commands/
│   │   │   │   ├── CreateProduct/
│   │   │   │   │   ├── CreateProductCommand.cs
│   │   │   │   │   ├── CreateProductCommandHandler.cs
│   │   │   │   │   └── CreateProductCommandValidator.cs
│   │   │   │   ├── UpdateProduct/
│   │   │   │   ├── DeleteProduct/
│   │   │   │   ├── UpdateProductStock/
│   │   │   │   ├── ActivateProduct/
│   │   │   │   └── DeactivateProduct/
│   │   │   └── Queries/
│   │   │       ├── GetProducts/
│   │   │       │   ├── GetProductsQuery.cs
│   │   │       │   └── GetProductsQueryHandler.cs
│   │   │       ├── GetProductById/
│   │   │       ├── SearchProducts/
│   │   │       └── GetProductsByCategory/
│   │   ├── Orders/                                 # Order Use Cases
│   │   │   ├── Commands/
│   │   │   │   ├── CreateOrder/
│   │   │   │   ├── UpdateOrderStatus/
│   │   │   │   └── CancelOrder/
│   │   │   └── Queries/
│   │   │       ├── GetOrders/
│   │   │       ├── GetOrderById/
│   │   │       └── GetOrdersByCustomer/
│   │   ├── Customers/                              # Customer Use Cases
│   │   │   ├── Commands/
│   │   │   │   ├── CreateCustomer/
│   │   │   │   ├── UpdateCustomer/
│   │   │   │   └── DeleteCustomer/
│   │   │   └── Queries/
│   │   │       ├── GetCustomers/
│   │   │       └── GetCustomerById/
│   │   ├── Behaviors/                              # MediatR Pipeline Behaviors
│   │   │   ├── ValidationBehavior.cs               # Automatic validation
│   │   │   └── LoggingBehavior.cs                  # Request/response logging
│   │   ├── Abstractions/                           # Interface definitions
│   │   │   ├── Data/
│   │   │   │   ├── IApplicationDbContext.cs
│   │   │   │   └── IUnitOfWork.cs
│   │   │   ├── Caching/
│   │   │   │   └── ICacheService.cs
│   │   │   ├── Email/
│   │   │   │   └── IEmailService.cs
│   │   │   ├── Messaging/
│   │   │   │   └── IEventBus.cs
│   │   │   └── Common/
│   │   │       ├── IDateTimeProvider.cs
│   │   │       └── ICurrentUserService.cs
│   │   ├── Common/
│   │   │   └── Result.cs                           # Result pattern for error handling
│   │   └── DependencyInjection.cs                  # Service registration
│   │
│   ├── CleanArchitecture.Infrastructure/           # 🔧 External Concerns
│   │   ├── Data/
│   │   │   ├── DataContext/
│   │   │   │   └── ApplicationDbContext.cs         # EF Core DbContext
│   │   │   ├── Configurations/
│   │   │   │   ├── ProductConfiguration.cs         # EF Core entity configurations
│   │   │   │   ├── OrderConfiguration.cs
│   │   │   │   └── CustomerConfiguration.cs
│   │   │   ├── Repositories/
│   │   │   │   ├── ProductRepository.cs            # Repository implementations
│   │   │   │   ├── OrderRepository.cs
│   │   │   │   └── CustomerRepository.cs
│   │   │   ├── Interceptors/
│   │   │   │   ├── AuditableEntityInterceptor.cs   # Auto-update audit fields
│   │   │   │   └── DomainEventDispatcherInterceptor.cs
│   │   │   └── Seed/
│   │   │       └── DataSeeder.cs                   # Sample data generation
│   │   ├── Services/
│   │   │   ├── EmailService.cs                     # Email implementation
│   │   │   ├── CacheService.cs                     # In-memory cache
│   │   │   ├── EventBus.cs                         # Event publishing
│   │   │   ├── DateTimeProvider.cs
│   │   │   └── CurrentUserService.cs
│   │   ├── BackgroundServices/
│   │   │   ├── CacheCleanupService.cs              # Periodic cache cleanup
│   │   │   └── DomainEventProcessorService.cs      # Async event processing
│   │   ├── HealthChecks/
│   │   │   ├── DatabaseHealthCheck.cs
│   │   │   └── CacheHealthCheck.cs
│   │   ├── Migrations/                             # EF Core migrations
│   │   └── DependencyInjection.cs
│   │
│   └── Presentation/
│       ├── CleanArchitecture.Web/                  # 🎨 Blazor Server Web UI
│       │   ├── Components/
│       │   │   ├── Pages/
│       │   │   │   ├── Home.razor                  # Home page
│       │   │   │   ├── Products.razor              # Product management page
│       │   │   │   ├── Counter.razor               # Sample counter demo
│       │   │   │   └── Weather.razor               # Sample weather demo
│       │   │   ├── Layout/
│       │   │   │   ├── MainLayout.razor            # Main application layout
│       │   │   │   ├── NavMenu.razor               # Navigation menu
│       │   │   │   └── ReconnectModal.razor        # SignalR reconnection UI
│       │   │   ├── App.razor                       # Root component
│       │   │   ├── Routes.razor                    # Route configuration
│       │   │   └── _Imports.razor                  # Global using statements
│       │   ├── Services/
│       │   │   └── ProductApiClient.cs             # Typed HTTP client for API
│       │   ├── wwwroot/                            # Static assets (CSS, JS, images)
│       │   ├── Program.cs                          # Blazor app configuration
│       │   └── appsettings.json                    # Configuration
│       │
│       └── CleanArchitecture.API/                  # 📱 REST API
│           ├── Controllers/
│           │   ├── ProductsController.cs           # Product endpoints
│           │   ├── OrdersController.cs             # Order endpoints (future)
│           │   └── CustomersController.cs          # Customer endpoints (future)
│           ├── Program.cs                          # Application entry point
│           ├── appsettings.json                    # Configuration
│           ├── Products.http                       # HTTP test requests
│           └── Properties/
│               └── launchSettings.json
│
├── tests/                                          # 🧪 Test Projects
│   ├── CleanArchitecture.UnitTests/                # Unit tests
│   └── CleanArchitecture.IntegrationTests/         # Integration tests
│
├── docs/                                           # 📚 Documentation
│   ├── API-DOCUMENTATION.md
│   └── QUICKSTART.md
│
└── CleanArchitecture.sln                          # Solution file
```

---

## 🛠️ Technologies Used

### Core Framework
- **[.NET 9](https://dotnet.microsoft.com/)** - Latest .NET framework with improved performance
- **[C# 13](https://docs.microsoft.com/en-us/dotnet/csharp/)** - Modern C# features
- **[Blazor Server](https://docs.microsoft.com/en-us/aspnet/core/blazor/)** - Interactive server-side web UI framework

### Libraries & Packages

#### Blazor Web UI
| Package | Version | Purpose |
|---------|---------|---------|
| [ASP.NET Core Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/) | 9.0 | Server-side Blazor framework |
| [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/) | Built-in | Real-time communication |
| [Bootstrap 5](https://getbootstrap.com/) | 5.3 | Responsive UI styling |
| HttpClient | Built-in | API communication |
| DataAnnotations | Built-in | Form validation |

#### Application Layer
| Package | Version | Purpose |
|---------|---------|---------|
| [MediatR](https://github.com/jbogard/MediatR) | 12.5.0 | CQRS implementation, mediator pattern |
| [FluentValidation](https://fluentvalidation.net/) | 12.0.0 | Fluent validation rules |
| [FluentValidation.DependencyInjectionExtensions](https://fluentvalidation.net/) | 12.0.0 | DI integration |

#### Infrastructure Layer
| Package | Version | Purpose |
|---------|---------|---------|
| [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) | 9.0.9 | ORM for data access |
| [EF Core SQL Server](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer) | 9.0.9 | SQL Server provider |
| [EF Core Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools) | 9.0.9 | Migration tools |

#### API Layer
| Package | Version | Purpose |
|---------|---------|---------|
| [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) | 7.2.0 | OpenAPI/Swagger documentation |
| [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi) | 9.0.6 | OpenAPI support |

### Database
- **SQL Server** (LocalDB for development, full SQL Server for production)

### Design Patterns Used
- ✅ Clean Architecture
- ✅ CQRS (Command Query Responsibility Segregation)
- ✅ Repository Pattern
- ✅ Unit of Work Pattern
- ✅ Mediator Pattern
- ✅ Specification Pattern (planned)
- ✅ Factory Pattern
- ✅ Result Pattern
- ✅ Domain Events
- ✅ Pipeline Behavior Pattern
- ✅ Dependency Injection
- ✅ Value Object Pattern
- ✅ Aggregate Pattern

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) / [VS Code](https://code.visualstudio.com/) / [Rider](https://www.jetbrains.com/rider/)
- [Git](https://git-scm.com/)

### Installation Steps

#### 1. Clone the Repository
```bash
git clone https://github.com/rijwanansari/clean-architecture-dotnet9.git
cd clean-architecture-dotnet9
```

#### 2. Restore Dependencies
```bash
dotnet restore
```

#### 3. Update Connection String
Edit `src/Presentation/CleanArchitecture.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

For production SQL Server:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=CleanArchDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
  }
}
```

#### 4. Apply Database Migrations
```bash
dotnet ef database update --project src/CleanArchitecture.Infrastructure --startup-project src/Presentation/CleanArchitecture.API
```

This will:
- Create the database
- Apply all migrations
- Set up tables and relationships

#### 5. Build the Solution
```bash
dotnet build
```

#### 6. Run the API
```bash
cd src/Presentation/CleanArchitecture.API
dotnet run
```

Or press **F5** in Visual Studio to run with debugging.

#### 7. Run the Applications

**Start the API:**
```bash
cd src/Presentation/CleanArchitecture.API
dotnet run
```
The API will start at: https://localhost:7001

**Start the Blazor Web UI (in a new terminal):**
```bash
cd src/Presentation/CleanArchitecture.Web
dotnet run
```
The Web UI will start at: https://localhost:7002

#### 8. Access the Application

- **🎨 Blazor Web UI**: https://localhost:7002 (Main application)
- **📱 Swagger API**: https://localhost:7001 (API documentation)
- **💚 Health Check**: https://localhost:7001/health
- **🔌 API Base URL**: https://localhost:7001/api

> **Tip**: Update `appsettings.json` in CleanArchitecture.Web if the API runs on a different port:
> ```json
> {
>   "ApiBaseUrl": "https://localhost:7001"
> }
> ```

---

## 📖 Application Documentation

### Blazor Web UI

The Blazor Server application provides a modern, interactive web interface for managing products.

#### Features

**🏠 Home Page** (`/`)
- Welcome dashboard
- Quick links to main features
- Application overview

**📦 Products Management** (`/products`)
- **View Products**: Paginated table with search functionality
- **Create Product**: Modal form with validation
- **Edit Product**: In-place editing with real-time updates
- **Delete Product**: Confirmation before deletion
- **Search**: Real-time search across name and description
- **Pagination**: Navigate through large product catalogs

**⚡ Demo Pages**
- **Counter** (`/counter`): Interactive counter demo
- **Weather** (`/weather`): Sample data fetching demo

#### Blazor Component Architecture

```csharp
@page "/products"
@rendermode InteractiveServer
@inject ProductApiClient Api

<h3>Products</h3>

@code {
    private List<ProductDto> _items = new();
    
    protected override async Task OnInitializedAsync()
    {
        await LoadProducts();
    }
    
    private async Task LoadProducts()
    {
        var (items, total) = await Api.GetPagedAsync(page, pageSize);
        _items = items.ToList();
    }
}
```

#### API Client Pattern

The Blazor app uses a typed HTTP client to communicate with the backend API:

```csharp
public class ProductApiClient
{
    private readonly HttpClient _http;
    
    public async Task<(IReadOnlyList<ProductDto> Items, int TotalCount)> GetPagedAsync(
        int page, int pageSize, string? search = null)
    {
        var url = $"/api/products?page={page}&pageSize={pageSize}";
        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PagedResponse<ProductDto>>();
    }
}
```

#### UI Screenshots & Examples

**Product List View:**
- Sortable columns
- Search bar with real-time filtering
- Action buttons (Edit, Delete)
- Pagination controls

**Create/Edit Modal:**
- Form validation with DataAnnotations
- Required field indicators
- Real-time validation feedback
- Success/error notifications

**Responsive Design:**
- Mobile-friendly layout
- Bootstrap 5 styling
- Dark/Light theme support
- Accessible components

### REST API Documentation

### Available Endpoints

#### Products API

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/products` | Get all products (paginated) | No |
| `GET` | `/api/products/{id}` | Get product by ID | No |
| `GET` | `/api/products/search` | Search products | No |
| `GET` | `/api/products/category/{category}` | Get products by category | No |
| `POST` | `/api/products` | Create new product | No |
| `PUT` | `/api/products/{id}` | Update product | No |
| `PATCH` | `/api/products/{id}/stock` | Update stock quantity | No |
| `PATCH` | `/api/products/{id}/activate` | Activate product | No |
| `PATCH` | `/api/products/{id}/deactivate` | Deactivate product | No |
| `DELETE` | `/api/products/{id}` | Delete product | No |

#### Orders API (Coming Soon)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/orders` | Get all orders | Yes |
| `GET` | `/api/orders/{id}` | Get order by ID | Yes |
| `POST` | `/api/orders` | Create new order | Yes |
| `PATCH` | `/api/orders/{id}/status` | Update order status | Yes |
| `POST` | `/api/orders/{id}/cancel` | Cancel order | Yes |

#### Customers API (Coming Soon)

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/customers` | Get all customers | Yes |
| `GET` | `/api/customers/{id}` | Get customer by ID | Yes |
| `POST` | `/api/customers` | Create new customer | No |
| `PUT` | `/api/customers/{id}` | Update customer | Yes |
| `DELETE` | `/api/customers/{id}` | Delete customer | Yes |

### Request/Response Examples

#### 1. Create Product

**Request:**
```http
POST /api/products
Content-Type: application/json

{
  "name": "Wireless Mouse",
  "description": "Ergonomic wireless mouse with 2.4GHz connectivity",
  "price": 29.99,
  "currency": "USD",
  "stockQuantity": 100,
  "category": "Electronics"
}
```

**Response:** `201 Created`
```json
"3fa85f64-5717-4562-b3fc-2c963f66afa6"
```

**Response Headers:**
```
Location: /api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
```

#### 2. Get All Products (Paginated)

**Request:**
```http
GET /api/products?page=1&pageSize=10
```

**Response:** `200 OK`
```json
{
  "items": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "Wireless Mouse",
      "description": "Ergonomic wireless mouse with 2.4GHz connectivity",
      "price": 29.99,
      "currency": "USD",
      "stockQuantity": 100,
      "category": "Electronics",
      "isActive": true
    },
    {
      "id": "7c9e6679-7425-40de-944b-e07fc1f90ae7",
      "name": "Mechanical Keyboard",
      "description": "RGB mechanical gaming keyboard",
      "price": 129.99,
      "currency": "USD",
      "stockQuantity": 50,
      "category": "Electronics",
      "isActive": true
    }
  ],
  "page": 1,
  "pageSize": 10,
  "totalCount": 25,
  "totalPages": 3
}
```

#### 3. Search Products

**Request:**
```http
GET /api/products/search?searchTerm=mouse&page=1&pageSize=10
```

**Response:** `200 OK`
```json
{
  "items": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "Wireless Mouse",
      "description": "Ergonomic wireless mouse with 2.4GHz connectivity",
      "price": 29.99,
      "currency": "USD",
      "stockQuantity": 100,
      "category": "Electronics",
      "isActive": true
    }
  ],
  "page": 1,
  "pageSize": 10,
  "totalCount": 1,
  "totalPages": 1
}
```

#### 4. Update Product

**Request:**
```http
PUT /api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6
Content-Type: application/json

{
  "name": "Wireless Mouse Pro",
  "description": "Premium ergonomic wireless mouse with RGB lighting",
  "price": 39.99,
  "currency": "USD",
  "category": "Electronics"
}
```

**Response:** `204 No Content`

#### 5. Update Stock

**Request:**
```http
PATCH /api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6/stock
Content-Type: application/json

{
  "quantity": 50
}
```

**Response:** `204 No Content`

#### 6. Error Response

**Request:**
```http
POST /api/products
Content-Type: application/json

{
  "name": "",
  "description": "",
  "price": -10,
  "currency": "INVALID",
  "stockQuantity": -5,
  "category": ""
}
```

**Response:** `400 Bad Request`
```json
{
  "error": "Validation failed",
  "errors": [
    "Product name is required",
    "Product description is required",
    "Price must be greater than zero",
    "Currency must be a 3-letter ISO code (e.g., USD, EUR)",
    "Stock quantity cannot be negative",
    "Category is required"
  ]
}
```

### Testing the Application

#### Option 1: Blazor Web UI (Recommended for End Users)
1. Run both API and Web projects
2. Navigate to https://localhost:7002
3. Use the interactive product management interface
4. Create, edit, search, and delete products through the UI

**Benefits:**
- User-friendly interface
- Real-time validation
- No technical knowledge required
- Full CRUD operations
- Search and pagination

#### Option 2: Swagger UI (For API Testing)
1. Run the API application
2. Navigate to https://localhost:7001
3. Explore and test endpoints interactively

#### Option 3: HTTP File (VS Code)
1. Install **REST Client** extension in VS Code
2. Open `src/Presentation/CleanArchitecture.API/Products.http`
3. Click **Send Request** above any HTTP request

#### Option 4: cURL
```bash
# Create a product
curl -X POST https://localhost:7001/api/products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Wireless Mouse",
    "description": "Ergonomic mouse",
    "price": 29.99,
    "currency": "USD",
    "stockQuantity": 100,
    "category": "Electronics"
  }'

# Get all products
curl https://localhost:7001/api/products?page=1&pageSize=10

# Search products
curl "https://localhost:7001/api/products/search?searchTerm=mouse"
```

#### Option 5: Postman
Import the OpenAPI specification from: https://localhost:7001/swagger/v1/swagger.json

---

## 🎨 Blazor Features Deep Dive

### Why Blazor Server?

Blazor Server offers several advantages for this architecture:

| Feature | Benefit |
|---------|---------|
| **Server-Side Rendering** | Full .NET runtime on server, smaller payload to client |
| **Real-Time Updates** | SignalR enables instant UI updates |
| **Code Sharing** | Share DTOs and models between API and UI |
| **C# Everywhere** | No JavaScript required (optional for interop) |
| **Secure** | Business logic stays on server |
| **SEO Friendly** | Server-rendered content |
| **Easy Debugging** | Debug C# code with breakpoints |

### Blazor Component Examples

#### 1. Interactive Form with Validation

```razor
<EditForm Model="_editModel" OnValidSubmit="Save">
    <DataAnnotationsValidator />
    <ValidationSummary class="text-danger" />
    
    <div class="mb-3">
        <label class="form-label">Product Name</label>
        <InputText class="form-control" @bind-Value="_editModel.Name" />
        <ValidationMessage For="@(() => _editModel.Name)" />
    </div>
    
    <div class="mb-3">
        <label class="form-label">Price</label>
        <InputNumber class="form-control" @bind-Value="_editModel.Price" />
        <ValidationMessage For="@(() => _editModel.Price)" />
    </div>
    
    <button type="submit" class="btn btn-primary">Save</button>
</EditForm>
```

#### 2. Real-Time Search

```razor
<input class="form-control" 
       placeholder="Search products..." 
       @bind="_searchTerm" 
       @bind:event="oninput" 
       @bind:after="SearchProducts" />

@code {
    private string _searchTerm = "";
    
    private async Task SearchProducts()
    {
        await LoadProducts(_searchTerm);
    }
}
```

#### 3. Modal Dialogs

```razor
@if (_showModal)
{
    <div class="modal fade show d-block" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create Product</h5>
                    <button type="button" class="btn-close" @onclick="Hide"></button>
                </div>
                <div class="modal-body">
                    <!-- Form content -->
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" @onclick="Hide">Cancel</button>
                    <button class="btn btn-primary" @onclick="Save">Save</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-backdrop fade show"></div>
}
```

#### 4. Pagination Component

```razor
<div class="d-flex justify-content-between">
    <div>Showing page @_currentPage of @_totalPages</div>
    <div class="btn-group">
        <button class="btn btn-outline-secondary" 
                disabled="@(_currentPage <= 1)" 
                @onclick="PreviousPage">
            Previous
        </button>
        <button class="btn btn-outline-secondary" 
                disabled="@(_currentPage >= _totalPages)" 
                @onclick="NextPage">
            Next
        </button>
    </div>
</div>
```

### SignalR Integration

Blazor Server uses SignalR for real-time communication:

```csharp
// Automatic reconnection handling
<ReconnectModal />

@code {
    // UI automatically updates when server state changes
    private async Task UpdateProduct()
    {
        await Api.UpdateAsync(product);
        // UI re-renders automatically via SignalR
    }
}
```

### State Management

```csharp
// Scoped services for user-specific state
builder.Services.AddScoped<ProductApiClient>();

// Singleton services for app-wide state
builder.Services.AddSingleton<INotificationService, NotificationService>();
```

### Error Handling in Blazor

```razor
@if (!string.IsNullOrEmpty(_errorMessage))
{
    <div class="alert alert-danger alert-dismissible fade show">
        @_errorMessage
        <button type="button" class="btn-close" @onclick="() => _errorMessage = null"></button>
    </div>
}

@code {
    private string? _errorMessage;
    
    private async Task LoadProducts()
    {
        try
        {
            _errorMessage = null;
            var result = await Api.GetProductsAsync();
            _products = result.Items;
        }
        catch (Exception ex)
        {
            _errorMessage = $"Error: {ex.Message}";
        }
    }
}
```

### Styling with Bootstrap 5

The application uses Bootstrap 5 for responsive design:

```html
<!-- Responsive table -->
<div class="table-responsive">
    <table class="table table-striped table-hover">
        <!-- content -->
    </table>
</div>

<!-- Responsive grid -->
<div class="row">
    <div class="col-md-6 col-lg-4">
        <!-- content -->
    </div>
</div>
```

---

## 🗄️ Database Schema

### Entity Relationship Diagram

```
┌─────────────────┐         ┌──────────────────┐
│    Customer     │         │      Order       │
├─────────────────┤         ├──────────────────┤
│ Id (PK)         │◄───────┤│ Id (PK)          │
│ FirstName       │    1  * ││ OrderNumber      │
│ LastName        │         ││ CustomerId (FK)  │
│ Email           │         ││ Status           │
│ PhoneNumber     │         ││ PaymentMethod    │
│ Address         │         ││ ShippingAddress  │
│ CreatedAt       │         ││ CreatedAt        │
│ UpdatedAt       │         ││ UpdatedAt        │
└─────────────────┘         └────────┬─────────┘
                                     │
                                     │ 1
                                     │
                                     │ *
                           ┌─────────▼──────────┐
                           │    OrderItem       │
                           ├────────────────────┤
                           │ Id (PK)            │
                           │ OrderId (FK)       │
                           │ ProductId (FK)     │
                           │ ProductName        │
                           │ Quantity           │
                           │ UnitPrice          │
                           │ CreatedAt          │
                           └─────────┬──────────┘
                                     │
                                     │ *
                                     │
                                     │ 1
┌─────────────────┐                 │
│     Product     │◄────────────────┘
├─────────────────┤
│ Id (PK)         │
│ Name            │
│ Description     │
│ Price           │
│ StockQuantity   │
│ Category        │
│ IsActive        │
│ CreatedAt       │
│ UpdatedAt       │
└─────────────────┘
```

### Tables

#### Products
- Primary Key: `Id` (Guid)
- Stores product catalog information
- Includes pricing, inventory, and categorization
- Soft delete support via `IsActive` flag

#### Orders
- Primary Key: `Id` (Guid)
- Foreign Key: `CustomerId` → Customers.Id
- Stores order header information
- Tracks order status through state machine
- Immutable after creation (update via commands only)

#### OrderItems
- Primary Key: `Id` (Guid)
- Foreign Keys: `OrderId` → Orders.Id, `ProductId` → Products.Id
- Line items for each order
- Snapshot of product details at time of order

#### Customers
- Primary Key: `Id` (Guid)
- Stores customer information
- Includes contact details and addresses
- One-to-many relationship with Orders

---

## 🎨 Design Patterns

### 1. Clean Architecture
Ensures dependency rules: inner layers don't depend on outer layers.

### 2. CQRS (Command Query Responsibility Segregation)
```csharp
// Command (Write)
public record CreateProductCommand(string Name, decimal Price) : IRequest<Result<Guid>>;

// Query (Read)
public record GetProductsQuery(int Page, int PageSize) : IRequest<Result<PagedResponse<ProductDto>>>;
```

### 3. Repository Pattern
```csharp
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<(List<Product> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    void Update(Product product);
    void Delete(Product product);
}
```

### 4. Unit of Work
```csharp
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
```

### 5. Mediator Pattern (via MediatR)
```csharp
// Send command
var command = new CreateProductCommand("Mouse", 29.99m);
var result = await _mediator.Send(command);

// Send query
var query = new GetProductsQuery(1, 10);
var products = await _mediator.Send(query);
```

### 6. Pipeline Behaviors
```csharp
// Automatic validation before handler execution
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validate request
        // If valid, call next()
        // If invalid, throw ValidationException
    }
}
```

### 7. Result Pattern
```csharp
public class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }
    
    public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };
    public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
}
```

### 8. Value Objects
```csharp
public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    
    public Money Multiply(int quantity) => new Money(Amount * quantity, Currency);
}
```

### 9. Domain Events
```csharp
public class OrderCreatedEvent : IDomainEvent
{
    public Guid OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime OccurredOn { get; init; }
}
```

---

## ✨ Features

### Current Features

#### ✅ Blazor Web UI
- Interactive product management interface
- Real-time search and filtering
- Pagination with page navigation
- Create product modal with validation
- Edit product modal with validation
- Delete confirmation
- Client-side form validation
- Server-side validation integration
- Responsive Bootstrap 5 design
- Error handling and user feedback
- Loading indicators
- SignalR-powered real-time updates

#### ✅ Products Management (API)
- Create, Read, Update, Delete products
- Search products by name/description
- Filter products by category
- Pagination support
- Stock management
- Activate/Deactivate products

#### ✅ Orders Management (Application Layer)
- Create orders with multiple items
- Update order status
- Cancel orders
- View order history
- Customer-specific orders

#### ✅ Customers Management (Application Layer)
- Customer registration
- Update customer information
- Customer profile management

#### ✅ Technical Features
- **Validation**: FluentValidation with detailed error messages
- **Logging**: Structured logging with Serilog integration points
- **Caching**: In-memory cache service (extensible to Redis)
- **Health Checks**: Database and service health monitoring
- **Background Services**: Async processing capabilities
- **Domain Events**: Event-driven architecture support
- **Auditing**: Automatic CreatedAt/UpdatedAt tracking
- **Global Exception Handling**: Centralized error handling
- **Swagger Documentation**: Interactive API documentation
- **CORS**: Cross-origin resource sharing support

### Security Features (Planned)

- 🔒 JWT Authentication
- 🔐 Role-based Authorization
- 🛡️ Input Sanitization
- 📝 Audit Logging
- 🔑 API Key Management

---

## 🧪 Testing

### Unit Tests
Test business logic in isolation:

```bash
dotnet test tests/CleanArchitecture.UnitTests
```

Example unit tests:
- Domain entity behavior
- Command/Query handlers
- Validators
- Value objects

### Integration Tests
Test end-to-end flows:

```bash
dotnet test tests/CleanArchitecture.IntegrationTests
```

Example integration tests:
- API endpoints
- Database operations
- Repository implementations
- Background services

### Test Coverage
Run with code coverage:

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

---

## 🗺️ Roadmap

### Phase 1: Core Foundation ✅ (Completed)
- [x] Clean Architecture setup
- [x] Domain entities and value objects
- [x] Repository pattern
- [x] CQRS with MediatR
- [x] FluentValidation
- [x] Products API
- [x] EF Core with SQL Server
- [x] Swagger documentation
- [x] Blazor Server Web UI
- [x] Product Management UI (CRUD)
- [x] API Client pattern
- [x] Form validation
- [x] Search and pagination
- [x] Responsive design

### Phase 2: Feature Completion 🚧 (In Progress)
- [ ] Orders UI in Blazor (Order management page)
- [ ] Customers UI in Blazor (Customer management page)
- [ ] Orders API endpoints (Controllers)
- [ ] Customers API endpoints (Controllers)
- [ ] Authentication & Authorization (JWT)
- [ ] Blazor authentication state
- [ ] User login/logout UI
- [ ] Refresh Token implementation
- [ ] Role-based access control
- [ ] API versioning

### Phase 3: Advanced Features 📅 (Planned)
- [ ] Redis caching
- [ ] Distributed caching
- [ ] Event sourcing
- [ ] CQRS with separate read/write databases
- [ ] RabbitMQ/Azure Service Bus integration
- [ ] SignalR for real-time notifications
- [ ] File upload (product images)
- [ ] Blob storage (Azure/AWS S3)

### Phase 4: Performance & Scalability 📅
- [ ] Response caching
- [ ] Output caching (.NET 9 feature)
- [ ] Database query optimization
- [ ] Connection pooling
- [ ] Load testing with K6/JMeter
- [ ] Performance profiling
- [ ] Database indexing strategy

### Phase 5: Observability 📅
- [ ] Application Insights integration
- [ ] Serilog with structured logging
- [ ] Distributed tracing (OpenTelemetry)
- [ ] Metrics and monitoring
- [ ] ELK Stack integration
- [ ] Custom dashboards

### Phase 6: DevOps & CI/CD 📅
- [ ] Docker containerization
- [ ] Docker Compose for local development
- [ ] Kubernetes deployment manifests
- [ ] GitHub Actions CI/CD pipeline
- [ ] Azure DevOps pipelines
- [ ] Infrastructure as Code (Terraform/Bicep)
- [ ] Automated database migrations

### Phase 7: Testing & Quality 📅
- [ ] Comprehensive unit tests (80%+ coverage)
- [ ] Integration tests
- [ ] E2E tests with Playwright
- [ ] Load testing
- [ ] Security testing (OWASP)
- [ ] Mutation testing
- [ ] Contract testing

### Phase 8: Enhanced UI Features 📅
- [ ] Blazor WebAssembly option (for offline capability)
- [ ] Progressive Web App (PWA) support
- [ ] Dark/Light theme toggle
- [ ] Advanced data grids with sorting/filtering
- [ ] Chart.js integration for analytics
- [ ] Export to Excel/PDF from UI
- [ ] Drag-and-drop file upload
- [ ] Mobile app (MAUI Blazor Hybrid)
- [ ] Customer portal
- [ ] Admin panel with dashboard
- [ ] Real-time notifications in UI

### Phase 9: Advanced Domain Features 📅
- [ ] Product reviews and ratings
- [ ] Shopping cart
- [ ] Payment gateway integration (Stripe/PayPal)
- [ ] Inventory management
- [ ] Shipping integration
- [ ] Invoice generation (PDF)
- [ ] Email notifications
- [ ] SMS notifications
- [ ] Push notifications

### Phase 10: Business Intelligence 📅
- [ ] Reporting module
- [ ] Analytics dashboard
- [ ] Sales reports
- [ ] Inventory reports
- [ ] Customer insights
- [ ] Data export (Excel/CSV)

---

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

### How to Contribute

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/amazing-feature
   ```
3. **Commit your changes**
   ```bash
   git commit -m 'Add some amazing feature'
   ```
4. **Push to the branch**
   ```bash
   git push origin feature/amazing-feature
   ```
5. **Open a Pull Request**

### Coding Standards

- Follow C# coding conventions
- Write unit tests for new features
- Update documentation
- Keep commits atomic and meaningful
- Use conventional commit messages

### Pull Request Process

1. Ensure all tests pass
2. Update README.md with any new features
3. Add/update XML documentation comments
4. Request review from maintainers

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 📧 Contact & Support

- **Author**: Rijwan Ansari
- **Email**: support@cleanarchitecture.com
- **GitHub**: [@rijwanansari](https://github.com/rijwanansari)
- **Issues**: [GitHub Issues](https://github.com/rijwanansari/clean-architecture-dotnet9/issues)

---

## 🙏 Acknowledgments

- [Robert C. Martin (Uncle Bob)](https://blog.cleancoder.com/) - Clean Architecture principles
- [Jason Taylor](https://github.com/jasontaylordev) - Clean Architecture template inspiration
- [Vladimir Khorikov](https://enterprisecraftsmanship.com/) - Domain-Driven Design guidance
- [Jimmy Bogard](https://jimmybogard.com/) - MediatR and CQRS patterns
- The .NET Community

---

## 📚 Additional Resources

### Books
- 📖 **Clean Architecture** by Robert C. Martin
- 📖 **Domain-Driven Design** by Eric Evans
- 📖 **Implementing Domain-Driven Design** by Vaughn Vernon
- 📖 **Patterns of Enterprise Application Architecture** by Martin Fowler

### Articles & Tutorials
- [Microsoft - Web API best practices](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)

### Videos
- [Clean Architecture with ASP.NET Core](https://www.youtube.com/watch?v=dK4Yb6-LxAk)
- [Domain-Driven Design Fundamentals](https://app.pluralsight.com/library/courses/domain-driven-design-fundamentals)

---

## ⭐ Show Your Support

If you find this project helpful, please consider:
- ⭐ **Starring** this repository
- 🐛 **Reporting** bugs and issues
- 💡 **Suggesting** new features
- 📖 **Improving** documentation
- 🔀 **Contributing** code

---

<div align="center">

### Built with ❤️ using Clean Architecture and .NET 9

**[⬆ Back to Top](#-clean-architecture-with-net-9)**

</div>
