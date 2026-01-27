---
title: "resultmatchasyncextensions"
sidebar_position: 99
---

# ResultMatchAsyncExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultMatchAsyncExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultMatchAsyncExtensions](./bogoware.monads.resultmatchasyncextensions)<br />
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, Func&lt;Error, TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, TResult> successful, Func<Error, TResult> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, TResult&gt;<br />

`failure` Func&lt;Error, TResult&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;Error, TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, Func<Error, TResult> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br />

`failure` Func&lt;Error, TResult&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, TResult> successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, TResult&gt;<br />

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br />

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, TResult, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, TResult successful, TResult failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` TResult<br />

`failure` TResult<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TResult&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TResult> successful, TResult failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TResult&gt;<br />

`failure` TResult<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;Task&lt;TResult&gt;&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<Task<TResult>> successful, TResult failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;Task&lt;TResult&gt;&gt;<br />

`failure` TResult<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, TResult, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, TResult successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` TResult<br />

`failure` Func&lt;TResult&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, TResult, Func&lt;Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, TResult successful, Func<Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` TResult<br />

`failure` Func&lt;Task&lt;TResult&gt;&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TResult&gt;, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TResult> successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TResult&gt;<br />

`failure` Func&lt;TResult&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, TResult> successful, TResult failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, TResult&gt;<br />

`failure` TResult<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, TResult&gt;, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, TResult> successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, TResult&gt;<br />

`failure` Func&lt;TResult&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, TResult, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, TResult successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` TResult<br />

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TResult&gt;, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TResult> successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TResult&gt;<br />

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, TResult)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, TResult failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br />

`failure` TResult<br />

#### Returns

Task&lt;TResult&gt;<br />

### **Match&lt;TValue, TResult&gt;(Task&lt;Result&lt;TValue&gt;&gt;, Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;TResult&gt;)**

```csharp
public static Task<TResult> Match<TValue, TResult>(Task<Result<TValue>> result, Func<TValue, Task<TResult>> successful, Func<TResult> failure)
```

#### Type Parameters

`TValue`<br />

`TResult`<br />

#### Parameters

`result` Task&lt;Result&lt;TValue&gt;&gt;<br />

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br />

`failure` Func&lt;TResult&gt;<br />

#### Returns

Task&lt;TResult&gt;<br />
