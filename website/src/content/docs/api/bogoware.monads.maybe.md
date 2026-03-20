---
title: Maybe
sidebar:
  order: 99
---


Namespace: Bogoware.Monads

```csharp
public static class Maybe
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Maybe](./bogoware.monads.maybe)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Methods

### **From&lt;T&gt;(T)**

```csharp
public static Maybe<T> From<T>(T value)
```

#### Type Parameters

`T`<br>

#### Parameters

`value` T<br>

#### Returns

Maybe&lt;T&gt;<br>

### **From&lt;T&gt;(Maybe&lt;T&gt;)**

```csharp
public static Maybe<T> From<T>(Maybe<T> maybe)
```

#### Type Parameters

`T`<br>

#### Parameters

`maybe` Maybe&lt;T&gt;<br>

#### Returns

Maybe&lt;T&gt;<br>

### **Some&lt;T&gt;(T)**

```csharp
public static Maybe<T> Some<T>(T value)
```

#### Type Parameters

`T`<br>

#### Parameters

`value` T<br>

#### Returns

Maybe&lt;T&gt;<br>

### **Some&lt;T&gt;(Maybe&lt;T&gt;)**

```csharp
public static Maybe<T> Some<T>(Maybe<T> maybe)
```

#### Type Parameters

`T`<br>

#### Parameters

`maybe` Maybe&lt;T&gt;<br>

#### Returns

Maybe&lt;T&gt;<br>

### **None&lt;T&gt;()**

```csharp
public static Maybe<T> None<T>()
```

#### Type Parameters

`T`<br>

#### Returns

Maybe&lt;T&gt;<br>

### **None()**

```csharp
public static Maybe<Unit> None()
```

#### Returns

[Maybe&lt;Unit&gt;](./bogoware.monads.maybe-1)<br>
