# Bogoware Monads

_Yet another functional library for C#_

## Monads: quick introduction

Monads are a powerful tool to model operations in a functional way and it's not a coincidence that they are the cornerstone of functional programming.
It's not our mission to explain what monads are and how they work, there are plenty of
resources on the web that do that.

For the objective of this library they can be considered as a sort of _container_ that
encapsulates the result of an operation and provides a set of methods that allow to chain
tasks in a safe way, warranting that the operation will be executed only if it is fine,
shielding from attempting to perform further processing in case of failure or missing data.

The benefit of adopting a functional approach is that it allows to model operations in a way
that is more readable and easier to reason about, moreover it allows to write code that is more
robust and less prone to errors.

## Bogoware Monads

This library provides the `Result` and `Maybe` monads to support functional programming in C#. 
These monads provide te means to model pipelines of operations in a robust, coincise and readable way.

> The `Result<T, E>` monad is used to model operations that can fail.

>The `Maybe<T>` monad is used to model operations that can return a value or not.

## `Result<T, E>` Design Goals

The `Result<T, E>` monad is used to model operations that can fail or return a value. The `Result<T, E>` monad is a generic type that has two type parameters, `T` and `E`, where `T` is the type of the value returned by the operation  uppon success and `E` is the type of the error returned in case of failure.

`Result<T, E>` provides a set of methods that allow to chain operations in a functional way:
* `Map` allows to transform the value returned by the operation, thus modelliing the _happy_ flow
* `Bind` allows to chain operations that return a `Result<T, E>`.
* `Match` allows to handle the result of the operation.
* `RecoverWith` allows to recover from an error by returning a `Result<T, E>`
* `Ensure` allow to assert a condition on the value returned by the operation
* `ExecuteIfSuccess` allows to execute an action if the operation succeeds
* `ExecuteIfFailure` allows to execute an action if the operation fails
 
There are also some unsafe methods intended to switch to the procedural way. They are intended to support developers that aren't familiar with the functional approach and may need
to switch to the procedural way to get things done.

These methods should be avoided as much as possible because they break the functional approach
and make the code less robust and exposed to unwanted exceptions:

* `GetValueOrThrow` allows to extract the value from the `Result<T, E>` monad.
* `GetErrorOrThrow` allows to extract the error from the `Result<T, E>` monad. 

The benefit of sticking to the `Result<T, E>` monad is that it allows to model operations in a way that is more readable and easier to reason about, moreover it allows to write code that is more robust and less prone to errors.

## `Error` Design Goals

The `Error` class is used to model errors and work inconjunction with the `Result<T, E>` monad.

Every application should model its own errors by deriving from the `Error` class.

There are two types of errors:
* `LogicError`s: these errors are caused by the application logic and should be handled programmatically. For example: `InvalidEmailError`, `InvalidPasswordError`, `InvalidUsernameError`, etc.
* `RuntimeError`s: these errors are caused by external sources and are not recoverable by domain logic. For example: `DatabaseError`, `NetworkError`, `FileSystemError`, etc.

Distinguishing between `LogicError`s and `RuntimeError`s is important because it allows to handle them differently.
* `LogicError`s should be handled programmatically and can be safely reported to the user in case of malformed request
* while `RuntimeError`s should be handled by the infrastructure and aren't meant to be reported to the user.

## `Maybe<T>` Design Goals

Before stating what is intended to be achieved with the `Maybe` monad, let's clarify that it's not intended to be used as a replacement for `Nullable<T>` essentailly because some fundamental libraries, such as Entity Framework, rely on `Nullable<T>` to model class attributes and the support to structural types is still limited. A more pragmatic approach is to use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and or method paramethers. 

The benefit of using `Maybe` over `Nullable<T>` is that `Maybe` provides a set of methods that allow to chain operations in a functional way. This becomes very useful when dealing with operations that can return a value or not, like when querying a database.

The presence of an implicit conversion from `Nullable<T>` to `Maybe<T>` allows to lift up `Nullable<T>` values to `Maybe<T>` values and use the `Maybe<T>` methods to chain operations.

> **Practical rule**: use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and or method paramethers.
.
