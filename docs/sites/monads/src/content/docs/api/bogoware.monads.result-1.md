---
title: "Result<TValue>"
sidebar:
  order: 99
---

# Result&lt;TValue&gt;

Namespace: Bogoware.Monads

Represents the result of an operation that may fail.

```csharp
public struct Result<TValue>
```

#### Type Parameters

`TValue`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>
Implements IResult&lt;TValue&gt;, [IResult](./bogoware.monads.iresult), IEquatable&lt;Result&lt;TValue&gt;&gt;, IEnumerable&lt;TValue&gt;, [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>
Attributes [NullableContextAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute), [IsReadOnlyAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.isreadonlyattribute)

## Properties

### **Value**

Returns the value if the [Result&lt;TValue&gt;](./bogoware.monads.result-1).[Result&lt;TValue&gt;.IsSuccess](./bogoware.monads.result-1#issuccess)
 otherwise throw an [ResultFailedException](./bogoware.monads.resultfailedexception).
 This method should be avoided in favor of pure functional composition style.

```csharp
public TValue Value { get; }
```

#### Property Value

TValue<br>

#### Exceptions

[ResultFailedException](./bogoware.monads.resultfailedexception)<br>

### **Error**

Returns the error if the [Result&lt;TValue&gt;](./bogoware.monads.result-1).[Result&lt;TValue&gt;.IsFailure](./bogoware.monads.result-1#isfailure)
 otherwise throw an [ResultSuccessException](./bogoware.monads.resultsuccessexception).
 This method should be avoided in favor of pure functional composition style.

```csharp
public Error Error { get; }
```

#### Property Value

[Error](./bogoware.monads.error)<br>

#### Exceptions

[ResultFailedException](./bogoware.monads.resultfailedexception)<br>

### **IsSuccess**

Is `true` if the [Result&lt;TValue&gt;](./bogoware.monads.result-1) is successful, otherwise `false`.

```csharp
public bool IsSuccess { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsFailure**

Is `true` if the [Result&lt;TValue&gt;](./bogoware.monads.result-1) is failed, otherwise `false`.

```csharp
public bool IsFailure { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **Result(TValue)**

Initializes a successful instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the given `value`.

```csharp
Result(TValue value)
```

#### Parameters

`value` TValue<br>

### **Result(Error)**

Initializes a failed instance of the [Result&lt;TValue&gt;](./bogoware.monads.result-1) with the given `error`.

```csharp
Result(Error error)
```

#### Parameters

`error` [Error](./bogoware.monads.error)<br>

### **Result(Result&lt;TValue&gt;)**

```csharp
Result(Result<TValue> result)
```

#### Parameters

`result` [Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

## Methods

### **GetValueOrThrow()**

```csharp
TValue GetValueOrThrow()
```

#### Returns

TValue<br>

### **GetErrorOrThrow()**

```csharp
Error GetErrorOrThrow()
```

#### Returns

[Error](./bogoware.monads.error)<br>

### **Map&lt;TNewValue&gt;(TNewValue)**

In case of success returns the `newValue`..

```csharp
Result<TNewValue> Map<TNewValue>(TNewValue newValue)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`newValue` TNewValue<br>

#### Returns

Result&lt;TNewValue&gt;<br>

### **Map&lt;TNewValue&gt;(Func&lt;TNewValue&gt;)**

In case of success returns the `functor` result.

```csharp
Result<TNewValue> Map<TNewValue>(Func<TNewValue> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;TNewValue&gt;<br>

#### Returns

Result&lt;TNewValue&gt;<br>

### **Map&lt;TNewValue&gt;(Func&lt;Task&lt;TNewValue&gt;&gt;)**

```csharp
Task<Result<TNewValue>> Map<TNewValue>(Func<Task<TNewValue>> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Map&lt;TNewValue&gt;(Func&lt;TValue, TNewValue&gt;)**

In case of success transform the original value by applying the `functor`.

```csharp
Result<TNewValue> Map<TNewValue>(Func<TValue, TNewValue> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;TValue, TNewValue&gt;<br>

#### Returns

Result&lt;TNewValue&gt;<br>

### **Map&lt;TNewValue&gt;(Func&lt;TValue, Task&lt;TNewValue&gt;&gt;)**

```csharp
Task<Result<TNewValue>> Map<TNewValue>(Func<TValue, Task<TNewValue>> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;TValue, Task&lt;TNewValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Map(Action&lt;TValue&gt;)**

```csharp
Result<Unit> Map(Action<TValue> functor)
```

#### Parameters

`functor` Action&lt;TValue&gt;<br>

#### Returns

[Result&lt;Unit&gt;](./bogoware.monads.result-1)<br>

### **Map(Func&lt;TValue, Task&gt;)**

```csharp
Task<Result<Unit>> Map(Func<TValue, Task> functor)
```

#### Parameters

`functor` Func&lt;TValue, Task&gt;<br>

#### Returns

[Task&lt;Result&lt;Unit&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **MapError(Error)**

In case of failure return the `newError`.

```csharp
Result<TValue> MapError(Error newError)
```

#### Parameters

`newError` [Error](./bogoware.monads.error)<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **MapError&lt;TNewError&gt;(Func&lt;TNewError&gt;)**

In case of failure return the `newErrorFunctor` result.

```csharp
Result<TValue> MapError<TNewError>(Func<TNewError> newErrorFunctor)
```

#### Type Parameters

`TNewError`<br>

#### Parameters

`newErrorFunctor` Func&lt;TNewError&gt;<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **MapError&lt;TNewError&gt;(Func&lt;Error, TNewError&gt;)**

```csharp
Result<TValue> MapError<TNewError>(Func<Error, TNewError> newErrorFunctor)
```

#### Type Parameters

`TNewError`<br>

#### Parameters

`newErrorFunctor` Func&lt;Error, TNewError&gt;<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **MapError&lt;TNewError&gt;(Func&lt;Task&lt;TNewError&gt;&gt;)**

```csharp
Task<Result<TValue>> MapError<TNewError>(Func<Task<TNewError>> newErrorFunctor)
```

#### Type Parameters

`TNewError`<br>

#### Parameters

`newErrorFunctor` Func&lt;Task&lt;TNewError&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **MapError&lt;TNewError&gt;(Func&lt;Error, Task&lt;TNewError&gt;&gt;)**

```csharp
Task<Result<TValue>> MapError<TNewError>(Func<Error, Task<TNewError>> newErrorFunctor)
```

#### Type Parameters

`TNewError`<br>

#### Parameters

`newErrorFunctor` Func&lt;Error, Task&lt;TNewError&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Bind&lt;TNewValue&gt;(Result&lt;TNewValue&gt;)**

In case of success return the `newResult`.

```csharp
Result<TNewValue> Bind<TNewValue>(Result<TNewValue> newResult)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`newResult` Result&lt;TNewValue&gt;<br>

#### Returns

Result&lt;TNewValue&gt;<br>

### **Bind&lt;TNewValue&gt;(Func&lt;Result&lt;TNewValue&gt;&gt;)**

In case of success return the `functor` result.

```csharp
Result<TNewValue> Bind<TNewValue>(Func<Result<TNewValue>> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;Result&lt;TNewValue&gt;&gt;<br>

#### Returns

Result&lt;TNewValue&gt;<br>

### **Bind&lt;TNewValue&gt;(Func&lt;Task&lt;Result&lt;TNewValue&gt;&gt;&gt;)**

```csharp
Task<Result<TNewValue>> Bind<TNewValue>(Func<Task<Result<TNewValue>>> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;Task&lt;Result&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Bind&lt;TNewValue&gt;(Func&lt;TValue, Result&lt;TNewValue&gt;&gt;)**

```csharp
Result<TNewValue> Bind<TNewValue>(Func<TValue, Result<TNewValue>> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;TValue, Result&lt;TNewValue&gt;&gt;<br>

#### Returns

Result&lt;TNewValue&gt;<br>

### **Bind&lt;TNewValue&gt;(Func&lt;TValue, Task&lt;Result&lt;TNewValue&gt;&gt;&gt;)**

```csharp
Task<Result<TNewValue>> Bind<TNewValue>(Func<TValue, Task<Result<TNewValue>>> functor)
```

#### Type Parameters

`TNewValue`<br>

#### Parameters

`functor` Func&lt;TValue, Task&lt;Result&lt;TNewValue&gt;&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TNewValue&gt;&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, TResult&gt;, TResult)**

In case of success evaluate the `successful` functor, otherwise returns `failure`.

```csharp
TResult Match<TResult>(Func<TValue, TResult> successful, TResult failure)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`successful` Func&lt;TValue, TResult&gt;<br>

`failure` TResult<br>

#### Returns

TResult<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, Task&lt;TResult&gt;&gt;, TResult)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, TResult failure)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`failure` TResult<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, TResult&gt;, Func&lt;Error, TResult&gt;)**

```csharp
TResult Match<TResult>(Func<TValue, TResult> successful, Func<Error, TResult> failure)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`successful` Func&lt;TValue, TResult&gt;<br>

`failure` Func&lt;Error, TResult&gt;<br>

#### Returns

TResult<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;Error, TResult&gt;)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, TResult> failure)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`failure` Func&lt;Error, TResult&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, TResult&gt;, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, TResult> successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`successful` Func&lt;TValue, TResult&gt;<br>

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **Match&lt;TResult&gt;(Func&lt;TValue, Task&lt;TResult&gt;&gt;, Func&lt;Error, Task&lt;TResult&gt;&gt;)**

```csharp
Task<TResult> Match<TResult>(Func<TValue, Task<TResult>> successful, Func<Error, Task<TResult>> failure)
```

#### Type Parameters

`TResult`<br>

#### Parameters

`successful` Func&lt;TValue, Task&lt;TResult&gt;&gt;<br>

`failure` Func&lt;Error, Task&lt;TResult&gt;&gt;<br>

#### Returns

Task&lt;TResult&gt;<br>

### **RecoverWith(TValue)**

```csharp
Result<TValue> RecoverWith(TValue newValue)
```

#### Parameters

`newValue` TValue<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **RecoverWith(Func&lt;TValue&gt;)**

```csharp
Result<TValue> RecoverWith(Func<TValue> functor)
```

#### Parameters

`functor` Func&lt;TValue&gt;<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **RecoverWith(Func&lt;Error, TValue&gt;)**

```csharp
Result<TValue> RecoverWith(Func<Error, TValue> functor)
```

#### Parameters

`functor` Func&lt;Error, TValue&gt;<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **RecoverWith(Func&lt;Task&lt;TValue&gt;&gt;)**

```csharp
Task<Result<TValue>> RecoverWith(Func<Task<TValue>> functor)
```

#### Parameters

`functor` Func&lt;Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **RecoverWith(Func&lt;Error, Task&lt;TValue&gt;&gt;)**

```csharp
Task<Result<TValue>> RecoverWith(Func<Error, Task<TValue>> functor)
```

#### Parameters

`functor` Func&lt;Error, Task&lt;TValue&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure(Func&lt;TValue, Boolean&gt;, Error)**

If the [Result&lt;TValue&gt;](./bogoware.monads.result-1).[Result&lt;TValue&gt;.IsSuccess](./bogoware.monads.result-1#issuccess) is true then evaluate the `predicate`
 and return the [Result&lt;TValue&gt;](./bogoware.monads.result-1) if the predicate is true, otherwise return
 a new [Result&lt;TValue&gt;](./bogoware.monads.result-1) provided by `error`.

```csharp
Result<TValue> Ensure(Func<TValue, bool> predicate, Error error)
```

#### Parameters

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` [Error](./bogoware.monads.error)<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **Ensure(Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Error)**

```csharp
Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Error error)
```

#### Parameters

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` [Error](./bogoware.monads.error)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure(Func&lt;TValue, Boolean&gt;, Func&lt;TValue, Error&gt;)**

```csharp
Result<TValue> Ensure(Func<TValue, bool> predicate, Func<TValue, Error> error)
```

#### Parameters

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` Func&lt;TValue, Error&gt;<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **Ensure(Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Func&lt;TValue, Error&gt;)**

```csharp
Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Error> error)
```

#### Parameters

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` Func&lt;TValue, Error&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure(Func&lt;TValue, Boolean&gt;, Func&lt;TValue, Task&lt;Error&gt;&gt;)**

```csharp
Task<Result<TValue>> Ensure(Func<TValue, bool> predicate, Func<TValue, Task<Error>> error)
```

#### Parameters

`predicate` Func&lt;TValue, Boolean&gt;<br>

`error` Func&lt;TValue, Task&lt;Error&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Ensure(Func&lt;TValue, Task&lt;Boolean&gt;&gt;, Func&lt;TValue, Task&lt;Error&gt;&gt;)**

```csharp
Task<Result<TValue>> Ensure(Func<TValue, Task<bool>> predicate, Func<TValue, Task<Error>> error)
```

#### Parameters

`predicate` Func&lt;TValue, Task&lt;Boolean&gt;&gt;<br>

`error` Func&lt;TValue, Task&lt;Error&gt;&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfSuccess(Action&lt;TValue&gt;)**

Execute the action if the [Result&lt;TValue&gt;](./bogoware.monads.result-1).[Result&lt;TValue&gt;.IsSuccess](./bogoware.monads.result-1#issuccess) is true.

```csharp
Result<TValue> IfSuccess(Action<TValue> action)
```

#### Parameters

`action` Action&lt;TValue&gt;<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **IfSuccess(Func&lt;TValue, Task&gt;)**

```csharp
Task<Result<TValue>> IfSuccess(Func<TValue, Task> action)
```

#### Parameters

`action` Func&lt;TValue, Task&gt;<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **IfFailure(Action&lt;Error&gt;)**

Execute the action if the [Result&lt;TValue&gt;](./bogoware.monads.result-1).[Result&lt;TValue&gt;.IsFailure](./bogoware.monads.result-1#isfailure) is true.

```csharp
Result<TValue> IfFailure(Action<Error> action)
```

#### Parameters

`action` [Action&lt;Error&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-1)<br>

#### Returns

[Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

### **IfFailure(Func&lt;Error, Task&gt;)**

```csharp
Task<Result<TValue>> IfFailure(Func<Error, Task> action)
```

#### Parameters

`action` [Func&lt;Error, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

#### Returns

Task&lt;Result&lt;TValue&gt;&gt;<br>

### **Equals(Result&lt;TValue&gt;)**

```csharp
bool Equals(Result<TValue> other)
```

#### Parameters

`other` [Result&lt;TValue&gt;](./bogoware.monads.result-1)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetEnumerator()**

```csharp
IEnumerator<TValue> GetEnumerator()
```

#### Returns

IEnumerator&lt;TValue&gt;<br>

### **Equals(Object)**

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
