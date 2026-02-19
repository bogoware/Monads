---
title: "Error"
sidebar_position: 99
---

# Error

Namespace: Bogoware.Monads

Base class for all errors.

```csharp
public abstract class Error
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Error](./bogoware.monads.error)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Message**

The error message.

```csharp
public abstract string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **Error()**

```csharp
protected Error()
```
