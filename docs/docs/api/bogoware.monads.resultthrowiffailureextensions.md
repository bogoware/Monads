---
title: "resultthrowiffailureextensions"
sidebar_position: 99
---

# ResultThrowIfFailureExtensions

Namespace: Bogoware.Monads

```csharp
public static class ResultThrowIfFailureExtensions
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ResultThrowIfFailureExtensions](./bogoware.monads.resultthrowiffailureextensions)<br />
Attributes [ExtensionAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.extensionattribute)

## Methods

### **ThrowIfFailure&lt;TValue&gt;(Result&lt;TValue&gt;)**

Throws a [ResultFailedException](./bogoware.monads.resultfailedexception) if the [Result&lt;TValue&gt;](./bogoware.monads.result-1) is a `Failure`.

```csharp
public static void ThrowIfFailure<TValue>(Result<TValue> result)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`result` Result&lt;TValue&gt;<br />

### **ThrowIfFailure&lt;TValue&gt;(Task&lt;Result&lt;TValue&gt;&gt;)**

```csharp
public static Task ThrowIfFailure<TValue>(Task<Result<TValue>> resultTask)
```

#### Type Parameters

`TValue`<br />

#### Parameters

`resultTask` Task&lt;Result&lt;TValue&gt;&gt;<br />

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br />
