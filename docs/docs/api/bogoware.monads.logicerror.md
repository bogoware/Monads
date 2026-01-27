---
title: "logicerror"
sidebar_position: 99
---

# LogicError

Namespace: Bogoware.Monads

Represents a logic error, that is an error that is caused by a
 logical flaw in the application.
 A logic error is not caused by an external factor,
 such as a network error or a file system error.
 Logic errors are usually caused by invalid input or invalid state and should be
 treated programmatically.
 
 This class can be further inherited to model specific domain error needs.

```csharp
public class LogicError : Error, System.IEquatable`1[[Bogoware.Monads.LogicError, Bogoware.Monads, Version=11.0.0.0, Culture=neutral, PublicKeyToken=null]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Error](./bogoware.monads.error) → [LogicError](./bogoware.monads.logicerror)<br />
Implements [IEquatable&lt;LogicError&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)<br />
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

## Constructors

### **LogicError(String)**

```csharp
public LogicError(string message)
```

#### Parameters

`message` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

## Methods

### **Equals(LogicError)**

```csharp
public bool Equals(LogicError other)
```

#### Parameters

`other` [LogicError](./bogoware.monads.logicerror)<br />

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br />

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br />

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br />

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br />

### **Deconstruct(String&)**

```csharp
public void Deconstruct(String& message)
```

#### Parameters

`message` [String&](https://docs.microsoft.com/en-us/dotnet/api/system.string&)<br />

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />
