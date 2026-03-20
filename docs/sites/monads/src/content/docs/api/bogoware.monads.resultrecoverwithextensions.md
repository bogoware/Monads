---
title: ResultRecoverWithExtensions
sidebar:
  order: 99
---

# ResultRecoverWithExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultRecoverWithExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultRecoverWithExtensions](./bogoware.monads.resultrecoverwithextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **RecoverWith&lt;TValue, TError&gt;(Result&lt;TValue&gt;, TValue)**

```csharp
public static Result<TValue> RecoverWith<TValue, TError>(Result<TValue> result, TValue newValue)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`newValue` TValue<br>

#### Returns

Result&lt;TValue&gt;<br>

### **RecoverWith&lt;TValue, TError&gt;(Result&lt;TValue&gt;, Func&lt;TValue&gt;)**

```csharp
public static Result<TValue> RecoverWith<TValue, TError>(Result<TValue> result, Func<TValue> functor)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`functor` Func&lt;TValue&gt;<br>

#### Returns

Result&lt;TValue&gt;<br>

### **RecoverWith&lt;TValue, TError&gt;(Result&lt;TValue&gt;, Func&lt;Task&lt;TValue&gt;&gt;)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue, TError>(Result<TValue> result, Func<Task<TValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TError`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`functor` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Error, TValue&gt;)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue>(Task<Result<TValue>> result, Func<Error, TValue> functor)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;Error, TValue&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Error, Task&lt;TValue&gt;&gt;)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue>(Task<Result<TValue>> result, Func<Error, Task<TValue>> functor)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;Error, Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, TValue)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue>(Task<Result<TValue>> result, TValue newValue)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`newValue` TValue<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue&gt;)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue>(Task<Result<TValue>> result, Func<TValue> functor)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&lt;TValue&gt;&gt;)**

```csharp
public static Task<Result<TValue>> RecoverWith<TValue>(Task<Result<TValue>> result, Func<Task<TValue>> functor)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>
