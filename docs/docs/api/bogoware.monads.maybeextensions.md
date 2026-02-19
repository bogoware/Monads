---
title: "MaybeExtensions"
sidebar_position: 99
---

# MaybeExtensions

Namespace: Bogoware.Monads

```csharp
public static class MaybeExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MaybeExtensions](./bogoware.monads.maybeextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Map&lt;TValue, TNewValue&gt;(Maybe&lt;TValue&gt;, TNewValue)**

Map the value to a new one.

```csharp
public static Maybe<TNewValue> Map<TValue, TNewValue>(Maybe<TValue> maybe, TNewValue value)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`value` TNewValue<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Maybe&lt;TValue&gt;, Func&lt;TNewValue&gt;)**

```csharp
public static Maybe<TNewValue> Map<TValue, TNewValue>(Maybe<TValue> maybe, Func<TNewValue> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`map` Func&lt;TNewValue&gt;<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Maybe&lt;TValue&gt;, Func&lt;Task&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Map<TValue, TNewValue>(Maybe<TValue> maybe, Func<Task<TNewValue>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`map` Func&lt;Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Maybe&lt;TValue&gt;, Func&lt;Maybe&lt;TNewValue&gt;&gt;)**

Bind the maybe and, possibly, to a new one.

```csharp
public static Maybe<TNewValue> Bind<TValue, TNewValue>(Maybe<TValue> maybe, Func<Maybe<TNewValue>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`map` Func&lt;Maybe&lt;TNewValue&gt;&gt;<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Maybe&lt;TValue&gt;, Func&lt;Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(Maybe<TValue> maybe, Func<Task<Maybe<TNewValue>>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`map` Func&lt;Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, TResult, TResult)**

Maps a new value for both state of a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1)

```csharp
public static TResult Match<TValue, TResult>(Maybe<TValue> maybe, TResult resultOnValue, TResult resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` TResult<br>

`resultOnNone` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, Func&lt;TResult&gt;, TResult)**

```csharp
public static TResult Match<TValue, TResult>(Maybe<TValue> maybe, Func<TResult> resultOnValue, TResult resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` Func&lt;TResult&gt;<br>

`resultOnNone` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, TResult, Func&lt;TResult&gt;)**

```csharp
public static TResult Match<TValue, TResult>(Maybe<TValue> maybe, TResult resultOnValue, Func<TResult> resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` TResult<br>

`resultOnNone` Func&lt;TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, Func&lt;TResult&gt;, Func&lt;TResult&gt;)**

```csharp
public static TResult Match<TValue, TResult>(Maybe<TValue> maybe, Func<TResult> resultOnValue, Func<TResult> resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` Func&lt;TResult&gt;<br>

`resultOnNone` Func&lt;TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, Func&lt;Task&lt;TResult&gt;&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Maybe<TValue> maybe, Func<Task<TResult>> resultOnValue, TResult resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` Func&lt;Task&lt;TResult&gt;&gt;<br>

`resultOnNone` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, TResult, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Maybe<TValue> maybe, TResult resultOnValue, Func<Task<TResult>> resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` TResult<br>

`resultOnNone` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Maybe&lt;TValue&gt;, Func&lt;Task&lt;TResult&gt;&gt;, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Maybe<TValue> maybe, Func<Task<TResult>> resultOnValue, Func<Task<TResult>> resultOnNone)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`resultOnValue` Func&lt;Task&lt;TResult&gt;&gt;<br>

`resultOnNone` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **ToMaybe&lt;TValue&gt;(IEnumerable&lt;TValue&gt;)**

Returns a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) with `Some(first)` in case of non empty list.

```csharp
public static Maybe<TValue> ToMaybe<TValue>(IEnumerable<TValue> values)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`values` IEnumerable&lt;TValue&gt;<br>

#### Returns

Maybe&lt;TValue&gt;<br>

### **Where&lt;TValue&gt;(TValue, Func&lt;TValue, Boolean&gt;)**

Returns the original `Some` if predicate is satisfied, `None` otherwise.

```csharp
public static Maybe<TValue> Where<TValue>(TValue obj, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`obj` TValue<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

Maybe&lt;TValue&gt;<br>

### **WhereNot&lt;TValue&gt;(TValue, Func&lt;TValue, Boolean&gt;)**

Returns the original `Some` if predicate is not satisfied, `None` otherwise.

```csharp
public static Maybe<TValue> WhereNot<TValue>(TValue obj, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`obj` TValue<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

Maybe&lt;TValue&gt;<br>

### **Satisfy&lt;TValue&gt;(Maybe&lt;TValue&gt;, Func&lt;TValue, Boolean&gt;)**

Evaluate the `predicate` applied to the value if present.
 Return `false` in case of `None`.

```csharp
public static bool Satisfy<TValue>(Maybe<TValue> maybe, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Satisfy&lt;TValue&gt;(Maybe&lt;TValue&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;)**

```csharp
public static Task<bool> Satisfy<TValue>(Maybe<TValue> maybe, Func<TValue, Task<bool>> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Execute&lt;TValue&gt;(Maybe`1&, Action&lt;Maybe&lt;TValue&gt;&gt;)**

Execute the action.

```csharp
public static Maybe`1& Execute<TValue>(Maybe`1& maybe, Action<Maybe<TValue>> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe`1&<br>

`action` Action&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

Maybe`1&<br>

### **Execute&lt;TValue&gt;(Maybe&lt;TValue&gt;, Func&lt;Maybe&lt;TValue&gt;, Task&gt;)**

```csharp
public static Task<Maybe<TValue>> Execute<TValue>(Maybe<TValue> maybe, Func<Maybe<TValue>, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`action` Func&lt;Maybe&lt;TValue&gt;, Task&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **IfSome&lt;TValue&gt;(Maybe`1&, Action)**

Execute the action if the [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) is `Some`.

```csharp
public static Maybe`1& IfSome<TValue>(Maybe`1& maybe, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe`1&<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Maybe`1&<br>

### **IfSome&lt;TNewValue&gt;(Maybe&lt;TNewValue&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Maybe<TNewValue>> IfSome<TNewValue>(Maybe<TNewValue> maybe, Func<Task> action)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TNewValue&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **IfNone&lt;TNewValue&gt;(Maybe`1&, Action)**

Execute the action if the [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) is `None`.

```csharp
public static Maybe`1& IfNone<TNewValue>(Maybe`1& maybe, Action action)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`maybe` Maybe`1&<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Maybe`1&<br>

### **IfNone&lt;TNewValue&gt;(Maybe&lt;TNewValue&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Maybe<TNewValue>> IfNone<TNewValue>(Maybe<TNewValue> maybe, Func<Task> action)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TNewValue&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **WithDefault&lt;TNewValue&gt;(Maybe&lt;TNewValue&gt;, TNewValue)**

```csharp
public static Maybe<TNewValue> WithDefault<TNewValue>(Maybe<TNewValue> maybe, TNewValue value)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TNewValue&gt;<br>

`value` TNewValue<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **WithDefault&lt;TNewValue&gt;(Maybe&lt;TNewValue&gt;, Func&lt;TNewValue&gt;)**

```csharp
public static Maybe<TNewValue> WithDefault<TNewValue>(Maybe<TNewValue> maybe, Func<TNewValue> value)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TNewValue&gt;<br>

`value` Func&lt;TNewValue&gt;<br>

#### Returns

Maybe&lt;TNewValue&gt;<br>

### **WithDefault&lt;TNewValue&gt;(Maybe&lt;TNewValue&gt;, Func&lt;Task&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> WithDefault<TNewValue>(Maybe<TNewValue> maybe, Func<Task<TNewValue>> value)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`maybe` Maybe&lt;TNewValue&gt;<br>

`value` Func&lt;Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **MapToResult&lt;TValue&gt;(Maybe&lt;TValue&gt;)**

Convert a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) to a [Result&lt;TValue&gt;](./bogoware.monads.result-1) with a default error in case of `None`.

```csharp
public static Result<TValue> MapToResult<TValue>(Maybe<TValue> maybe)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

#### Returns

Result&lt;TValue&gt;<br>

### **MapToResult&lt;TValue&gt;(Maybe&lt;TValue&gt;, Func&lt;Error&gt;)**

Convert a [Maybe&lt;TValue&gt;](./bogoware.monads.maybe-1) to a [Result&lt;TValue&gt;](./bogoware.monads.result-1) with a default error in case of `None`.

```csharp
public static Result<TValue> MapToResult<TValue>(Maybe<TValue> maybe, Func<Error> errorFunc)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`errorFunc` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Result&lt;TValue&gt;<br>

### **MapToResult&lt;TValue&gt;(Maybe&lt;TValue&gt;, Func&lt;Task&lt;Error&gt;&gt;)**

```csharp
public static Task<Result<TValue>> MapToResult<TValue>(Maybe<TValue> maybe, Func<Task<Error>> errorFunc)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Maybe&lt;TValue&gt;<br>

`errorFunc` [Func&lt;Task&lt;Error&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>
