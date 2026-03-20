---
title: ResultSatisfyExtensions
sidebar:
  order: 99
---

# ResultSatisfyExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultSatisfyExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultSatisfyExtensions](./bogoware.monads.resultsatisfyextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Satisfy&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Boolean&gt;)**

Evaluate the `predicate` applied to the value if present.
 Return `false` in case of `None`.

```csharp
public static bool Satisfy<TValue>(Result<TValue> result, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Satisfy&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;)**

```csharp
public static Task<bool> Satisfy<TValue>(Result<TValue> result, Func<TValue, Task<bool>> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Satisfy&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Boolean&gt;)**

```csharp
public static Task<bool> Satisfy<TValue>(Task<Result<TValue>> result, Func<TValue, bool> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Boolean&gt;<br>

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Satisfy&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;Boolean&gt;&gt;)**

```csharp
public static Task<bool> Satisfy<TValue>(Task<Result<TValue>> maybe, Func<TValue, Task<bool>> predicate)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`maybe` Task&lt;Result&lt;TValue&gt;&gt;<br>

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

#### Returns

[Task&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
