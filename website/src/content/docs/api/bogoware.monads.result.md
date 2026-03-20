---
title: Result
sidebar:
  order: 99
---

# Result

Namespace: Bogoware.Monads

```csharp
public static class Result
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Result](./bogoware.monads.result)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Unit**

```csharp
public static Result<Unit> Unit { get; }
```

#### Property Value

[Result&lt;Unit&gt;](./bogoware.monads.result-1)<br>

## Methods

### **Success&lt;TValue&gt;(TValue)**

Initializes a new successful instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the given `value`.

```csharp
public static Result<TValue> Success<TValue>(TValue value)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`value` TValue<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Failure&lt;TValue&gt;(String)**

Initializes a new failed instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with a [LogicError](./bogoware.monads.logicerror)
 with the message `errorMessage`.

```csharp
public static Result<TValue> Failure<TValue>(string errorMessage)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`errorMessage` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Failure&lt;TValue&gt;(Error)**

Initializes a new failed instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the given `error`.

```csharp
public static Result<TValue> Failure<TValue>(Error error)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`error` [Error](./bogoware.monads.error)<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Ensure(Boolean, Func&lt;Error&gt;)**

If the `condition` is `true` then initializes a new successful instance
 of [Result&lt;TValue&gt;](./bogoware.monads.result-1), otherwise return a failed instance of [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 with the given `error`.

```csharp
public static Result<Unit> Ensure(bool condition, Func<Error> error)
```

#### Parameters

`condition` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`error` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

[Result&lt;Unit&gt;](./bogoware.monads.result-1)<br>

### **Ensure(Func&lt;Boolean&gt;, Func&lt;Error&gt;)**

If the `predicate` evaluates to `true` then initializes a new successful instance
 of [Result&lt;TValue&gt;](./bogoware.monads.result-1), otherwise return a failed instance of [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 with the given `error`.

```csharp
public static Result<Unit> Ensure(Func<bool> predicate, Func<Error> error)
```

#### Parameters

`predicate` [Func&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

`error` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

[Result&lt;Unit&gt;](./bogoware.monads.result-1)<br>

### **Ensure(Func&lt;Task&lt;Boolean&gt;&gt;, Func&lt;Error&gt;)**

If the `predicate` evaluates to `true` then initializes a new successful instance
 of [Result&lt;TValue&gt;](./bogoware.monads.result-1), otherwise return a failed instance of [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 with the given `error`.

```csharp
public static Task<Result<Unit>> Ensure(Func<Task<bool>> predicate, Func<Error> error)
```

#### Parameters

`predicate` [Func&lt;Task&lt;Boolean&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

`error` [Func&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

[Task&lt;Result&lt;Unit&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Bind&lt;TValue&gt;(Func&lt;Result&lt;TValue&gt;&gt;)**

Initializes a new instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the value returned by `result`.

```csharp
public static Result<TValue> Bind<TValue>(Func<Result<TValue>> result)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Func&lt;Result&lt;TValue&gt;&gt;<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Bind&lt;TValue&gt;(Func&lt;Task&lt;Result&lt;TValue&gt;&gt;&gt;)**

Initializes a new instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the value returned by `result`.

```csharp
public static Task<Result<TValue>> Bind<TValue>(Func<Task<Result<TValue>>> result)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`result` Func&lt;Task&lt;Result&lt;TValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **From&lt;T&gt;(T)**

Initializes a new instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the value.

```csharp
public static Result<T> From<T>(T value)
```

#### Type Parameters

`T`<br>

#### Parameters

`value` T<br>

#### Returns

Result&lt;T&gt;<br>

### **Execute(Action)**

Wraps the execution of the given `action` in a [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 catching any thrown exception and returning it as an [RuntimeError](./bogoware.monads.runtimeerror) .

```csharp
public static Result<Unit> Execute(Action action)
```

#### Parameters

`action` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

#### Returns

[Result&lt;Unit&gt;](./bogoware.monads.result-1)<br>

### **Execute(Func&lt;Task&gt;)**

Wraps the execution of the given `action` in a [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 catching any thrown exception and returning it as an [RuntimeError](./bogoware.monads.runtimeerror) .

```csharp
public static Task<Result<Unit>> Execute(Func<Task> action)
```

#### Parameters

`action` [Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

#### Returns

[Task&lt;Result&lt;Unit&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Execute&lt;TValue&gt;(Func&lt;TValue&gt;)**

Wraps the execution of the given `function` in a [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 catching any thrown exception and returning it as an [RuntimeError](./bogoware.monads.runtimeerror) .

```csharp
public static Result<TValue> Execute<TValue>(Func<TValue> function)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`function` Func&lt;TValue&gt;<br>

#### Returns

Result&lt;TValue&gt;<br>

### **Execute&lt;TValue&gt;(Func&lt;Task&lt;TValue&gt;&gt;)**

Wraps the execution of the given `function` in a [Result&lt;TValue&gt;](./bogoware.monads.result-1)
 catching any thrown exception and returning it as an [RuntimeError](./bogoware.monads.runtimeerror) .

```csharp
public static Task<Result<TValue>> Execute<TValue>(Func<Task<TValue>> function)
```

#### Type Parameters

`TValue`<br>

#### Parameters

`function` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>
