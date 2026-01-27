---
sidebar_position: 1
slug: /
title: Introduction
---

# Bogoware Monads

![Nuget](https://img.shields.io/nuget/dt/Bogoware.Monads?logo=nuget&style=plastic) ![Nuget](https://img.shields.io/nuget/v/Bogoware.Monads?style=plastic)

_A functional programming library for C# providing `Result<T>` and `Maybe<T>` monads_

**Supported Platforms:** .NET Standard 2.1 | .NET 8 | .NET 9 | .NET 10

[Changelog](./changelog) | [NuGet Package](https://www.nuget.org/packages/Bogoware.Monads) | [GitHub Repository](https://github.com/bogoware/monads)

## Quick Start

Install from NuGet and start using the monads in your C# projects:

```shell
dotnet add package Bogoware.Monads
```

### Your First Maybe Example

Let's start with a simple example using `Maybe<T>` to handle optional values safely:

```csharp
using Bogoware.Monads;

// Traditional approach with null checks
public string GetFullName(string firstName, string? lastName)
{
    if (lastName != null)
        return $"{firstName} {lastName}";
    return firstName;
}

// Using Maybe<T> for safer optional handling
public record Person(string FirstName, Maybe<string> LastName);

public string GetFullNameSafe(Person person)
{
    return person.LastName
        .Map(last => $"{person.FirstName} {last}")
        .GetValue(person.FirstName);
}

// Usage
var personWithLastName = new Person("John", Maybe.Some("Doe"));
var personWithoutLastName = new Person("Jane", Maybe.None<string>());

Console.WriteLine(GetFullNameSafe(personWithLastName));   // "John Doe"
Console.WriteLine(GetFullNameSafe(personWithoutLastName)); // "Jane"
```

### Your First Result Example

Now let's see how `Result<T>` handles operations that can fail:

```csharp
using Bogoware.Monads;

// Using Result<T> for explicit error handling
public Result<User> CreateUserSafe(string email, string password)
{
    return ValidateEmail(email)
        .Bind(() => ValidatePassword(password))
        .Map(() => new User(email, password));
}

public Result<Unit> ValidateEmail(string email)
{
    if (string.IsNullOrEmpty(email) || !email.Contains("@"))
        return Result.Failure<Unit>("Invalid email address");

    return Result.Unit;
}

public Result<Unit> ValidatePassword(string password)
{
    if (password.Length < 8)
        return Result.Failure<Unit>("Password must be at least 8 characters");

    return Result.Unit;
}

// Usage
var successResult = CreateUserSafe("john@example.com", "secure123");
var failureResult = CreateUserSafe("invalid-email", "short");

successResult.Match(
    user => $"User created: {user.Email}",
    error => $"Error: {error.Message}"
);
```

## Introduction to Monads

Monads are powerful tools for modeling operations in a functional way, making them a cornerstone of functional programming. For the purpose of this introduction, we can consider monads as an abstraction of a _safe container_ that encapsulates the result of an operation.

They provide methods that enable manipulation of the result in a safe manner, ensuring that the execution flow follows the "happy" path in case of success and the "unhappy" path in case of failure. This model is also known as _railway-oriented programming_.

By employing monads, code can be protected from further processing in case of errors or missing data. Adopting a functional approach offers benefits such as increased readability, improved reasoning capabilities, and more robust and error-resistant code.

## Library Overview

This library provides two well-known monads:

| Monad | Purpose |
|-------|---------|
| `Result<T>` | Model operations that can fail |
| `Maybe<T>` | Model operations that can optionally return a value |

Additionally, the library provides the `Error` abstract class, which complements the `Result<T>` monad and offers an ergonomic approach to error management at an application-wide scale.

## Next Steps

- **[Getting Started](./getting-started/installation)** - Installation and setup
- **[Concepts](./concepts/result-monad)** - Deep dive into Result and Maybe
- **[API Reference](./api)** - Complete API documentation
