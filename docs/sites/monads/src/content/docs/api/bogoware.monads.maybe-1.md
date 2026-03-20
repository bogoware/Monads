---
title: "Maybe<TValue>"
sidebar:
  order: 99
---

# Maybe&lt;TValue&gt;

Namespace: Bogoware.Monads

Represents an optional value.

```csharp
public struct Maybe<TValue>
```

#### Type Parameters

`TValue`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)<br>
Implements IMaybe&lt;TValue&gt;, [IMaybe](./bogoware.monads.imaybe), IEquatable&lt;Maybe&lt;TValue&gt;&gt;, IEnumerable&lt;TValue&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [IsReadOnlyAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.isreadonlyattribute)

## Fields

### **None**

Returns the singleton instance of [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) representing the none state.

```csharp
public static Maybe<TValue> None;
```

## Properties

### **IsSome**

Is `true` if the maybe is some, otherwise `false`.

```csharp
public bool IsSome { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsNone**

Is `true` if the maybe is none, otherwise `false`.

```csharp
public bool IsNone { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Value**

Gets the value if the maybe is some, otherwise the `default`.

```csharp
public TValue Value { get; }
```

#### Property Value

TValue<br>

## Constructors

### **Maybe(TValue)**

Initializes a new instance of the [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1).

```csharp
Maybe(TValue value)
```

#### Parameters

`value` TValue<br>

### **Maybe(Maybe&lt;TValue&gt;)**

Initializes a new instance of the [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1).

```csharp
Maybe(Maybe<TValue> maybe)
```

#### Parameters

`maybe` [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)<br>

## Methods

### **GetValueOrThrow()**

Returns the value if the maybe is some, otherwise throws an exception.

```csharp
TValue GetValueOrThrow()
```

#### Returns

TValue<br>

#### Exceptions

[MaybeNoneException](./bogoware.monads.maybenoneexception)<br>

### **Map&lt;TNewValue&gt;(Func&lt;TValue, TNewValue&gt;)**

Map the value to a new one.

```csharp
Maybe<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> map)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`map` Func&lt;TValue, TNewValue&gt;<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **Map&lt;TNewValue&gt;(Func&lt;TValue, Task&lt;TNewValue&gt;&gt;)**

```csharp
Task<Maybe<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> map)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`map` Func&lt;TValue, Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TNewValue&gt;(Func&lt;TValue, Maybe&lt;TNewValue&gt;&gt;)**

Bind the maybe and, possibly, to a new one.

```csharp
Maybe<TNewValue> Bind<TNewValue>(Func<TValue, Maybe<TNewValue>> map)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`map` Func&lt;TValue, Maybe&lt;TNewValue&gt;&gt;<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **Bind&lt;TNewValue&gt;(Func&lt;TValue, Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;)**

```csharp
Task<Maybe<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Maybe<TNewValue>>> map)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`map` Func&lt;TValue, Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, TResult&gt;, TResult)**

Maps a new value for both state of a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)

```csharp
TResult Match<TResult>(Func<TValue, TResult> mapValue, TResult none)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`mapValue` Func&lt;TValue, TResult&gt;<br>

`none` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, TResult&gt;, Func&lt;TResult&gt;)**

```csharp
TResult Match<TResult>(Func<TValue, TResult> mapValue, Func<TResult> none)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`mapValue` Func&lt;TValue, TResult&gt;<br>

`none` Func&lt;TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, Task&lt;TResult&gt;&gt;, TResult)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, TResult none)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`mapValue` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`none` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;TResult&gt;)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, Func<TResult> none)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`mapValue` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`none` Func&lt;TResult&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> mapValue, Func<Task<TResult>> none)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`mapValue` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`none` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, TResult&gt;, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, TResult> mapValue, Func<Task<TResult>> none)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`mapValue` Func&lt;TValue, TResult&gt;<br>

`none` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **IfSome(Action&lt;TValue&gt;)**

Execute the action if the [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) is `Some`.

```csharp
Maybe<TValue> IfSome(Action<TValue> action)
```

#### Parameters

`action` Action&lt;TValue&gt;<br>

#### Returns

[Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)<br>

### **IfSome(Func&lt;TValue, Task&gt;)**

```csharp
Task<Maybe<TValue>> IfSome(Func<TValue, Task> action)
```

#### Parameters

`action` Func&lt;TValue, Task&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **GetValue(TValue)**

Retrieve the value if present or return the `defaultValue` if missing.

```csharp
TValue GetValue(TValue defaultValue)
```

#### Parameters

`defaultValue` TValue<br>

#### Returns

TValue<br>

### **GetValue(Func&lt;TValue&gt;)**

```csharp
TValue GetValue(Func<TValue> defaultValue)
```

#### Parameters

`defaultValue` Func&lt;TValue&gt;<br>

#### Returns

TValue<br>

### **GetValue(Func&lt;Task&lt;TValue&gt;&gt;)**

```csharp
Task<TValue> GetValue(Func<Task<TValue>> defaultValue)
```

#### Parameters

`defaultValue` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;TValue&gt;<br>

### **OfType&lt;TNewValue&gt;()**

Downcast to `TNew` if possible, otherwise returns a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)
 that is actually None case.

```csharp
Maybe<TNewValue> OfType<TNewValue>()
```

#### Type Parameters

`TNewValue`<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **Equals(Object)**

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Equals(Maybe&lt;TValue&gt;)**

```csharp
bool Equals(Maybe<TValue> other)
```

#### Parameters

`other` [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
