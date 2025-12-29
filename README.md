# DDD GraphQL Implementation

A comprehensive example of implementing Domain-Driven Design (DDD) with GraphQL, demonstrating best practices for building scalable, maintainable APIs with proper separation of concerns, efficient data loading, and clean architecture.

## 🎯 Overview

This project showcases a production-ready implementation combining:
- **Domain-Driven Design (DDD)** - Rich domain models with proper encapsulation
- **GraphQL** - Flexible, efficient API with HotChocolate
- **CQRS** - Command Query Responsibility Segregation pattern
- **Clean Architecture** - Clear separation between Domain, Application, Infrastructure, and Presentation layers
- **Efficient Data Loading** - Batch loading with DataLoaders to prevent N+1 queries

The domain represents a cleaning service marketplace where customers can book cleaning services from cleaners who offer various service packages.

## 🏗️ Architecture

### Layer Structure

```
┌─────────────────────────────────────────┐
│           WebApi (Presentation)        │
│  - GraphQL Types & Resolvers            │
│  - Data Loaders                         │
│  - Query/Mutation Handlers              │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│         Application (Use Cases)         │
│  - MediatR Queries/Commands              │
│  - Validation Behaviors                 │
│  - Application Services                 │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│            Domain (Core)                 │
│  - Aggregates & Entities                │
│  - Value Objects                        │
│  - Domain Events                        │
│  - Read Models                          │
│  - Repository Interfaces                │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│        Infrastructure (Persistence)    │
│  - Entity Framework Core                │
│  - Query Stores (Repositories)          │
│  - Database Context                     │
└─────────────────────────────────────────┘
```

### Key Architectural Patterns

#### 1. **Domain-Driven Design (DDD)**
- **Aggregates**: Self-contained units with clear boundaries (Booking, Cleaner, Customer, ServiceOffer)
- **Entities**: Objects with identity (BookingReview within Booking aggregate)
- **Value Objects**: Immutable objects defined by their values (ServicePricing, ServicePricingSnapshot)
- **Domain Events**: Events that represent something important happened in the domain
- **Repository Pattern**: Abstract data access through interfaces

#### 2. **CQRS (Command Query Responsibility Segregation)**
- **Queries**: Read operations using MediatR handlers
- **Query Stores**: Optimized read models separate from write models
- **Compiled Queries**: Pre-compiled EF Core queries for maximum performance

#### 3. **GraphQL with HotChocolate**
- **Type System**: Strongly-typed GraphQL schema
- **Data Loaders**: Batch loading to prevent N+1 query problems
- **Field Resolvers**: Custom resolvers for nested relationships

## 📦 Domain Model

### Aggregates

#### **Booking Aggregate**
Represents a booking made by a customer for a cleaning service.

**Key Features:**
- Factory method for creation with validation
- Domain methods: `Confirm()`, `Complete()`, `Cancel()`, `Reschedule()`, `AddReview()`
- State management via `BookingStatus` enum
- Contains `ServicePricingSnapshot` value objects (historical pricing)
- Contains `BookingReview` entities

**Example:**
```csharp
var booking = Booking.Create(
    aggregateId: Guid.NewGuid(),
    serviceOfferAggregateId: serviceOfferId,
    customerAggregateId: customerId,
    scheduledDate: DateTimeOffset.UtcNow.AddDays(7),
    pricingSnapshots: snapshots
);

booking.Confirm();
booking.Complete();
```

#### **Cleaner Aggregate**
Represents a cleaning service provider.

**Key Features:**
- Profile management: `UpdateProfile()`
- Service management: `AddOfferedService()`, `RemoveOfferedService()`
- Activation: `Activate()`, `Deactivate()`, `Archive()`
- Contains `CleanerOfferedService` value objects

#### **Customer Aggregate**
Represents a customer who books services.

**Key Features:**
- Profile management with email validation
- Activation/deactivation
- Soft delete via archiving

#### **ServiceOffer Aggregate**
Represents a cleaning service offered by a cleaner.

**Key Features:**
- Multiple pricing models (Hourly, Fixed)
- Pricing management: `AddPricing()`, `UpdatePricing()`, `RemovePricing()`
- Activation/deactivation
- Contains `ServicePricing` value objects

### Value Objects

Value objects are immutable and defined by their values:

- **ServicePricing**: Price and pricing model for a service offer
- **ServicePricingSnapshot**: Historical pricing at time of booking
- **CleanerOfferedService**: Link between cleaner and service offer

### Entities

Entities within aggregates:

- **BookingReview**: Review entity within Booking aggregate

## 🔄 CQRS Implementation

### Query Side (Read Models)

Read models are optimized DTOs for querying:

```csharp
public class BookingReadModel : EntityReadModel
{
    public required Guid AggregateId { get; init; }
    public required Guid ServiceOfferAggregateId { get; init; }
    public required Guid CustomerAggregateId { get; init; }
    
    public ServiceOfferReadModel? ServiceOffer { get; init; }
    public CustomerReadModel? Customer { get; init; }
    public List<ServicePricingSnapshotReadModel>? ServicePricingSnapshots { get; init; }
    public List<BookingReviewReadModel>? BookingReviews { get; init; }
}
```

### Query Stores

Query stores implement optimized read operations:

- **Compiled Queries**: Using `EF.CompileAsyncQuery` for maximum performance
- **Async Enumeration**: Efficient streaming of results
- **Batch Operations**: Methods to load multiple entities by IDs

```csharp
private static readonly Func<ServiceDbContext, Guid, CancellationToken, Task<CleanerReadModel?>> 
    GetCleanerByAggregateIdQuery = EF.CompileAsyncQuery(...);
```

## 🚀 GraphQL Implementation

### Query Types

All queries use MediatR for clean separation:

```csharp
[GraphQLName("serviceOffersPage")]
public Task<GetServiceOffersPageResponse> GetServiceOffersPage(
    int page,
    int pageSize,
    [Service] IMediator mediator,
    CancellationToken cancellationToken)
    => mediator.Send(new GetServiceOffersPage { Page = page, PageSize = pageSize }, cancellationToken);
```

### Data Loaders (N+1 Prevention)

Data loaders batch requests to prevent N+1 query problems:

```csharp
public sealed class CleanerByAggregateIdDataLoader : GroupedDataLoader<Guid, CleanerReadModel>
{
    protected override Task<ILookup<Guid, CleanerReadModel>> LoadGroupedBatchAsync(
        IReadOnlyList<Guid> cleanerAggregateIds, 
        CancellationToken cancellationToken)
        => _mediator.Send(new GetCleanersByAggregateIds { CleanerAggregateIds = cleanerAggregateIds }, cancellationToken);
}
```

**How it works:**
1. GraphQL resolver requests a cleaner for a service offer
2. Data loader collects all cleaner IDs from all service offers in the batch
3. Single database query loads all cleaners at once
4. Results are distributed back to the resolvers

### Type Resolvers

Custom resolvers for nested relationships:

```csharp
d.Field(o => o.Cleaner)
    .Type<ObjectType<CleanerReadModel>>()
    .Resolve(async ctx =>
    {
        var serviceOffer = ctx.Parent<ServiceOfferReadModel>();
        var loader = ctx.DataLoader<CleanerByAggregateIdDataLoader>();
        var cleaners = await loader.LoadAsync(serviceOffer.CleanerAggregateId, ctx.RequestAborted);
        return cleaners?.FirstOrDefault();
    });
```

## 📊 Available Queries

### Paginated Queries

All paginated queries return a response with items and total count:

- `serviceOffersPage(page: Int!, pageSize: Int!)` - Get paginated service offers
- `cleanersPage(page: Int!, pageSize: Int!)` - Get paginated cleaners
- `bookingsPage(page: Int!, pageSize: Int!)` - Get paginated bookings
- `customersPage(page: Int!, pageSize: Int!)` - Get paginated customers

### Get by ID Queries

- `cleaner(cleanerAggregateId: UUID!)` - Get single cleaner
- `serviceOffer(serviceOfferAggregateId: UUID!)` - Get single service offer
- `booking(bookingAggregateId: UUID!)` - Get single booking
- `customer(customerAggregateId: UUID!)` - Get single customer

## 💡 Key Design Decisions

### 1. **Aggregate Design**
- **Private Setters**: All properties use private setters to enforce encapsulation
- **Factory Methods**: `Create()` methods ensure valid aggregate creation
- **Domain Methods**: Business logic encapsulated in domain methods
- **Validation**: Input validation at aggregate boundaries

### 2. **Read/Write Separation**
- **Write Models**: Rich domain models with business logic
- **Read Models**: Simple DTOs optimized for queries
- **Query Stores**: Separate from write repositories

### 3. **Performance Optimization**
- **Compiled Queries**: Pre-compiled EF Core queries
- **Data Loaders**: Batch loading prevents N+1 queries
- **Async Enumeration**: Efficient streaming of large result sets
- **DbContext Pooling**: Connection pooling for better performance

### 4. **Type Safety**
- **Strongly Typed**: All queries and responses are strongly typed
- **Read Models**: Separate read models prevent accidental domain logic in queries
- **Value Objects**: Immutable value objects ensure data integrity

## 📝 Example Queries

### Get Service Offers with Nested Data

```graphql
query GetServiceOffersPage($page: Int!, $pageSize: Int!) {
  serviceOffersPage(page: $page, pageSize: $pageSize) {
    serviceOffers {
      id
      aggregateId
      title
      description
      createdAt
      cleaner {
        name
        description
        phoneNumber
        offeredServices {
          title
          description
        }
      }
      servicePricings {
        price
        pricingModel
      }
    }
    totalCount
  }
}
```

### Get Bookings with Deep Nesting

```graphql
query GetBookings($page: Int!, $pageSize: Int!) {
  bookingsPage(page: $page, pageSize: $pageSize) {
    bookings {
      id
      aggregateId
      customer {
        name
        email
        bookings {
          id
          serviceOffer {
            title
          }
        }
      }
      serviceOffer {
        title
        description
        cleaner {
          name
          description
        }
      }
      servicePricingSnapshots {
        price
        pricingModel
      }
      bookingReviews {
        review
        rating
        createdAt
      }
    }
    totalCount
  }
}
```

### Get Single Entity by ID

```graphql
query GetBooking($bookingAggregateId: UUID!) {
  booking(bookingAggregateId: $bookingAggregateId) {
    id
    aggregateId
    customer {
      name
      email
    }
    serviceOffer {
      title
      cleaner {
        name
      }
    }
    servicePricingSnapshots {
      price
      pricingModel
    }
  }
}
```

## 🛠️ Setup & Development

### Prerequisites

- .NET 8.0 SDK
- SQL Server (or SQL Server Express)
- Visual Studio 2022 / Rider / VS Code

### Database Setup

1. **Update Connection String**

   Edit `WebApi/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "ServiceDatabase": "Server=localhost;Database=CleaningServiceDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

2. **Create Migration**

   ```bash
   Add-Migration InitialMigration -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext -OutputDir EntityFramework\Migrations
   ```

3. **Apply Migration**

   ```bash
   Update-Database -Project Infrastructure -StartupProject WebApi -Context Infrastructure.EntityFramework.ServiceDbContext
   ```

4. **Seed Data**

   The migration includes seed data. If you need to re-seed:
   ```bash
   # Run the seed data migration
   Update-Database -Project Infrastructure -StartupProject WebApi
   ```

### Running the Application

```bash
cd WebApi
dotnet run
```

GraphQL endpoint: `https://localhost:5001/graphql` (or check `launchSettings.json`)

GraphQL Playground: Available at the GraphQL endpoint

## 🏛️ Project Structure

```
src/
├── Domain/                          # Core domain layer
│   ├── Abstractions/               # Base classes (Entity, AggregateRoot, ValueObject)
│   ├── Aggregates/                 # Domain aggregates
│   │   ├── BookingAggregate/
│   │   │   ├── Booking.cs         # Aggregate root
│   │   │   ├── Entities/          # Entities within aggregate
│   │   │   ├── ValueObjects/      # Value objects
│   │   │   ├── ReadModels/        # Query read models
│   │   │   └── Repositories/      # Repository interfaces
│   │   └── ...
│   └── Shared/                      # Shared domain concepts
│
├── Application/                     # Application layer
│   ├── Queries/                    # CQRS queries
│   ├── Commands/                   # CQRS commands (future)
│   ├── Behaviors/                  # MediatR behaviors (validation)
│   └── DependencyInjection/
│
├── Infrastructure/                  # Infrastructure layer
│   └── EntityFramework/
│       ├── Repositories/          # Query store implementations
│       ├── EntityTypeConfigurations/ # EF Core configurations
│       └── Migrations/            # Database migrations
│
└── WebApi/                          # Presentation layer
    └── Graph/
        ├── Query.cs                # GraphQL queries
        ├── Mutation.cs             # GraphQL mutations (future)
        ├── Types/                  # GraphQL type configurations
        └── Loaders/                # Data loaders
```

## 🎓 Key Concepts Explained

### Aggregate Root

An aggregate root is the entry point to an aggregate. It ensures consistency and enforces business rules:

```csharp
public class Booking : AggregateRoot
{
    // Private setters enforce encapsulation
    public Guid ServiceOfferAggregateId { get; private set; }
    public BookingStatus Status { get; private set; }
    
    // Factory method ensures valid creation
    public static Booking Create(...) { }
    
    // Domain methods encapsulate business logic
    public void Confirm() { }
    public void Complete() { }
}
```

### Value Objects

Immutable objects defined by their values, not identity:

```csharp
public class ServicePricing : ValueObject
{
    public long Price { get; private init; }
    public PricingModel PricingModel { get; private init; }
    
    // Equality based on all properties
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Price;
        yield return PricingModel;
    }
}
```

### Read Models

Optimized DTOs for querying, separate from domain models:

- No business logic
- Optimized for specific queries
- Can denormalize data for performance
- Used only for reading data

### Query Stores

Specialized repositories for read operations:

- Use compiled queries for performance
- Return read models, not domain entities
- Optimized for specific query patterns
- Can use different data sources (SQL, NoSQL, etc.)

### Data Loaders

HotChocolate DataLoaders batch requests to prevent N+1 queries:

**Without DataLoader:**
```
Query 1: Get 10 service offers
Query 2: Get cleaner for service offer 1
Query 3: Get cleaner for service offer 2
...
Query 11: Get cleaner for service offer 10
Total: 11 queries
```

**With DataLoader:**
```
Query 1: Get 10 service offers
Query 2: Get all cleaners (batch) for service offers 1-10
Total: 2 queries
```

## 🔒 Best Practices Implemented

1. **Encapsulation**: Private setters, factory methods, domain methods
2. **Validation**: Input validation at aggregate boundaries
3. **Immutability**: Value objects are immutable
4. **Separation of Concerns**: Clear layer boundaries
5. **Performance**: Compiled queries, data loaders, async operations
6. **Type Safety**: Strong typing throughout
7. **Testability**: Interfaces, dependency injection
8. **Maintainability**: Clean code, clear structure

## 🚧 Future Enhancements

- [ ] Command handlers for mutations
- [ ] Domain event handlers
- [ ] Unit tests for domain logic
- [ ] Integration tests for queries
- [ ] Authentication & Authorization
- [ ] GraphQL subscriptions
- [ ] Caching layer
- [ ] Event sourcing (optional)

## 📚 Technologies Used

- **.NET 8.0** - Runtime and framework
- **HotChocolate** - GraphQL server
- **Entity Framework Core** - ORM
- **MediatR** - CQRS mediator
- **SQL Server** - Database
- **FluentValidation** - Validation (ready for use)

## 🤝 Contributing

This is an example project demonstrating DDD + GraphQL patterns. Feel free to use it as a reference or starting point for your own projects.

## 📄 License

This project is provided as-is for educational and reference purposes.

---

**Built with ❤️ using Domain-Driven Design and GraphQL**
