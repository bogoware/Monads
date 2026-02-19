---
title: "MaybeNoneError"
sidebar_position: 99
---

# MaybeNoneError

Namespace: Bogoware.Monads

```csharp
public class MaybeNoneError : LogicError, System.IEquatable`1[[Bogoware.Monads.LogicError, Bogoware.Monads, Version=11.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Error](./bogoware.monads.error) → [LogicError](./bogoware.monads.logicerror) → [MaybeNoneError](./bogoware.monads.maybenoneerror)<br>
Implements [IEquatable&lt;LogicError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Fields

### **Default**

```csharp
public static MaybeNoneError Default;
```

## Properties

### **Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **MaybeNoneError(String)**

```csharp
public MaybeNoneError(string message)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
