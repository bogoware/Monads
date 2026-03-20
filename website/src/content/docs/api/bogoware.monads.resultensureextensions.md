---
title: ResultEnsureExtensions
sidebar:
  order: 99
---


Namespace: Bogoware.Monads

```csharp
public static class ResultEnsureExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultEnsureExtensions](./bogoware.monads.resultensureextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Ensure&lt;TValue, TError&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Boolean&gt;, Func&lt;TError&gt;)**

```csharp
public static Result<TValue> Ensure<TValue, TError>(Result<TValue> result, Func<TValue, bool> predicate, Func<TError> error)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` Func&lt;TError&gt;<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Ensure&lt;TValue, TError&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Boolean&gt;, Func&lt;Task&lt;TError&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue, TError>(Result<TValue> result, Func<TValue, bool> predicate, Func<Task<TError>> error)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` Func&lt;Task&lt;TError&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue, TError&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Func&lt;TError&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue, TError>(Result<TValue> result, Func<TValue, Task<bool>> predicate, Func<TError> error)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` Func&lt;TError&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue, TError&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Func&lt;Task&lt;TError&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue, TError>(Result<TValue> result, Func<TValue, Task<bool>> predicate, Func<Task<TError>> error)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` Func&lt;Task&lt;TError&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;, Error)**

```csharp
public static Task<Result<TValue>> Ensure<TValue>(Task<Result<TValue>> result, Func<TValue, bool> predicate, Error error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` [Error](./bogoware.monads.error)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Error)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue>(Task<Result<TValue>> result, Func<TValue, Task<bool>> predicate, Error error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` [Error](./bogoware.monads.error)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;, Func&lt;Error&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue>(Task<Result<TValue>> result, Func<TValue, bool> predicate, Func<Error> error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;, Func&lt;Task&lt;Error&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue>(Task<Result<TValue>> result, Func<TValue, bool> predicate, Func<Task<Error>> error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` [Func&lt;Task&lt;Error&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Func&lt;Error&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue>(Task<Result<TValue>> result, Func<TValue, Task<bool>> predicate, Func<Error> error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Func&lt;Task&lt;Error&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Ensure<TValue>(Task<Result<TValue>> result, Func<TValue, Task<bool>> predicate, Func<Task<Error>> error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` [Func&lt;Task&lt;Error&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>
