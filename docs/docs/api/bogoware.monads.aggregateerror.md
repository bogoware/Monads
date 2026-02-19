---
title: "AggregateError"
sidebar_position: 99
---

# AggregateError

Namespace: Bogoware.Monads

An error that aggregates multiple errors.

```csharp
public class AggregateError : Error
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Error](./bogoware.monads.error) → [AggregateError](./bogoware.monads.aggregateerror)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Errors**

The errors that were aggregated.

```csharp
public IEnumerable<Error> Errors { get; }
```

#### Property Value

[IEnumerable&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **AggregateError(String, IEnumerable&lt;Error&gt;)**

Initializes a new instance of the [AggregateError](./bogoware.monads.aggregateerror) class.

```csharp
public AggregateError(string message, IEnumerable<Error> innerErrors)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The error message

`innerErrors` [IEnumerable&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
The inner errors

### **AggregateError(String, Error, Error, Error[])**

```csharp
public AggregateError(string message, Error first, Error second, Error[] others)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`first` [Error](./bogoware.monads.error)<br>

`second` [Error](./bogoware.monads.error)<br>

`others` [Error[]](./bogoware.monads.error)<br>

### **AggregateError(IEnumerable&lt;Error&gt;)**

```csharp
public AggregateError(IEnumerable<Error> innerErrors)
```

#### Parameters

`innerErrors` [IEnumerable&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **AggregateError(Error, Error, Error[])**

```csharp
public AggregateError(Error first, Error second, Error[] others)
```

#### Parameters

`first` [Error](./bogoware.monads.error)<br>

`second` [Error](./bogoware.monads.error)<br>

`others` [Error[]](./bogoware.monads.error)<br>
