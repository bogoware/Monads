---
title: ResultFailedException
sidebar:
  order: 99
---

# ResultFailedException

Namespace: Bogoware.Monads

Exception thrown when attempting to get the value of a [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 where [Result&lt;TValue&gt;.IsFailure](./bogoware.monads.result-1#isfailure) is `true`.

```csharp
public class ResultFailedException : ResultInvalidOperationException, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) → [SystemException](https://docs.microsoft.com/en-us/dotnet/api/system.systemexception) → [InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception) → [ResultInvalidOperationException](./bogoware.monads.resultinvalidoperationexception) → [ResultFailedException](./bogoware.monads.resultfailedexception)<br>
Implements [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Error**

The error of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) that failed.

```csharp
public Error Error { get; }
```

#### Property Value

[Error](./bogoware.monads.error)<br>

### **TargetSite**

```csharp
public MethodBase TargetSite { get; }
```

#### Property Value

[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br>

### **Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Data**

```csharp
public IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br>

### **InnerException**

```csharp
public Exception InnerException { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

### **HelpLink**

```csharp
public string HelpLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Source**

```csharp
public string Source { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HResult**

```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **StackTrace**

```csharp
public string StackTrace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **ResultFailedException(Error)**

```csharp
public ResultFailedException(Error error)
```

#### Parameters

`error` [Error](./bogoware.monads.error)<br>

## Events

### **SerializeObjectState**

#### Caution

BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more information.

---

```csharp
protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState;
```
