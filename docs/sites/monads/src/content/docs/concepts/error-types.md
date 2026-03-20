---
title: Error Types
sidebar:
  order: 3
---

# Error Types

The `Error` class is used for modeling errors and works in conjunction with the `Result<T>` monad.

## Error Categories

| Type | Purpose |
|------|---------|
| `LogicError` | Application logic errors, can be reported to users |
| `RuntimeError` | Infrastructure errors, should not be exposed |
| `AggregateError` | Collection of multiple errors |
| `MaybeNoneError` | Default error when converting `Maybe.None` to `Result` |

## LogicError

Base class for application logic errors:

```csharp
var error = new LogicError("Invalid input provided");
var result = Result.Failure<string>(error);
```

## RuntimeError

Wraps exceptions that occur during execution:

```csharp
try
{
    var data = await riskyOperation();
    return Result.Success(data);
}
catch (Exception ex)
{
    return Result.Failure<string>(new RuntimeError(ex));
}

// Or use Result.Execute:
var result = Result.Execute(() => riskyOperation());
```

## AggregateError

Contains multiple errors from aggregation:

```csharp
var results = operations.AggregateResults();
// If failures exist, error is AggregateError

if (results.IsFailure && results.Error is AggregateError aggError)
{
    foreach (var error in aggError.Errors)
    {
        Console.WriteLine(error.Message);
    }
}
```

## Custom Error Hierarchies

Each application should model its own error types:

```csharp
public abstract class ApplicationError : LogicError
{
    public int ErrorCode { get; }

    protected ApplicationError(string message, int errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}

public class NotFoundError : ApplicationError
{
    public string ResourceName { get; }

    public NotFoundError(string resource, int code)
        : base($"{resource} not found", code)
    {
        ResourceName = resource;
    }
}

public class ValidationError : ApplicationError
{
    public string Field { get; }

    public ValidationError(string field, string message, int code)
        : base(message, code)
    {
        Field = field;
    }
}
```

## Handling Errors

Distinguish between error types in API responses:

```csharp
// ASP.NET Core example
public IActionResult HandleResult<T>(Result<T> result)
{
    return result.Match(
        value => Ok(value),
        error => error switch
        {
            LogicError => BadRequest(error.Message),
            RuntimeError => StatusCode(500, "Internal error"),
            _ => StatusCode(500)
        }
    );
}
```

## See Also

- [Result&lt;T&gt; Monad](./result-monad)
