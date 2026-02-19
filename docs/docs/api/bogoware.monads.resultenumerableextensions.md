---
title: "ResultEnumerableExtensions"
sidebar_position: 99
---

# ResultEnumerableExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultEnumerableExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultEnumerableExtensions](./bogoware.monads.resultenumerableextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Where&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;)**

Filters `Success`es via the predicate.
 `Failure`s are discarded.

```csharp
public static IEnumerable<Result<TValue>> Where<TValue>(IEnumerable<Result<TValue>> successes, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

### **WhereNot&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;)**

Filters `Success`es via negated predicate.
 `Failure`s are discarded.

```csharp
public static IEnumerable<Result<TValue>> WhereNot<TValue>(IEnumerable<Result<TValue>> successes, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

### **AllSuccess(IEnumerable&lt;IResult&gt;)**

Determines if all [Result&lt;TValue&gt;](./bogoware.monads.result-1)s of a sequence are `Success`s.

```csharp
public static bool AllSuccess(IEnumerable<IResult> successes)
```

#### Parameters

`successes` [IEnumerable&lt;IResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AllSuccess&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static bool AllSuccess<TValue>(IEnumerable<Result<TValue>> successes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AllFailure(IEnumerable&lt;IResult&gt;)**

Determines if all [Result&lt;TValue&gt;](./bogoware.monads.result-1)s of a sequence are `Failure`s.

```csharp
public static bool AllFailure(IEnumerable<IResult> successes)
```

#### Parameters

`successes` [IEnumerable&lt;IResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AllFailure&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static bool AllFailure<TValue>(IEnumerable<Result<TValue>> successes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnySuccess(IEnumerable&lt;IResult&gt;)**

Determines if any [Result&lt;TValue&gt;](./bogoware.monads.result-1) of a sequence is `Success`.

```csharp
public static bool AnySuccess(IEnumerable<IResult> successes)
```

#### Parameters

`successes` [IEnumerable&lt;IResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnySuccess&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static bool AnySuccess<TValue>(IEnumerable<Result<TValue>> successes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnyFailure(IEnumerable&lt;IResult&gt;)**

Determines if any [Result&lt;TValue&gt;](./bogoware.monads.result-1) of a sequence is `Failure`.

```csharp
public static bool AnyFailure(IEnumerable<IResult> successes)
```

#### Parameters

`successes` [IEnumerable&lt;IResult&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **AnyFailure&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static bool AnyFailure<TValue>(IEnumerable<Result<TValue>> successes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SelectValues&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;)**

Extract values from [Result&lt;TValue&gt;](./bogoware.monads.result-1)s.
 `Failure`s are discarded.

```csharp
public static IEnumerable<TValue> SelectValues<TValue>(IEnumerable<Result<TValue>> successes)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`successes` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

IEnumerable&lt;TValue&gt;<br>

### **MapEach&lt;TValue, TNewValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TNewValue&gt;)**

Maps values via the functor.

```csharp
public static IEnumerable<Result<TNewValue>> MapEach<TValue, TNewValue>(IEnumerable<Result<TValue>> results, Func<TValue, TNewValue> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`results` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, TNewValue&gt;<br>

#### Returns

IEnumerable&lt;Result&lt;TNewValue&gt;&gt;<br>

### **BindEach&lt;TValue, TNewValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Result&lt;TNewValue&gt;&gt;)**

Bind values via the functor.

```csharp
public static IEnumerable<Result<TNewValue>> BindEach<TValue, TNewValue>(IEnumerable<Result<TValue>> results, Func<TValue, Result<TNewValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`results` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, Result&lt;TNewValue&gt;&gt;<br>

#### Returns

IEnumerable&lt;Result&lt;TNewValue&gt;&gt;<br>

### **MatchEach&lt;TValue, TResult&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, Func&lt;Error, TResult&gt;)**

Matches results.

```csharp
public static IEnumerable<TResult> MatchEach<TValue, TResult>(IEnumerable<Result<TValue>> results, Func<TValue, TResult> mapSuccesses, Func<Error, TResult> mapFailures)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`results` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

`mapSuccesses` Func&lt;TValue, TResult&gt;<br>

`mapFailures` Func&lt;Error, TResult&gt;<br>

#### Returns

IEnumerable&lt;TResult&gt;<br>

### **MatchEach&lt;TValue, TResult&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, TResult)**

```csharp
public static IEnumerable<TResult> MatchEach<TValue, TResult>(IEnumerable<Result<TValue>> results, Func<TValue, TResult> mapSuccesses, TResult failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`results` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

`mapSuccesses` Func&lt;TValue, TResult&gt;<br>

`failure` TResult<br>

#### Returns

IEnumerable&lt;TResult&gt;<br>

### **AggregateResults&lt;TValue&gt;(IEnumerable&lt;Result&lt;TValue&gt;&gt;)**

Aggregates an enumeration of Result into a Result of an enumeration.
 If all [Result&lt;TValue&gt;](./bogoware.monads.result-1)s are `Success` then return a `Success`[Result&lt;TValue&gt;](./bogoware.monads.result-1).
 otherwise return a `Failure` with an [AggregateError](./bogoware.monads.aggregateerror).

```csharp
public static Result<IEnumerable<TValue>> AggregateResults<TValue>(IEnumerable<Result<TValue>> results)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`results` IEnumerable&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

Result&lt;IEnumerable&lt;TValue&gt;&gt;<br>
