---
title: "Result<T> Monad"
sidebar:
  order: 1
---

# Result&lt;T&gt; Monad

The `Result<T>` monad is designed for modeling operations that can either fail or return a value. It is a generic type, with `T` representing the type of the value returned by the successful operation.

## Design Goals

`Result<T>` provides a set of methods that facilitate chaining operations in a functional way:

| Method | Purpose |
|--------|---------|
| `Map` | Transform the value (happy flow) |
| `MapError` | Transform the error (unhappy flow) |
| `Bind` | Chain operations returning `Result<T>` |
| `Match` | Handle both success and failure paths |
| `RecoverWith` | Recover from an error |
| `Ensure` | Assert a condition on the value |
| `IfSuccess` | Execute side effects on success |
| `IfFailure` | Execute side effects on failure |

## Creating Results

```csharp
// Success
var success = Result.Success(42);
var unitSuccess = Result.Unit; // For void operations

// Failure
var failure = Result.Failure<int>("Something went wrong");
var customFailure = Result.Failure<int>(new CustomError("Details"));

// From values
var fromValue = Result.From(42);
```

## Core Operations

### Map

Transforms the value if the result is successful:

```csharp
var result = Result.Success(42);
var doubled = result.Map(x => x * 2); // Result<int> with 84
var text = result.Map(x => $"Value: {x}"); // Result<string>
```

### Bind

Chains operations that return `Result<T>`:

```csharp
public Result<int> ParseNumber(string text) =>
    int.TryParse(text, out var num)
        ? Result.Success(num)
        : Result.Failure<int>("Invalid number");

var result = ParseNumber("42")
    .Bind(n => ValidateRange(n))
    .Map(n => n * 2);
```

### Match

Handles both success and failure cases:

```csharp
var message = result.Match(
    value => $"Success: {value}",
    error => $"Failed: {error.Message}"
);
```

## Unsafe Methods

Use sparingly - these deviate from functional paradigm:

```csharp
// Extract value (throws if failure)
var value = result.Value;
var value2 = result.GetValueOrThrow();

// Extract error (throws if success)
var error = failedResult.Error;

// Throw if failure
result.ThrowIfFailure();
```

## See Also

- [Error Types](./error-types)
- [Maybe&lt;T&gt; Monad](./maybe-monad)
- [Async Programming](./async-programming)
