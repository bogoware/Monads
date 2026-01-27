---
title: "resultmaperrorasyncextensions"
sidebar_position: 99
---

# ResultMapErrorAsyncExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultMapErrorAsyncExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultMapErrorAsyncExtensions](./bogoware.monads.resultmaperrorasyncextensions)<br />
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **MapError&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Error)**

```csharp
public static Task<Result<TValue>> MapError<TValue>(Task<Result<TValue>> result, Error newError)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`newError` [Error](./bogoware.monads.error)<br />

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br />

### **Map&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Error&gt;)**

```csharp
public static Task<Result<TValue>> Map<TValue>(Task<Result<TValue>> result, Func<Error> functor)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`functor` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br />

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br />

### **Map&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&lt;Error&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Map<TValue>(Task<Result<TValue>> result, Func<Task<Error>> functor)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`functor` [Func&lt;Task&lt;Error&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br />

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br />

### **Map&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Error, Error&gt;)**

```csharp
public static Task<Result<TValue>> Map<TValue>(Task<Result<TValue>> result, Func<Error, Error> functor)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`functor` [Func&lt;Error, Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br />

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br />

### **Map&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Error, Task&lt;Error&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Map<TValue>(Task<Result<TValue>> result, Func<Error, Task<Error>> functor)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`functor` [Func&lt;Error, Task&lt;Error&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br />

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br />
