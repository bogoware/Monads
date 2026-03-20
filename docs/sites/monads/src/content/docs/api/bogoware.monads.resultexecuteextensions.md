---
title: ResultExecuteExtensions
sidebar:
  order: 99
---

# ResultExecuteExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultExecuteExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultExecuteExtensions](./bogoware.monads.resultexecuteextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **IfSuccess&lt;TValue&gt;(Result&lt;TValue&gt;, Action)**

```csharp
public static Result<TValue> IfSuccess<TValue>(Result<TValue> result, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Result&lt;TValue&gt;<br>

### **IfSuccess&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Result<TValue>> IfSuccess<TValue>(Result<TValue> result, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfFailure&lt;TValue&gt;(Result&lt;TValue&gt;, Action)**

```csharp
public static Result<TValue> IfFailure<TValue>(Result<TValue> result, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Result&lt;TValue&gt;<br>

### **IfFailure&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Result<TValue>> IfFailure<TValue>(Result<TValue> result, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Result&lt;TValue&gt;, Action)**

```csharp
public static Result<TValue> Execute<TValue>(Result<TValue> result, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Execute&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Result<TValue>> Execute<TValue>(Result<TValue> result, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Result&lt;TValue&gt;, Action&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static Result<TValue> Execute<TValue>(Result<TValue> result, Action<Result<TValue>> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` Action&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Execute&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;Result&lt;TValue&gt;, Task&gt;)**

```csharp
public static Task<Result<TValue>> Execute<TValue>(Result<TValue> result, Func<Result<TValue>, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`action` Func&lt;Result&lt;TValue&gt;, Task&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfSuccess&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action&lt;TValue&gt;)**

```csharp
public static Task<Result<TValue>> IfSuccess<TValue>(Task<Result<TValue>> result, Action<TValue> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` Action&lt;TValue&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfSuccess&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&gt;)**

```csharp
public static Task<Result<TValue>> IfSuccess<TValue>(Task<Result<TValue>> result, Func<TValue, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` Func&lt;TValue, Task&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfSuccess&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action)**

```csharp
public static Task<Result<TValue>> IfSuccess<TValue>(Task<Result<TValue>> result, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfSuccess&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Result<TValue>> IfSuccess<TValue>(Task<Result<TValue>> result, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfFailure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action&lt;Error&gt;)**

```csharp
public static Task<Result<TValue>> IfFailure<TValue>(Task<Result<TValue>> result, Action<Error> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Action&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfFailure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Error, Task&gt;)**

```csharp
public static Task<Result<TValue>> IfFailure<TValue>(Task<Result<TValue>> result, Func<Error, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Func&lt;Error, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfFailure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action)**

```csharp
public static Task<Result<TValue>> IfFailure<TValue>(Task<Result<TValue>> result, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfFailure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Result<TValue>> IfFailure<TValue>(Task<Result<TValue>> result, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action)**

```csharp
public static Task<Result<TValue>> Execute<TValue>(Task<Result<TValue>> result, Action action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&gt;)**

```csharp
public static Task<Result<TValue>> Execute<TValue>(Task<Result<TValue>> result, Func<Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Action&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static Task<Result<TValue>> Execute<TValue>(Task<Result<TValue>> resultTask, Action<Result<TValue>> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`resultTask` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` Action&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Execute&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Result&lt;TValue&gt;, Task&gt;)**

```csharp
public static Task<Result<TValue>> Execute<TValue>(Task<Result<TValue>> resultTask, Func<Result<TValue>, Task> action)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`resultTask` Task&lt;Result&lt;TValue&gt;&gt;<br>

`action` Func&lt;Result&lt;TValue&gt;, Task&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>
