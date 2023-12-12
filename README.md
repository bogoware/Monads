# Bogoware Monads

![Nuget](https://img.shields.io/nuget/dt/Bogoware.Monads?logo=nuget&style=plastic) ![Nuget](https://img.shields.io/nuget/v/Bogoware.Monads?style=plastic)


_Yet another functional library for C#_

## Quickstart

Install from Nuget and enjoy!

```shell
dotnet add package Bogoware.Monads
```


## Introduction to Monads

Monads are a powerful tool for modeling operations in a functional way, making them a cornerstone 
of functional programming. While we won't delve into a detailed explanation of monads and their inner 
workings in this document, there are numerous resources available online that approach the topic 
from different perspectives.

For the purpose of this introduction, we can consider monads as a type of "safe container" that encapsulates
the result of an operation. They provide methods that enable manipulation of the result in a safe manner,
ensuring that the operation executes only if it succeeds.

By employing monads, code can be protected from further processing in case of errors or missing data. 
Adopting a functional approach offers benefits such as increased readability, improved reasoning capabilities,
and more robust and error-resistant code.

## Functional Challenges in C#

C# offers good support for functional programming, but there are certain limitations that necessitate 
careful design decisions.

## Bogoware Monads

This library provides two well-known monads: `Result` and `Maybe` monads (also referred to as `Either`, 
`Optional`, `Option` in other contexts):

> The `Result<T>` monad is used to model operations that can fail.

>The `Maybe<T>` monad is used to model operations that can either return a value or be empty.

Additionally, the library provides the `Error` abstract class, which complements the `Result<T>` monad and
offers an ergonomic approach to error management at an application-wide scale.

## Design Goals for `Result<T>`

The `Result<T>` monad is designed for modeling operations that can either fail or return a value.
It is a generic type, with `T` representing the type of the value returned by the successful operation.

`Result<T>` provides a set of methods that facilitate chaining operations in a functional manner:
* `Map`: Allows transformation of the value returned by the operation, representing the "happy" flow.
  * `Map` to void functor will map to `Result<Unit>`
  * `MapToUnit()` is just a shortcut for `Map(_ => { })`
* `MapError`: Allows transformation of the error returned by the operation, representing the "unhappy" flow.
* `Bind`: Enables chaining of operations providing a fluent syntax that allows
 to capture the values on the "happy" path and use them in subsequent steps.
* `Match`: Facilitates handling of the operation's result by providing separate paths for the "happy" and "unhappy" flows.
* `RecoverWith`: Provides a way to recover from an error by returning a `Result<T>`
* `Ensure`: Allows asserting a condition on the value returned by the operation.
* `ExecuteIfSuccess`: Executes if the operation succeeds. It is typically used for side effects.
* `ExecuteIfFailure`: Executes if the operation fails. It is typically used for side effects.

There are also some unsafe methods intended to support developers who are less familiar with the functional approach
and may need to resort to a procedural style to achieve their goals.
These methods should be used sparingly, as they deviate from the functional paradigm and make the code less
robust, potentially leading to unexpected exceptions:

* `ThrowIfFailure`: Throws an exception if the operation fails. It is typically used to terminate the execution of the pipeline
  discarding the result of the operation.
* `GetValueOrThrow`: Extracts the value from the `Result<T>` monad.
* `GetErrorOrThrow`: Extracts the error from the `Result<T>` monad. 

By adhering to the `Result<T>` monad, code can be modeled in a more readable and reasoned manner.
It also contributes to writing more robust code with reduced error-proneness.

### `Result` Helper Methods

The `Result` class provides a set of helper methods that facilitate the creation of `Result<T>` instances or
make the code more readable.

* `Result.Success`: Creates a successful `Result<T>` instance with the specified value.
* `Result.Failure`: Creates a failed `Result<T>` instance with the specified error.
* `Result.Ensure`: Creates a successful `Result<Unit>` instance if the specified condition is true, otherwise creates 
a failed instance with the specified error.
* `Result.Bind`: Creates a `Result<T>` instance from a delegate. This method is particularly useful
when you need to start a chain of operations with a `Result<T>` instance and you like to have a consistent
syntax for all the steps of the chain.

For example, instead of writing:
```csharp
/// Publishes the project
public Result<Unit> Publish() {
    if (PublishingStatus == PublishingStatus.Published)
        return new InvalidOperationError("Already published");
    
    return ValidateCostComponents() // Note the explicit invocation of the method
        .Bind(ValidateTimingComponents)
        // ... more binding to validation methods
        .ExecuteIfSuccess(() => PublishingStatus = PublishingStatus.Published);
}
```

You can write:
```csharp
/// Publishes the project
public Result<Unit> Publish() => Result
    .Ensure(PublishingStatus != PublishingStatus.Published, () => new InvalidOperationError("Already published")
    .Bind(ValidateCostComponents)
    .Bind(ValidateTimingComponents)
    // ... more binding to validation methods
    .ExecuteIfSuccess(() => PublishingStatus = PublishingStatus.Published);
```

## Design Goals for `Error`

The `Error` class is used for modeling errors and works in conjunction with the `Result<T>` monad.

There are two types of errors:
* `LogicError`: These errors are caused by application logic and should be programmatically handled.
Examples include `InvalidEmailError`, `InvalidPasswordError`, `InvalidUsernameError`, etc.
* `RuntimeError`: These errors are caused by external sources and are unrelated to domain logic.
Examples include `DatabaseError`, `NetworkError`, `FileSystemError`, etc.


Distinguishing between `LogicError`s and `RuntimeError`s is important, as they require different handling approaches:
* `LogicError`s should be programmatically handled and can be safely reported to the user in case of a malformed request.
* `RuntimeError`s should be handled by the infrastructure and should not be reported to the user.

For example, in a typical ASP.NET Core application, `LogicErrors` can be handled by returning a `BadRequest`
response to the client, while `RuntimeErrors` can be handled by returning an `InternalServerError` response.

### `Error` Hierarchy Best Practices
Each application should model its own logic errors by deriving from a root class that represents the base class
for all logic errors. The root class should derive from the `LogicError` class.

For different kinds of logic errors that can occur, the application should derive specific classes,
each modeling a particular logic error and providing the necessary properties to describe the error.

In the following example, we model two logic errors: `NotFoundError` and `InvalidOperationError`:

```csharp

public abstract class ApplicationError: LogicError
{
	
	public int ErrorCode { get; }

	protected ApplicationError(string message, int errorCode)
		: base(message)
	{
		ErrorCode = errorCode;
	}
}

public class NotFoundError : ApplicationError
{
	
	public string ResourceName { get; }
	public string ResourceId { get; }
	public NotFoundError(string message, int errorCode, string resourceName, string resourceId)
		: base(message, errorCode)
	{
		ResourceName = resourceName;
		ResourceId = resourceId;
	}
}

public class InvalidOperationError : ApplicationError
{
	
	public string OperationName { get; }
	public string Reason { get; }
	public InvalidOperationError(string message, int errorCode, string operationName, string reason)
		: base(message, errorCode)
	{
		OperationName = operationName;
		Reason = reason;
	}
}
```

As demonstrated in the project [FluentValidationSample](./samples/FluentValidationSample) the `FluentValidation` library 
can be used to model validation errors.

In contrast to `LogicError`s, `RuntimeError`s are generated by the `Result.Execute()` methods to encapsulate exceptions 
thrown by the application.

## Design Goals for `Maybe<T>`

Before discussing what can be achieved with the `Maybe<T>` monad, let's clarify that it is not intended as a 
replacement for Nullable<T>.
This is mainly due to fundamental libraries, such as Entity Framework, relying on `Nullable<T>` to model class
attributes, while support for structural types remains limited.
A pragmatic approach involves using `Nullable<T>` for modeling class attributes and `Maybe<T>` for modeling
return values and method parameters.

The advantage of using `Maybe<T>` over `Nullable<T>` is that `Maybe<T>` provides a set of methods that enable
chaining operations in a functional manner.
This becomes particularly useful when dealing with operations that can either return a value or be empty,
such as querying a database.

The implicit conversion from `Nullable<T>` to `Maybe<T>` allows for lifting `Nullable<T>` values to `Maybe<T>`
values and utilizing `Maybe<T>` methods for chaining operations.

> **Practical rule**: Use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and
> method paramethers.

## Converting `Maybe<T>` to `Result<T>`

It is common to implement a pipeline of operations where an empty `Maybe<T>` instance should be interpreted as a failure,
in this case the `Maybe<T>` instance can be converted to a `Result<T>` instance by using the `ToResult` method.

The `ToResult` method accepts an error as a parameter and returns a `Result<T>` instance with the specified error
in case the `Maybe<T>` instance is empty.