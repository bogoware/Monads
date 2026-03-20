---
title: ResultMapExtensions
sidebar:
  order: 99
---


Namespace: Bogoware.Monads

```csharp
public static class ResultMapExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultMapExtensions](./bogoware.monads.resultmapextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **MapToUnit&lt;TValue&gt;(Result&lt;TValue&gt;)**

```csharp
public static Result<Unit> MapToUnit<TValue>(Result<TValue> result)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

#### Returns

[Result&lt;Unit&gt;](./bogoware.monads.result-1)<br>

### **MapToUnit&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static Task<Result<Unit>> MapToUnit<TValue>(Task<Result<TValue>> result)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

[Task&lt;Result&lt;Unit&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TNewValue&gt;)**

```csharp
public static Task<Result<TNewValue>> Map<TValue, TNewValue>(Task<Result<TValue>> result, Func<TNewValue> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TNewValue&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Result<TNewValue>> Map<TValue, TNewValue>(Task<Result<TValue>> result, Func<Task<TNewValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TNewValue&gt;)**

```csharp
public static Task<Result<TNewValue>> Map<TValue, TNewValue>(Task<Result<TValue>> result, Func<TValue, TNewValue> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, TNewValue&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue, TNewValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TNewValue&gt;&gt;)**

```csharp
public static Task<Result<TNewValue>> Map<TValue, TNewValue>(Task<Result<TValue>> result, Func<TValue, Task<TNewValue>> functor)
```

#### Type Parameters

`TValue`<br>

`TNewValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action&lt;TValue&gt;)**

```csharp
public static Task<Result<Unit>> Map<TValue>(Task<Result<TValue>> resultTask, Action<TValue> functor)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`resultTask` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Action&lt;TValue&gt;<br>

#### Returns

[Task&lt;Result&lt;Unit&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Map&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&gt;)**

```csharp
public static Task<Result<Unit>> Map<TValue>(Task<Result<TValue>> resultTask, Func<TValue, Task> functor)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`resultTask` Task&lt;Result&lt;TValue&gt;&gt;<br>

`functor` Func&lt;TValue, Task&gt;<br>

#### Returns

[Task&lt;Result&lt;Unit&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
