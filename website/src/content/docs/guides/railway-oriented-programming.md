---
title: Railway-Oriented Programming
sidebar:
  order: 1
---

# Railway-Oriented Programming

Railway-oriented programming is a pattern for modeling the flow of operations that can succeed or fail, visualized as two parallel tracks - the success track and the failure track.

## The Pattern

```
Success Track:  ───●───●───●───●──→ Success
                   │   │   │   │
Failure Track:  ───────────────────→ Failure
```

Each operation either:
- Continues on the success track (if successful)
- Switches to the failure track (if it fails)

Once on the failure track, subsequent operations are skipped.

## Basic Pipeline

```csharp
public Result<ProcessedOrder> ProcessOrder(OrderRequest request)
{
    return ValidateOrderRequest(request)
        .Bind(ValidateCustomer)
        .Bind(ValidateInventory)
        .Bind(CalculatePricing)
        .Map(CreateOrder);
}
```

## Advanced Pipeline with Side Effects

```csharp
public async Task<Result<ProcessedOrder>> ProcessOrderAsync(OrderRequest request)
{
    return await ValidateOrderRequest(request)
        .Bind(ValidateCustomer)
        .Bind(ValidateInventory)
        .Bind(CalculatePricing)
        .Bind(async order => await SaveOrderAsync(order))
        .Bind(async order => await ProcessPaymentAsync(order))
        .IfSuccess(async order => await SendConfirmationAsync(order))
        .IfFailure(async error => await LogErrorAsync(error));
}
```

## Starting Pipelines

Use `Result.Ensure` for cleaner pipeline initialization:

```csharp
// Instead of:
public Result<Unit> Publish()
{
    if (PublishingStatus == PublishingStatus.Published)
        return new InvalidOperationError("Already published");

    return ValidateCostComponents()
        .Bind(ValidateTimingComponents)
        .IfSuccess(() => PublishingStatus = PublishingStatus.Published);
}

// Prefer:
public Result<Unit> Publish() => Result
    .Ensure(PublishingStatus != PublishingStatus.Published,
            () => new InvalidOperationError("Already published"))
    .Bind(ValidateCostComponents)
    .Bind(ValidateTimingComponents)
    .IfSuccess(() => PublishingStatus = PublishingStatus.Published);
```

## Error Recovery

```csharp
var config = LoadConfigFromFile()
    .RecoverWith(() => LoadConfigFromEnvironment())
    .RecoverWith(GetDefaultConfig());
```

## Combining Maybe and Result

```csharp
public Result<UserProfile> GetUserProfile(int userId)
{
    return FindUser(userId)                    // Maybe<User>
        .MapToResult(() => new NotFoundError("User not found"))
        .Bind(user => LoadUserProfile(user))   // Result<UserProfile>
        .Map(profile => EnrichProfile(profile));
}
```

## Benefits

1. **Explicit error handling** - No hidden exceptions
2. **Composable operations** - Easy to add/remove steps
3. **Readable flow** - Clear success/failure paths
4. **Testable** - Each step can be tested in isolation
