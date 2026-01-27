---
title: "runtimeerror"
sidebar_position: 99
---

# RuntimeError

Namespace: Bogoware.Monads

Runtime errors are errors that depends on external factors
 and are to be considered as exceptional.
 For example: network errors, file system errors, etc.

```csharp
public sealed class RuntimeError : Error
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Error](./bogoware.monads.error) → [RuntimeError](./bogoware.monads.runtimeerror)<br />
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Exception**

```csharp
public Exception Exception { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br />

### **Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

## Constructors

### **RuntimeError(Exception)**

```csharp
public RuntimeError(Exception exception)
```

#### Parameters

`exception` [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br />
