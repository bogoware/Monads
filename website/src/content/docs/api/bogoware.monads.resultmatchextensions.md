---
title: ResultMatchExtensions
sidebar:
  order: 99
---


Namespace: Bogoware.Monads

```csharp
public static class ResultMatchExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultMatchExtensions](./bogoware.monads.resultmatchextensions)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, TResult, TResult)**

```csharp
public static TResult Match<TValue, TResult>(Result<TValue> result, TResult successful, TResult failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` TResult<br>

`failure` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TResult&gt;, TResult)**

```csharp
public static TResult Match<TValue, TResult>(Result<TValue> result, Func<TResult> successful, TResult failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TResult&gt;<br>

`failure` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;Task&lt;TResult&gt;&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, Func<Task<TResult>> successful, TResult failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;Task&lt;TResult&gt;&gt;<br>

`failure` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, TResult, Func&lt;TResult&gt;)**

```csharp
public static TResult Match<TValue, TResult>(Result<TValue> result, TResult successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` TResult<br>

`failure` Func&lt;TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, TResult, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, TResult successful, Func<Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` TResult<br>

`failure` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TResult&gt;, Func&lt;TResult&gt;)**

```csharp
public static TResult Match<TValue, TResult>(Result<TValue> result, Func<TResult> successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TResult&gt;<br>

`failure` Func&lt;TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TValue, TResult&gt;, TResult)**

```csharp
public static TResult Match<TValue, TResult>(Result<TValue> result, Func<TValue, TResult> successful, TResult failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TValue, TResult&gt;<br>

`failure` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TValue, TResult&gt;, Func&lt;TResult&gt;)**

```csharp
public static TResult Match<TValue, TResult>(Result<TValue> result, Func<TValue, TResult> successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TValue, TResult&gt;<br>

`failure` Func&lt;TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TValue, TResult&gt;, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, Func<TValue, TResult> successful, Func<Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TValue, TResult&gt;<br>

`failure` Func&lt;Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, TResult, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, TResult successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` TResult<br>

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TResult&gt;, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, Func<TResult> successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TResult&gt;<br>

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, Func<TValue, Task<TResult>> successful, TResult failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`failure` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TValue, TResult&gt;(Result&lt;TValue&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Result<TValue> result, Func<TValue, Task<TResult>> successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br>

`TResult`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`failure` Func&lt;TResult&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **GetValue&lt;TValue&gt;(Result&lt;TValue&gt;, TValue)**

Retrieve the value if [Result&lt;TValue&gt;.IsSuccess](./bogoware.monads.result-1#issuccess) or return the `recoverValue` if [Result&lt;TValue&gt;.IsFailure](./bogoware.monads.result-1#isfailure).

```csharp
public static TValue GetValue<TValue>(Result<TValue> result, TValue recoverValue)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`recoverValue` TValue<br>

#### Returns

TValue<br>

### **GetValue&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;TValue&gt;)**

```csharp
public static TValue GetValue<TValue>(Result<TValue> result, Func<TValue> recoverValue)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`recoverValue` Func&lt;TValue&gt;<br>

#### Returns

TValue<br>

### **GetValue&lt;TValue&gt;(Result&lt;TValue&gt;, Func&lt;Task&lt;TValue&gt;&gt;)**

```csharp
public static Task<TValue> GetValue<TValue>(Result<TValue> result, Func<Task<TValue>> recoverValue)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Result&lt;TValue&gt;<br>

`recoverValue` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;TValue&gt;<br>
