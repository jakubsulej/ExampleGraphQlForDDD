# Code Review - DDD GraphQL Implementation

## 🔴 Critical Issues

### 1. **Resource Leak: DbContext Not Disposed in Query Stores**
**Location**: All query store implementations
**Severity**: Critical
**Issue**: DbContext instances created via `IDbContextFactory` are never disposed, causing connection leaks.

```csharp
// Current (WRONG):
public async Task<BookingReadModel?> GetBookingByAggregateId(...)
{
    var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
    return await GetBookingByAggregateIdQuery(dbContext, bookingAggregateId, cancellationToken);
    // dbContext is never disposed!
}
```

**Fix**: Use `using` statement or `await using`:
```csharp
public async Task<BookingReadModel?> GetBookingByAggregateId(...)
{
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
    return await GetBookingByAggregateIdQuery(dbContext, bookingAggregateId, cancellationToken);
}
```

### 2. **Incorrect BookingId Mapping in BookingQueryStore**
**Location**: `Infrastructure/EntityFramework/Repositories/BookingQueryStore.cs` (lines 42, 76, 111, 147)
**Severity**: Critical
**Issue**: `BookingId = (int)r.Id` maps the Review's ID instead of the Booking's ID.

```csharp
// Current (WRONG):
BookingReviews = b.BookingReviews.Select(r => new BookingReviewReadModel
{
    Id = r.Id,
    BookingId = (int)r.Id,  // ❌ This is the review ID, not booking ID!
    ...
})
```

**Fix**: Should use `BookingAggregateId` or the actual foreign key:
```csharp
BookingId = (int)b.Id,  // Or use BookingAggregateId if needed
```

### 3. **Type Mismatch: Price Type Inconsistency**
**Location**: `Domain/Aggregates/BookingAggregate/ValueObjects/ServicePricingSnapshot.cs` vs ReadModel
**Severity**: High
**Issue**: `ServicePricingSnapshot` uses `long Price` but `ServicePricingSnapshotReadModel` uses `int Price`. Casting `(int)s.Price` can cause overflow.

**Fix**: Either:
- Change ReadModel to use `long Price`
- Or add validation to ensure price fits in int range
- Or change domain model to use `int` if that's the business requirement

## 🟠 Business Logic Issues

### 4. **Missing Booking Status Transition: InProgress**
**Location**: `Domain/Aggregates/BookingAggregate/Booking.cs`
**Severity**: Medium
**Issue**: `BookingStatus.InProgress` exists but there's no method to transition to it. `Complete()` only allows transition from `Confirmed`, not `InProgress`.

**Fix**: Add method or allow completion from InProgress:
```csharp
public void Start()
{
    if (Status != BookingStatus.Confirmed)
        throw new InvalidOperationException($"Cannot start booking in {Status} status");
    
    Status = BookingStatus.InProgress;
    UpdatedAt = DateTimeOffset.UtcNow;
}

// Or update Complete() to allow InProgress:
public void Complete()
{
    if (Status != BookingStatus.Confirmed && Status != BookingStatus.InProgress)
        throw new InvalidOperationException($"Cannot complete booking in {Status} status");
    // ...
}
```

### 5. **Payment.Refund() Uses Wrong Property**
**Location**: `Domain/Aggregates/PaymentAggregate/Payment.cs` (line 100)
**Severity**: Medium
**Issue**: `Refund()` sets `FailureReason` property, which is semantically incorrect. Should have separate `RefundReason` property.

**Fix**: Add `RefundReason` property:
```csharp
public string? RefundReason { get; private set; }

public void Refund(string reason)
{
    // ...
    RefundReason = reason;  // Not FailureReason
    // ...
}
```

### 6. **Missing Scheduled Date Validation**
**Location**: `Domain/Aggregates/BookingAggregate/Booking.Create()`
**Severity**: Medium
**Issue**: No validation that `scheduledDate` is in the future. Can create bookings in the past.

**Fix**: Add validation:
```csharp
if (scheduledDate <= DateTimeOffset.UtcNow)
    throw new ArgumentException("Scheduled date must be in the future", nameof(scheduledDate));
```

### 7. **Missing IsActive Filter in Query Stores**
**Location**: All query stores
**Severity**: Medium
**Issue**: Query stores don't filter by `IsActive` or `IsArchived`, potentially returning inactive/archived entities.

**Fix**: Add filters:
```csharp
.Where(b => !b.IsArchived && b.IsActive)  // For ServiceOffer
.Where(c => !c.IsArchived && c.IsActive)  // For Customer, Cleaner
```

### 8. **BookingReview Entity Relationship Issue**
**Location**: `Infrastructure/EntityFramework/EntityTypeConfigurations/BookingReviewEntityTypeConfiguration.cs`
**Severity**: Medium
**Issue**: `BookingReview` has `BookingAggregateId` but EF configuration uses `HasForeignKey("BookingId")` which doesn't match. The relationship might not work correctly.

**Fix**: Ensure foreign key matches:
```csharp
builder.HasMany(b => b.BookingReviews)
    .WithOne()
    .HasForeignKey(br => br.BookingAggregateId)  // Use actual property
    .OnDelete(DeleteBehavior.Cascade);
```

## 🟡 Code Quality Issues

### 9. **Missing Phone Number Validation**
**Location**: `Domain/Aggregates/CleanerAggregate/Cleaner.cs`, `Customer.cs`
**Severity**: Low
**Issue**: No format validation for phone numbers. Could accept invalid formats.

**Suggestion**: Add phone number validation or use a value object.

### 10. **Inconsistent Null Checks**
**Location**: Various aggregate methods
**Severity**: Low
**Issue**: Some methods check for null collections, others don't. Inconsistent pattern.

**Example**: `ServiceOffer.Create()` checks `servicePricings == null || servicePricings.Count == 0`, but `Booking.Create()` only checks `pricingSnapshots == null || pricingSnapshots.Count == 0`. Consider using null-coalescing or consistent validation.

### 11. **Missing AggregateId in EntityReadModel Base**
**Location**: `Domain/Abstractions/EntityReadModel.cs`
**Severity**: Low
**Issue**: `EntityReadModel` doesn't have `AggregateId`, but all read models need it. This causes duplication.

**Suggestion**: Consider adding `AggregateId` to base class if all read models need it.

### 12. **ServiceOfferQueryStore Query Issue**
**Location**: `Infrastructure/EntityFramework/Repositories/ServiceOfferQueryStore.cs` (line 49)
**Severity**: Low
**Issue**: Query filters by `CleanerAggregateId` but the method name suggests it should filter by service offer aggregate IDs. The query logic seems correct, but the naming could be clearer.

### 13. **Missing ArchivedAt Initialization**
**Location**: All aggregate `Create()` methods
**Severity**: Low
**Issue**: `ArchivedAt` is not initialized in factory methods. Should be set to `default(DateTimeOffset)` or `DateTimeOffset.MinValue` for clarity.

**Fix**: 
```csharp
ArchivedAt = default(DateTimeOffset),
// or
ArchivedAt = DateTimeOffset.MinValue,
```

## 🔵 Performance & Best Practices

### 14. **Query Store Pagination Count Issue**
**Location**: All query stores with pagination
**Severity**: Low
**Issue**: `GetCount()` methods don't filter by `IsArchived` or `IsActive`, causing count mismatch with paginated results.

**Fix**: Apply same filters in count queries:
```csharp
public async Task<int> GetBookingsCount(CancellationToken cancellationToken)
{
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
    return await dbContext.Bookings
        .Where(b => !b.IsArchived)  // Add filters
        .CountAsync(cancellationToken);
}
```

### 15. **Missing Indexes**
**Location**: Entity Framework configurations
**Severity**: Low
**Issue**: Some frequently queried fields might benefit from indexes (e.g., `CustomerAggregateId` in Bookings, `ServiceOfferAggregateId` in Bookings).

**Suggestion**: Review query patterns and add indexes for foreign key lookups.

### 16. **Compiled Query Parameter Count**
**Location**: Query stores
**Severity**: Low
**Issue**: Some compiled queries use `IEnumerable<Guid>` which might not be optimal. Consider using arrays or lists for better performance.

## 📝 Recommendations

1. **Add Unit Tests**: Especially for business logic in aggregates
2. **Add Integration Tests**: For query stores and EF configurations
3. **Consider Domain Events**: Implement the TODO comments for domain events
4. **Add Validation Attributes**: Consider FluentValidation for command validation
5. **Add Logging**: For domain operations and query execution
6. **Consider Soft Delete Pattern**: Already have `IsArchived`, but ensure it's consistently used
7. **Add Concurrency Handling**: RowVersion is configured, but ensure it's used in update operations
8. **Review Aggregate Boundaries**: Ensure `BookingReview` as an entity within `Booking` aggregate is correct vs having separate `Review` aggregate

## ✅ Positive Aspects

- Good encapsulation with private setters
- Proper factory methods
- Good use of value objects
- Compiled queries for performance
- Proper async/await patterns
- Good separation of concerns

