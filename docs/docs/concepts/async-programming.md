---
sidebar_position: 4
title: Async Programming
---

# Async Programming with Monads

Both `Result<T>` and `Maybe<T>` provide full async support for all major operations.

## Async Result Operations

### Async Map

```csharp
var result = await Result.Success("file.txt")
    .Map(async fileName => await File.ReadAllTextAsync(fileName));
```

### Async Bind

```csharp
public async Task<Result<User>> GetUserAsync(int id) =>
    await ValidateId(id)
        .Bind(async validId => await database.GetUserAsync(validId));
```

### Async Side Effects

```csharp
var result = await CreateUserAsync(email)
    .IfSuccess(async user => await SendWelcomeEmailAsync(user))
    .IfFailure(async error => await LogErrorAsync(error));
```

### Async Match

```csharp
var message = await result.Match(
    async user => await FormatUserDetailsAsync(user),
    async error => await FormatErrorMessageAsync(error)
);
```

### Async Ensure

```csharp
var validated = await result
    .Ensure(async user => await IsUserActiveAsync(user),
            new LogicError("User is not active"));
```

## Async Maybe Operations

### Async Map

```csharp
var maybe = await Maybe.Some("data")
    .Map(async data => await ProcessDataAsync(data));
```

### Async Bind

```csharp
var result = await Maybe.Some(userId)
    .Bind(async id => await FindUserAsync(id));
```

### Async Side Effects

```csharp
await maybe
    .IfSome(async value => await ProcessValueAsync(value))
    .IfNone(async () => await HandleMissingValueAsync());
```

## Task Extensions

All methods work seamlessly with `Task<Result<T>>` and `Task<Maybe<T>>`:

```csharp
public async Task<Result<ProcessedData>> ProcessUserDataAsync(int userId)
{
    return await GetUserAsync(userId)          // Task<Result<User>>
        .Bind(async user => await GetUserDataAsync(user.Id))
        .Map(async data => await ProcessDataAsync(data))
        .IfSuccess(async result => await CacheResultAsync(result));
}
```

## Safe Async Execution

```csharp
// Catches exceptions as RuntimeError
var result = await Result.Execute(async () =>
    await RiskyAsyncOperation()
);
```

## See Also

- [Result&lt;T&gt; Monad](./result-monad)
- [Maybe&lt;T&gt; Monad](./maybe-monad)
