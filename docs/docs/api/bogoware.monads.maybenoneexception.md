---
title: "maybenoneexception"
sidebar_position: 99
---

# MaybeNoneException

Namespace: Bogoware.Monads

Thrown when attempting to instantiate a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) from an empty value.

```csharp
public class MaybeNoneException : System.ArgumentNullException, System.Runtime.Serialization.ISerializable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception) → [SystemException](https://docs.microsoft.com/en-us/dotnet/api/system.systemexception) → [ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception) → [ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentnullexception) → [MaybeNoneException](./bogoware.monads.maybenoneexception)<br />
Implements [ISerializable](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iserializable)<br />
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Message**

```csharp
public string Message { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

### **ParamName**

```csharp
public string ParamName { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

### **TargetSite**

```csharp
public MethodBase TargetSite { get; }
```

#### Property Value

[MethodBase](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase)<br />

### **Data**

```csharp
public IDictionary Data { get; }
```

#### Property Value

[IDictionary](https://docs.microsoft.com/en-us/dotnet/api/system.collections.idictionary)<br />

### **InnerException**

```csharp
public Exception InnerException { get; }
```

#### Property Value

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br />

### **HelpLink**

```csharp
public string HelpLink { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

### **Source**

```csharp
public string Source { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

### **HResult**

```csharp
public int HResult { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br />

### **StackTrace**

```csharp
public string StackTrace { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br />

## Constructors

### **MaybeNoneException()**

```csharp
public MaybeNoneException()
```

### **MaybeNoneException(Exception)**

```csharp
public MaybeNoneException(Exception inner)
```

#### Parameters

`inner` [Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br />

## Events

### **SerializeObjectState**

#### Caution

BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more information.

---

```csharp
protected event EventHandler<SafeSerializationEventArgs> SerializeObjectState;
```
