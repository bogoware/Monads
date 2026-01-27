---
title: "iresult"
sidebar_position: 99
---

# IResult

Namespace: Bogoware.Monads

```csharp
public interface IResult
```

Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Properties

### **IsSuccess**

```csharp
public abstract bool IsSuccess { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br />

### **IsFailure**

```csharp
public abstract bool IsFailure { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br />

## Methods

### **GetErrorOrThrow()**

```csharp
Error GetErrorOrThrow()
```

#### Returns

[Error](./bogoware.monads.error)<br />
