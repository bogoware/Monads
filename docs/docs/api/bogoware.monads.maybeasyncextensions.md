---
title: "MaybeAsyncExtensions"
sidebar_position: 99
---

# MaybeAsyncExtensions

Namespace: Bogoware.Monads

```csharp
public static class MaybeAsyncExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MaybeAsyncExtensions](./bogoware.monads.maybeasyncextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TNewValue&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Map<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<TNewValue> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;TNewValue&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, TNewValue&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Map<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<TValue, TNewValue> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;TValue, TNewValue&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Task&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Map<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<Task<TNewValue>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Map<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<TValue, Task<TNewValue>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;TValue, Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **WithDefault&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, TValue)**

```csharp
public static Task<Maybe<TValue>> WithDefault<TValue>(Task<Maybe<TValue>> maybeTask, TValue value)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` TValue<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **WithDefault&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue&gt;)**

```csharp
public static Task<Maybe<TValue>> WithDefault<TValue>(Task<Maybe<TValue>> maybeTask, Func<TValue> value)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;TValue&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **WithDefault&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Task&lt;TValue&gt;&gt;)**

```csharp
public static Task<Maybe<TValue>> WithDefault<TValue>(Task<Maybe<TValue>> maybeTask, Func<Task<TValue>> value)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Maybe&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<Maybe<TNewValue>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;Maybe&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Maybe&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<TValue, Maybe<TNewValue>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;TValue, Maybe&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<Task<Maybe<TNewValue>>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;)**

```csharp
public static Task<Maybe<TNewValue>> Bind<TValue, TNewValue>(Task<Maybe<TValue>> maybeTask, Func<TValue, Task<Maybe<TNewValue>>> map)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`map` Func&lt;TValue, Task&lt;Maybe&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TNewValue&gt;&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, TResult, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, TResult newValue, TResult none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`newValue` TResult<br>

`none` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, Func<TValue, TResult> value, TResult none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;TValue, TResult&gt;<br>

`none` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, Func<TValue, TResult> value, Func<TResult> none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;TValue, TResult&gt;<br>

`none` Func&lt;TResult&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, Func<TValue, Task<TResult>> value, Func<TResult> none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`none` Func&lt;TResult&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, Func<TValue, TResult> value, Func<Task<TResult>> none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;TValue, TResult&gt;<br>

`none` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, Func<TValue, Task<TResult>> mapValue, TResult none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`mapValue` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`none` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Maybe<TValue>> maybeTask, Func<TValue, Task<TResult>> value, Func<Task<TResult>> none)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`value` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`none` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **IfSome&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Action)**

```csharp
public static Task<Maybe<TValue>> IfSome<TValue>(Task<Maybe<TValue>> maybeTask, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **IfSome&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Action&lt;TValue&gt;)**

```csharp
public static Task<Maybe<TValue>> IfSome<TValue>(Task<Maybe<TValue>> maybeTask, Action<TValue> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` Action&lt;TValue&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **IfSome&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Maybe<TValue>> IfSome<TValue>(Task<Maybe<TValue>> maybeTask, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **IfSome&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&gt;)**

```csharp
public static Task<Maybe<TValue>> IfSome<TValue>(Task<Maybe<TValue>> maybeTask, Func<TValue, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` Func&lt;TValue, Task&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **IfNone&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Action)**

```csharp
public static Task<Maybe<TValue>> IfNone<TValue>(Task<Maybe<TValue>> maybeTask, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **IfNone&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Maybe<TValue>> IfNone<TValue>(Task<Maybe<TValue>> maybeTask, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Action&lt;Maybe&lt;TValue&gt;&gt;)**

```csharp
public static Task<Maybe<TValue>> Execute<TValue>(Task<Maybe<TValue>> maybeTask, Action<Maybe<TValue>> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` Action&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Maybe&lt;TValue&gt;, Task&gt;)**

```csharp
public static Task<Maybe<TValue>> Execute<TValue>(Task<Maybe<TValue>> maybeTask, Func<Maybe<TValue>, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`action` Func&lt;Maybe&lt;TValue&gt;, Task&gt;<br>

#### Returns

Task&lt;Maybe&lt;TValue&gt;&gt;<br>

### **Satisfy&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;)**

Evaluate the `predicate` applied to the value if present.
 Return `false` in case of `None`.

```csharp
public static Task<bool> Satisfy<TValue>(Task<Maybe<TValue>> maybe, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Satisfy&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;)**

```csharp
public static Task<bool> Satisfy<TValue>(Task<Maybe<TValue>> maybe, Func<TValue, Task<bool>> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **MapToResult&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;)**

```csharp
public static Task<Result<TValue>> MapToResult<TValue>(Task<Maybe<TValue>> maybeTask)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **MapToResult&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Error&gt;)**

```csharp
public static Task<Result<TValue>> MapToResult<TValue>(Task<Maybe<TValue>> maybeTask, Func<Error> errorFunc)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`errorFunc` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **MapToResult&lt;TValue&gt;(Task&lt;Maybe&lt;TValue&gt;&gt;, Func&lt;Task&lt;Error&gt;&gt;)**

```csharp
public static Task<Result<TValue>> MapToResult<TValue>(Task<Maybe<TValue>> maybeTask, Func<Task<Error>> errorFunc)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybeTask` Task&lt;Maybe&lt;TValue&gt;&gt;<br>

`errorFunc` [Func&lt;Task&lt;Error&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>
