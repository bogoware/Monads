---
title: Installation
sidebar:
  order: 1
---

# Installation

## NuGet Package

Install Bogoware.Monads via NuGet:

```shell
dotnet add package Bogoware.Monads
```

Or via the Package Manager Console:

```powershell
Install-Package Bogoware.Monads
```

## Supported Platforms

| Platform | Version |
|----------|---------|
| .NET Standard | 2.1 |
| .NET | 8.0, 9.0, 10.0 |

## Basic Usage

After installation, add the namespace to your code:

```csharp
using Bogoware.Monads;
```

You're now ready to use `Result<T>` and `Maybe<T>` in your projects.

## Next Steps

- Learn about [Result\<T\>](../concepts/result-monad)
- Learn about [Maybe\<T\>](../concepts/maybe-monad)
- Explore [API Reference](../api)
