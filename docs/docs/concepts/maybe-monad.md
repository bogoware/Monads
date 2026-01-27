---
sidebar_position: 2
title: Maybe<T> Monad
---

# Maybe&lt;T&gt; Monad

The `Maybe<T>` monad is used for modeling operations that can optionally return a value. It provides a safer alternative to `null` with chainable operations.

## Design Goals

The advantage of using `Maybe<T>` over `Nullable<T>` is that `Maybe<T>` provides methods that enable chaining operations in a functional manner.

**Practical rule**: Use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and method parameters.

## Creating Maybe Values

```csharp
// Some value
var some = Maybe.Some("hello");

// None
var none = Maybe.None<string>();

// From nullable
string? nullable = GetOptionalValue();
var maybe = Maybe.From(nullable);
var maybe2 = (Maybe<string>)nullable; // Implicit conversion
```

## Core Operations

### Map

Transforms the value if present:

```csharp
var maybe = Maybe.Some("hello");
var upper = maybe.Map(s => s.ToUpper()); // Maybe<string> with "HELLO"

var none = Maybe.None<string>();
var result = none.Map(s => s.ToUpper()); // Still None
```

### Bind

Chains operations returning `Maybe<T>`:

```csharp
public Maybe<string> ValidateName(string name) =>
    !string.IsNullOrWhiteSpace(name)
        ? Maybe.Some(name.Trim())
        : Maybe.None<string>();

var result = Maybe.Some("  John  ")
    .Bind(ValidateName); // Maybe<string> with "John"
```

### Match

Handles both Some and None cases:

```csharp
var greeting = maybe.Match(
    name => $"Hello, {name}!",
    "Hello, stranger!"
);
```

### GetValue

Retrieves value with fallback:

```csharp
var value = maybe.GetValue("default");
var value2 = maybe.GetValue(() => GetDefaultValue()); // Lazy
```

## Side Effects

```csharp
maybe
    .IfSome(data => Logger.Info($"Got: {data}"))
    .IfNone(() => Logger.Warn("No data"));
```

## Converting to Result

```csharp
var result = maybe
    .MapToResult(() => new LogicError("Value not found"))
    .Bind(ValidateValue);
```

## See Also

- [Result&lt;T&gt; Monad](./result-monad)
- [Error Types](./error-types)
