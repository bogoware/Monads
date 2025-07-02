# Static Analyzer Warnings for Lazy Evaluation

This document explains the static analyzer warnings introduced to help developers use monads efficiently by leveraging lazy evaluation.

## Overview

The Bogoware.Monads library includes static analyzer warnings that inform developers when they might be missing out on lazy evaluation benefits. These warnings appear when you pass direct values or function calls to methods that have lazy alternatives.

## Affected Methods

### Maybe<T>

- `Map(TValue value)` → Use `Map(() => value)` instead
- `WithDefault(TValue value)` → Use `WithDefault(() => value)` instead

### Result<T>

- `RecoverWith(TValue value)` → Use `RecoverWith(() => value)` instead

### Async Variants

- `Task<Maybe<T>>.WithDefault(TValue value)` → Use `WithDefault(() => value)` instead
- `Task<Result<T>>.RecoverWith(TValue value)` → Use `RecoverWith(() => value)` instead

## Why These Warnings Matter

### The Problem: Eager Evaluation

When you pass a direct value or function call result to these methods, the evaluation happens immediately, even when the monad is in a state where the value won't be used:

```csharp
var maybe = Maybe.None<string>();

// BAD: ExpensiveMethod() is called even though maybe is None
var result = maybe.Map(ExpensiveMethod()); // ⚠️ Warning

// BAD: The expensive computation happens regardless
var withDefault = maybe.WithDefault(PerformExpensiveComputation()); // ⚠️ Warning
```

### The Solution: Lazy Evaluation

By wrapping your values in lambda expressions, evaluation is deferred until actually needed:

```csharp
var maybe = Maybe.None<string>();

// GOOD: ExpensiveMethod() is NOT called because maybe is None
var result = maybe.Map(() => ExpensiveMethod()); // ✅ No warning

// GOOD: Expensive computation only happens if maybe is None
var withDefault = maybe.WithDefault(() => PerformExpensiveComputation()); // ✅ No warning
```

## Performance Impact

The difference can be significant:

```csharp
// Performance test with Maybe.None
var maybe = Maybe.None<string>();

// Eager evaluation: always calls expensive method (1 call)
var eagerResult = maybe.Map(ExpensiveMethod()); // Method called unnecessarily

// Lazy evaluation: doesn't call expensive method (0 calls)
var lazyResult = maybe.Map(() => ExpensiveMethod()); // Method not called
```

## When to Use Each Approach

### Use Direct Values When:
- The value is a compile-time constant
- The value computation is very cheap
- You're okay with suppressing the warning with `#pragma warning disable CS0618`

### Use Lambda Expressions When:
- The value involves method calls
- The computation is expensive (I/O, database calls, complex calculations)
- You want optimal performance

## Suppressing Warnings

If you intentionally want to use direct values, you can suppress the warnings:

```csharp
#pragma warning disable CS0618
var result = maybe.Map(someValue);
#pragma warning restore CS0618
```

## Examples

### Good Patterns ✅

```csharp
// Using lambdas for lazy evaluation
maybe.Map(() => GetUserName())
maybe.WithDefault(() => "Default Value")
result.RecoverWith(() => GetFallbackValue())

// Direct constants (acceptable)
maybe.Map(() => "constant")
maybe.WithDefault(() => 42)
```

### Patterns That Generate Warnings ⚠️

```csharp
// These will generate compiler warnings
maybe.Map(GetUserName())           // Method called eagerly
maybe.WithDefault("Default Value") // String evaluated eagerly  
result.RecoverWith(GetFallbackValue()) // Method called eagerly
```

## Migration Guide

To migrate existing code that generates warnings:

1. **Identify the warning**: Look for CS0618 warnings in your build output
2. **Wrap in lambda**: Change `method(value)` to `method(() => value)`
3. **Test**: Ensure behavior is still correct
4. **Performance**: Monitor if you see the expected performance improvements

### Before:
```csharp
maybe.Map(ProcessData(input))
maybe.WithDefault(GetDefaultValue())
```

### After:
```csharp
maybe.Map(() => ProcessData(input))
maybe.WithDefault(() => GetDefaultValue())
```

This simple change ensures that `ProcessData()` and `GetDefaultValue()` are only called when actually needed.