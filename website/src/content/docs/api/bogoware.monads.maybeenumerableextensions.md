---
title: MaybeEnumerableExtensions
sidebar:
  order: 99
---


Namespace: Bogoware.Monads

```csharp
public static class MaybeEnumerableExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MaybeEnumerableExtensions](./bogoware.monads.maybeenumerableextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **AllSome(IEnumerable&lt;IMaybe&gt;)**

Determines if all [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)s of a sequence are `Some`s.

```csharp
public static bool AllSome(IEnumerable<IMaybe> maybes)
```

#### Parameters

`maybes` [IEnumerable&lt;IMaybe&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AllSome&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;)**

```csharp
public static bool AllSome<TValue>(IEnumerable<Maybe<TValue>> maybes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AllNone(IEnumerable&lt;IMaybe&gt;)**

Determines if all [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)s of a sequence are `None`s.

```csharp
public static bool AllNone(IEnumerable<IMaybe> maybes)
```

#### Parameters

`maybes` [IEnumerable&lt;IMaybe&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AllNone&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;)**

```csharp
public static bool AllNone<TValue>(IEnumerable<Maybe<TValue>> maybes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnySome(IEnumerable&lt;IMaybe&gt;)**

Determines if any [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) of a sequence is `Some`.

```csharp
public static bool AnySome(IEnumerable<IMaybe> maybes)
```

#### Parameters

`maybes` [IEnumerable&lt;IMaybe&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnySome&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;)**

```csharp
public static bool AnySome<TValue>(IEnumerable<Maybe<TValue>> maybes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnyNone(IEnumerable&lt;IMaybe&gt;)**

Determines if any [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) of a sequence is `None`.

```csharp
public static bool AnyNone(IEnumerable<IMaybe> maybes)
```

#### Parameters

`maybes` [IEnumerable&lt;IMaybe&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnyNone&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;)**

```csharp
public static bool AnyNone<TValue>(IEnumerable<Maybe<TValue>> maybes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SelectValues&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;)**

Extract values from [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)s.
 `None`s are discarded.

```csharp
public static IEnumerable<TValue> SelectValues<TValue>(IEnumerable<Maybe<TValue>> maybes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

IEnumerable&lt;TValue&gt;<br>

### **BindEach&lt;TValue, TNewValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Maybe&lt;TNewValue&gt;&gt;)**

Bind values via the functor.

```csharp
public static IEnumerable<Maybe<TNewValue>> BindEach<TValue, TNewValue>(IEnumerable<Maybe<TValue>> maybes, Func<TValue, Maybe<TNewValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, Maybe&lt;TNewValue&gt;&gt;<br>

#### Returns

IEnumerable&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **MapEach&lt;TValue, TNewValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, TNewValue&gt;)**

Maps values via the functor.

```csharp
public static IEnumerable<Maybe<TNewValue>> MapEach<TValue, TNewValue>(IEnumerable<Maybe<TValue>> maybes, Func<TValue, TNewValue> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, TNewValue&gt;<br>

#### Returns

IEnumerable&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **MatchEach&lt;TValue, TResult&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, TResult)**

Matches maybes via the two functors.

```csharp
public static IEnumerable<TResult> MatchEach<TValue, TResult>(IEnumerable<Maybe<TValue>> maybes, Func<TValue, TResult> mapSuccesses, TResult none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

`mapSuccesses` Func&lt;TValue, TResult&gt;<br>

`none` TResult<br>

#### Returns

IEnumerable&lt;TResult&gt;<br>

### **Where&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;)**

Filters `Some`s via the predicate.
 `None`s are discarded.

```csharp
public static IEnumerable<Maybe<TValue>> Where<TValue>(IEnumerable<Maybe<TValue>> maybes, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

### **WhereNot&lt;TValue&gt;(IEnumerable&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;)**

Filters `Some`s via negated predicate.
 `None`s are discarded.

```csharp
public static IEnumerable<Maybe<TValue>> WhereNot<TValue>(IEnumerable<Maybe<TValue>> maybes, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybes` IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

IEnumerable&lt;Maybe&lt;TValue&gt;&gt;<br>
