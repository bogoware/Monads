---
title: "ResultBindAsyncExtensions"
sidebar_position: 99
---

# ResultBindAsyncExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultBindAsyncExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultBindAsyncExtensions](./bogoware.monads.resultbindasyncextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Result&lt;TNewValue&gt;)**

```csharp
public static Task<Result<TNewValue>> Bind<TValue, TNewValue>(Task<Result<TValue>> result, Result<TNewValue> newValue)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`newValue` Result&lt;TNewValue&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Result&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Result<TNewValue>> Bind<TValue, TNewValue>(Task<Result<TValue>> result, Func<Result<TNewValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;Result&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&lt;Result&lt;TNewValue&gt;&gt;&gt;)**

```csharp
public static Task<Result<TNewValue>> Bind<TValue, TNewValue>(Task<Result<TValue>> result, Func<Task<Result<TNewValue>>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;Task&lt;Result&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Result&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Result<TNewValue>> Bind<TValue, TNewValue>(Task<Result<TValue>> result, Func<TValue, Result<TNewValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, Result&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Result&lt;TNewValue&gt;&gt;&gt;)**

```csharp
public static Task<Result<TNewValue>> Bind<TValue, TNewValue>(Task<Result<TValue>> result, Func<TValue, Task<Result<TNewValue>>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, Task&lt;Result&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>
