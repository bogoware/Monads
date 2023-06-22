# Bogoware Monads

_Yet another functional library for C#_

## Monads: quick introduction

Monads are a powerful tool to model operations in a functional way and it's not 
a coincidence that they are the cornerstone of functional programming.
It's not our mission to explain what monads are and how they works:, there are plenty of
resources on the web that face the question from different persepectives.

For the objective of this introduction let's say taht they can be considered 
as a sort of _safe container_ that encapsulate the result of an operation and 
provides method methods that allow to manipulate the result in a safe way, 
ensuring that the operation will be executed only if it is fine.

This contract is enough to shield code from performing any further processing in case
of errors or missing data.

The benefit of adopting a functional approach is that it allows to model operations in a way
that is more readable and easier to reason about, moreover it allows to write code that is more
robust and less prone to errors.

## C# functional challenges

C# has a good support to functional programming but there are some limitations that
imply challenging design descisions.

## Bogoware Monads

This library provides the well knows `Result` and `Maybe` monads (also known as `Either`, `Optional`, `Option` in
other contexts):

> The `Result<T>` monad is used to model operations that can fail.

>The `Maybe<T>` monad is used to model operations that can return a value or not.

Moreover the library provides the `Error` abstract class that complements the `Result<T, E>` monad to
provide an ergonimic approach to error management at application-wide scale.

## `Result<T>` design goals

The `Result<T>` monad is used to model operations that can fail or return a value.
The `Result<T>` monad is a generic type where `T` is the type of the value returned by the operation  uppon success.

`Result<T>` provides a set of methods that allow to chain operations in a functional way:
* `Map` allows to transform the value returned by the operation, thus modelliing the _happy_ flow
* `Bind` allows to chain operations that return a `Result<T>`.
* `Match` allows to handle the result of the operation.
* `RecoverWith` allows to recover from an error by returning a `Result<T>`
* `Ensure` allow to assert a condition on the value returned by the operation
* `ExecuteIfSuccess` allows to execute an action if the operation succeeds
* `ExecuteIfFailure` allows to execute an action if the operation fails
 
There are also some unsafe methods intended to switch to the procedural way.
They are intended to support developers that aren't familiar with the functional approach and may need
to switch to the procedural way to get things done.

These methods should be avoided as much as possible because they break the functional approach
and make the code less robust and exposed to unwanted exceptions:

* `GetValueOrThrow` allows to extract the value from the `Result<T>` monad.
* `GetErrorOrThrow` allows to extract the error from the `Result<T>` monad. 

The benefit of sticking to the `Result<T>` monad is that it allows to model operations in a way that is more 
readable and easier to reason about, moreover it allows to write code that is more robust and less prone to errors.

## `Error` design goals

The `Error` class is used to model errors and work inconjunction with the `Result<T>` monad.

There are two types of errors:
* `LogicError`s: these errors are caused by the application logic and should be handled programmatically. For example: `InvalidEmailError`, `InvalidPasswordError`, `InvalidUsernameError`, etc.
* `RuntimeError`s: these errors are caused by external sources and are not related to domain logic. For example: `DatabaseError`, `NetworkError`, `FileSystemError`, etc.

Distinguishing between `LogicError`s and `RuntimeError`s is important because it allows to handle them differently.
* `LogicError`s should be handled programmatically and can be safely reported to the user in case of malformed request
* while `RuntimeError`s should be handled by the infrastructure and aren't meant to be reported to the user.

A typical Asp.Net Core application should handle `LogicError`s by returning a `BadRequest` response to the client
and `RuntimeError`s by returning an `InternalServerError` response to the client for example.

### `Error` hierarchy: best practices
Every application should model its own logic errors by deriving from the `LogicError` class a root class 
that represents the base class for all logic errors.

From this root class the application should derive a class for the different kinds of logic errors that can occur.
Each class should model a specific logic error and provide the necessary properties to describe the error.

In the following example we model two logic errors: `NotFoundError` and `InvalidOperationError`:

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

Another candidate for a logic error is the `ValidationError` class that can be used to model validation errors.
If you are using the `FluentValidation` library you can use the `ValidationFailure` class to model validation errors.

In contrast to `LogicError`s, `RuntimeError`s are generated by the `Try()` methods to encapsulate exceptions 
thrown by the application.

## `Maybe<T>` design goals

Before stating what is intended to be achieved with the `Maybe` monad, let's clarify that it's not intended to be used as a replacement for `Nullable<T>` essentailly because some fundamental libraries, such as Entity Framework, rely on `Nullable<T>` to model class attributes and the support to structural types is still limited. A more pragmatic approach is to use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and or method paramethers. 

The benefit of using `Maybe` over `Nullable<T>` is that `Maybe` provides a set of methods that allow to chain operations in a functional way. This becomes very useful when dealing with operations that can return a value or not, like when querying a database.

The presence of an implicit conversion from `Nullable<T>` to `Maybe<T>` allows to lift up `Nullable<T>` values to `Maybe<T>` values and use the `Maybe<T>` methods to chain operations.

> **Practical rule**: use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and or method paramethers.
.
