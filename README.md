# Bogoware Monads

![Nuget](https://img.shields.io/nuget/dt/Bogoware.Monads?logo=nuget&style=plastic) ![Nuget](https://img.shields.io/nuget/v/Bogoware.Monads?style=plastic)

_A functional programming library for C# providing `Result<T>` and `Maybe<T>` monads_

**Supported Platforms:** .NET Standard 2.1 | .NET 8 | .NET 9 | .NET 10

[CHANGELOG](./CHANGELOG.md) | [NuGet Package](https://www.nuget.org/packages/Bogoware.Monads)

## Table of Contents

- [Getting Started](#getting-started)
- [Introduction to Monads](#introduction-to-monads)
- [Library Overview](#library-overview)
- [Result&lt;T&gt; Monad](#resultt-monad)
  - [Design Goals](#design-goals-for-resultt)
  - [Complete API Reference](#complete-resultt-api-reference)
  - [Static Helper Methods](#result-static-helper-methods)
- [Maybe&lt;T&gt; Monad](#maybet-monad)
  - [Design Goals](#design-goals-for-maybet)
  - [Complete API Reference](#complete-maybet-api-reference)
  - [Converting to Result&lt;T&gt;](#converting-maybet-to-resultt)
- [Working with Collections](#working-with-collections)
  - [IEnumerable&lt;Maybe&lt;T&gt;&gt; Extensions](#manipulating-ienumerablemaybet)
  - [IEnumerable&lt;Result&lt;T&gt;&gt; Extensions](#manipulating-ienumerableresultt)
- [Error Types and Management](#error-types-and-management)
  - [Built-in Error Types](#built-in-error-types)
  - [Error Hierarchy Best Practices](#error-hierarchy-best-practices)
- [Async Programming with Monads](#async-programming-with-monads)
- [Advanced Patterns and Best Practices](#advanced-patterns-and-best-practices)

## Getting Started

Install from NuGet and start using the monads in your C# projects:

```shell
dotnet add package Bogoware.Monads
```

### Your First Maybe Example

Let's start with a simple example using `Maybe<T>` to handle optional values safely:

```csharp
using Bogoware.Monads;

// Traditional approach with null checks
public string GetFullName(string firstName, string? lastName)
{
    if (lastName != null)
        return $"{firstName} {lastName}";
    return firstName;
}

// Using Maybe<T> for safer optional handling
public record Person(string FirstName, Maybe<string> LastName);

public string GetFullNameSafe(Person person)
{
    return person.LastName
        .Map(last => $"{person.FirstName} {last}")
        .GetValue(person.FirstName);
}

// Usage
var personWithLastName = new Person("John", Maybe.Some("Doe"));
var personWithoutLastName = new Person("Jane", Maybe.None<string>());

Console.WriteLine(GetFullNameSafe(personWithLastName));   // "John Doe"
Console.WriteLine(GetFullNameSafe(personWithoutLastName)); // "Jane"
```

### Your First Result Example

Now let's see how `Result<T>` handles operations that can fail:

```csharp
using Bogoware.Monads;

// Traditional approach with exceptions
public User CreateUserUnsafe(string email, string password)
{
    if (string.IsNullOrEmpty(email) || !email.Contains("@"))
        throw new ArgumentException("Invalid email");
    
    if (password.Length < 8)
        throw new ArgumentException("Password too short");
    
    return new User(email, password);
}

// Using Result<T> for explicit error handling
public Result<User> CreateUserSafe(string email, string password)
{
    return ValidateEmail(email)
        .Bind(() => ValidatePassword(password))
        .Map(() => new User(email, password));
}

public Result<Unit> ValidateEmail(string email)
{
    if (string.IsNullOrEmpty(email) || !email.Contains("@"))
        return Result.Failure<Unit>("Invalid email address");
    
    return Result.Unit;
}

public Result<Unit> ValidatePassword(string password)
{
    if (password.Length < 8)
        return Result.Failure<Unit>("Password must be at least 8 characters");
    
    return Result.Unit;
}

// Usage
var successResult = CreateUserSafe("john@example.com", "secure123");
var failureResult = CreateUserSafe("invalid-email", "short");

successResult.Match(
    user => $"User created: {user.Email}",
    error => $"Error: {error.Message}"
);
```

### Combining Maybe and Result

Here's a practical example that combines both monads:

```csharp
using Bogoware.Monads;

public record Author(string FirstName, Maybe<string> LastName)
{
    public string FullName => LastName
        .Map(last => $"{FirstName} {last}")
        .GetValue(FirstName);
}

public record Book(string Title, Maybe<Author> Author);

public class BookService
{
    private readonly List<Book> _books = new();

    public Maybe<Book> FindBookByTitle(string title)
    {
        var book = _books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return book != null ? Maybe.Some(book) : Maybe.None<Book>();
    }

    public Result<string> GetBookDescription(string title)
    {
        return FindBookByTitle(title)
            .MapToResult(() => new LogicError($"Book '{title}' not found"))
            .Map(book => FormatBookDescription(book));
    }

    private string FormatBookDescription(Book book)
    {
        return book.Author
            .Map(author => $"'{book.Title}' by {author.FullName}")
            .GetValue(() => $"'{book.Title}' (Author unknown)");
    }
}

// Usage
var bookService = new BookService();
var result = bookService.GetBookDescription("Clean Code");

result.Match(
    description => Console.WriteLine(description),
    error => Console.WriteLine($"Error: {error.Message}")
);
```

### Advanced Pipeline Example

For more complex scenarios, you can chain multiple operations:

```csharp
public Result<User> ProcessUserRegistration(string email, string password, string confirmPassword)
{
    return ValidateEmail(email)
        .Bind(() => ValidatePassword(password))
        .Bind(() => ValidatePasswordMatch(password, confirmPassword))
        .Bind(() => CheckEmailNotExists(email))
        .Map(() => new User(email, password))
        .Bind(SaveUser)
        .IfSuccess(user => SendWelcomeEmail(user))
        .Match(
            user => Result.Success(user),
            error => LogError(error)
        );
}
```

This approach ensures that:
- Operations only proceed if the previous step succeeded
- Errors are captured and handled explicitly
- The code is more readable and maintainable
- No exceptions are thrown for expected failure cases


## Introduction to Monads

Monads are powerful tools for modeling operations in a functional way, making them a cornerstone 
of functional programming. While we won't delve into a detailed explanation of monads and their inner 
workings, there are numerous resources available online that approach the topic 
from different perspectives.

For the purpose of this introduction, we can consider monads as an abstraction of _safe container_ that encapsulates
the result of an operation. They provide methods that enable manipulation of the result in a safe manner,
ensuring that the execution flow follows the "happy" path in case of success and the "unhappy" path in case of failure. This model is also known as _railway-oriented programming_.

By employing monads, code can be protected from further processing in case of errors or missing data. 
Adopting a functional approach offers benefits such as increased readability, improved reasoning capabilities,
and more robust and error-resistant code.

## Library Overview

This library provides two well-known monads: `Result` and `Maybe` monads (also referred to as `Either`, 
`Optional`, `Option` in other contexts):

> The `Result<T>` monad is used to model operations that can fail.

>The `Maybe<T>` monad is used to model operations that can optionally return a value.

Additionally, the library provides the `Error` abstract class, which complements the `Result<T>` monad and
offers an ergonomic approach to error management at an application-wide scale.

## Result&lt;T&gt; Monad

## Design Goals for `Result<T>`

The `Result<T>` monad is designed for modeling operations that can either fail or return a value.
It is a generic type, with `T` representing the type of the value returned by the successful operation.

`Result<T>` provides a set of methods that facilitate chaining operations in a functional way:
* `Map`: Allows transformation of the value returned by the operation, representing the "happy" flow.
  * `Map` to void functor will map to `Result<Unit>`
  * `MapToUnit()` is just a shortcut for `Map(_ => { })`
* `MapError`: Allows transformation of the error returned by the operation, representing the "unhappy" flow.
* `Bind`: Enables chaining of operations providing a fluent syntax that allows
 to capture the values on the "happy" path and use them in subsequent steps.
* `Match`: Facilitates handling of the operation's result by providing separate paths for the "happy" and "unhappy" flows.
* `RecoverWith`: Provides a way to recover from an error by returning a `Result<T>`
* `Ensure`: Allows asserting a condition on the value returned by the operation.
* `IfSuccess`: Executes if the operation succeeds. It is typically used to generate side effects.
* `IfFailure`: Executes if the operation fails. It is typically used to generate side effects.

There are also some unsafe methods intended to support developers who are less familiar with the functional approach
and may need to resort to a procedural style to achieve their goals.
These methods should be used sparingly, as they deviate from the functional paradigm and make the code less
robust, potentially leading to unexpected exceptions:

* `ThrowIfFailure()`: Throws an exception if the operation fails. It is typically used to terminate the execution of the pipeline
  discarding the result of the operation.
* `Value` or `GetValueOrThrow()`: Extracts the value from the `Result<T>` monad.
* `Error` or `GetErrorOrThrow()`: Extracts the error from the `Result<T>` monad. 

By adhering to the `Result<T>` monad, code can be modeled in a more readable and reasoned manner.
It also contributes to writing more robust code with reduced error-proneness.

### Complete `Result<T>` API Reference

#### Core Methods

##### Map
Transforms the value if the result is successful:

```csharp
var result = Result.Success(42);
var doubled = result.Map(x => x * 2); // Result<int> with value 84

// Map to different type
var text = result.Map(x => $"Value: {x}"); // Result<string>

// Map to Unit (void operations)
var unit = result.Map(x => Console.WriteLine(x)); // Result<Unit>
var unit2 = result.MapToUnit(); // Shortcut for discarding the value
```

##### Bind
Chains operations that return `Result<T>`:

```csharp
public Result<int> ParseNumber(string text) => 
    int.TryParse(text, out var num) ? Result.Success(num) : Result.Failure<int>("Invalid number");

public Result<string> FormatNumber(int number) =>
    number >= 0 ? Result.Success($"#{number:D4}") : Result.Failure<string>("Negative numbers not allowed");

// Chain operations
var result = ParseNumber("42")
    .Bind(FormatNumber); // Result<string> with "#0042"
```

##### Match
Handles both success and failure cases:

```csharp
var result = CreateUser("john@example.com");
var message = result.Match(
    user => $"Created user: {user.Email}",
    error => $"Failed: {error.Message}"
);
```

##### MapError
Transforms error types:

```csharp
var result = Result.Failure<string>("Database connection failed");
var mappedError = result.MapError(err => new CustomError($"Service Error: {err.Message}"));
```

##### RecoverWith
Provides fallback values on failure:

```csharp
var result = Result.Failure<string>("Network error");
var recovered = result.RecoverWith("Default value"); // Result<string> with "Default value"

// Using function for lazy evaluation
var recovered2 = result.RecoverWith(() => GetFallbackValue());
```

##### Ensure
Validates conditions and fails if not met:

```csharp
var result = Result.Success("john@example.com")
    .Ensure(email => email.Contains("@"), new ValidationError("Invalid email format"));
```

##### Side Effects: IfSuccess and IfFailure
Execute actions without changing the result:

```csharp
var result = CreateUser("john@example.com")
    .IfSuccess(user => Logger.Info($"User created: {user.Id}"))
    .IfFailure(error => Logger.Error($"Creation failed: {error.Message}"));
```

##### Satisfy
Check conditions on the result value (class types only):

```csharp
var result = Result.Success("john@example.com");
var isValidEmail = result.Satisfy(email => email.Contains("@")); // Returns true

var failedResult = Result.Failure<string>("Error");
var check = failedResult.Satisfy(email => email.Contains("@")); // Returns false
```

#### Unsafe Methods (Use Sparingly)

```csharp
var result = Result.Success(42);

// Extract value or throw exception
var value = result.GetValueOrThrow(); // Returns 42
var value2 = result.Value; // Same as above

// Extract error or throw exception  
var failedResult = Result.Failure<int>("Error message");
var error = failedResult.GetErrorOrThrow(); // Returns Error
var error2 = failedResult.Error; // Same as above

// Throw if result is failure
result.ThrowIfFailure(); // No exception thrown for success
failedResult.ThrowIfFailure(); // Throws ResultFailedException
```

### `Result` Static Helper Methods

The `Result` class provides a comprehensive set of helper methods that facilitate the creation of `Result<T>` instances and
make the code more readable and functional.

#### Factory Methods

```csharp
// Create successful results
var success = Result.Success(42); // Result<int>
var unitSuccess = Result.Unit; // Result<Unit> for void operations

// Create failed results
var failure1 = Result.Failure<int>("Something went wrong"); // Uses LogicError
var failure2 = Result.Failure<int>(new CustomError("Custom error")); // Uses custom error

// Create from values
var fromValue = Result.From(42); // Result<int> - Success
```

#### Safe Execution

```csharp
// Execute actions safely (catches exceptions as RuntimeError)
var result1 = Result.Execute(() => RiskyOperation()); // Result<Unit>
var result2 = Result.Execute(() => ComputeValue()); // Result<T>

// Async execution
var asyncResult = await Result.Execute(async () => await RiskyAsyncOperation());
```

#### Conditional Results

```csharp
// Create results based on conditions
var result1 = Result.Ensure(userAge >= 18, () => new ValidationError("Must be 18+"));
var result2 = Result.Ensure(() => IsValidOperation(), () => new LogicError("Invalid state"));

// Async conditions
var asyncResult = await Result.Ensure(async () => await ValidateAsync(), 
                                     () => new ValidationError("Validation failed"));
```

#### Functional Composition

```csharp
// Start chains with Result.Bind for consistent syntax
var result = Result.Bind(() => GetInitialValue())
    .Bind(ValidateValue)
    .Bind(ProcessValue)
    .Map(FormatOutput);

// Instead of:
var result2 = GetInitialValue() // Direct call breaks the chain style
    .Bind(ValidateValue)
    .Bind(ProcessValue)
    .Map(FormatOutput);
```

#### Complete Example

For example, instead of writing:
```csharp
/// Publishes the project
public Result<Unit> Publish() {
    if (PublishingStatus == PublishingStatus.Published)
        return new InvalidOperationError("Already published");
    
    return ValidateCostComponents() // Note the explicit invocation of the method
        .Bind(ValidateTimingComponents)
        // ... more binding to validation methods
        .IfSuccess(() => PublishingStatus = PublishingStatus.Published);
}
```

You can write:
```csharp
/// Publishes the project
public Result<Unit> Publish() => Result
    .Ensure(PublishingStatus != PublishingStatus.Published, () => new InvalidOperationError("Already published"))
    .Bind(ValidateCostComponents)
    .Bind(ValidateTimingComponents)
    // ... more binding to validation methods
    .IfSuccess(() => PublishingStatus = PublishingStatus.Published);
```

## Working with Collections

### Manipulating `IEnumerable<Maybe<T>>`

The library provides a comprehensive set of extension methods for working with sequences of `Maybe<T>` instances:

#### Core Collection Methods

```csharp
var books = new List<Maybe<Book>> { 
    Maybe.Some(new Book("1984", "Orwell")), 
    Maybe.None<Book>(), 
    Maybe.Some(new Book("Brave New World", "Huxley")) 
};

// SelectValues: Extract all Some values, discard None values
var validBooks = books.SelectValues(); // IEnumerable<Book> with 2 books

// MapEach: Transform each Maybe, preserving None values
var upperTitles = books.MapEach(book => book.Title.ToUpper()); 
// IEnumerable<Maybe<string>> with 2 Some values and 1 None

// BindEach: Chain operations on each Maybe, preserving None values  
var authors = books.BindEach(book => book.Author); 
// IEnumerable<Maybe<Author>>

// MatchEach: Transform all Maybes to a common type
var descriptions = books.MatchEach(
    book => $"Book: {book.Title}",
    "No book"
); // IEnumerable<string>
```

#### Filtering and Predicates

```csharp
var names = new[] {
    Maybe.Some("Alice"), Maybe.None<string>(), Maybe.Some("Bob"), Maybe.Some("Charlie")
};

// Where: Filter Some values based on predicate, None values are discarded
var shortNames = names.Where(n => n.Length <= 3); // Maybe<string>[] with Some("Bob")

// WhereNot: Filter Some values with negated predicate
var longNames = names.WhereNot(n => n.Length <= 3); // Maybe<string>[] with Some("Alice"), Some("Charlie")

// Predicate methods (requires reference types)
var allHaveValues = names.AllSome(); // false (contains None)
var allEmpty = names.AllNone(); // false (contains Some values)
```

### Manipulating `IEnumerable<Result<T>>`

The library provides powerful extension methods for working with sequences of `Result<T>` instances:

#### Core Collection Methods

```csharp
var operations = new[] {
    Result.Success("file1.txt"),
    Result.Failure<string>("Access denied"),
    Result.Success("file3.txt")
};

// SelectValues: Extract all successful values, discard failures
var successfulFiles = operations.SelectValues(); // IEnumerable<string> with 2 files

// MapEach: Transform each Result, preserving failures
var processedFiles = operations.MapEach(file => file.ToUpper());
// IEnumerable<Result<string>> with 2 successes and 1 failure

// BindEach: Chain operations on each Result, preserving failures
var fileContents = operations.BindEach(ReadFileContent);
// IEnumerable<Result<string>>

// MatchEach: Transform all Results to a common type
var messages = operations.MatchEach(
    file => $"Processed: {file}",
    error => $"Error: {error.Message}"
); // IEnumerable<string>
```

#### Predicate Methods

```csharp
var results = new[] {
    Result.Success(1),
    Result.Failure<int>("Error 1"), 
    Result.Success(2),
    Result.Failure<int>("Error 2")
};

// Check if all results are successful
var allSucceeded = results.AllSuccess(); // false

// Check if all results failed
var allFailed = results.AllFailure(); // false

// Check if any result succeeded
var anySucceeded = results.AnySuccess(); // true

// Check if any result failed  
var anyFailed = results.AnyFailure(); // true
```

#### Aggregation

```csharp
var userOperations = new[] {
    CreateUser("john@example.com"),
    CreateUser("jane@example.com"),
    CreateUser("invalid-email") // This will fail
};

// AggregateResults: Combine all results into a single Result
var aggregated = userOperations.AggregateResults();
// Result<IEnumerable<User>> - fails with AggregateError containing all errors

// If all operations succeed:
var allSuccess = new[] {
    Result.Success(1),
    Result.Success(2),
    Result.Success(3)
};
var combined = allSuccess.AggregateResults(); // Result<IEnumerable<int>> with [1, 2, 3]
```

## Error Types and Management

### Design Goals for `Error`

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

### Built-in Error Types

#### LogicError
Base class for application logic errors:

```csharp
// Simple logic error
var error = new LogicError("Invalid input provided");
var result = Result.Failure<string>(error);
```

#### RuntimeError  
Wraps exceptions that occur during execution:

```csharp
try 
{
    // Some risky operation
    var data = await riskOperation();
    return Result.Success(data);
}
catch (Exception ex)
{
    return Result.Failure<string>(new RuntimeError(ex));
}

// Or use Result.Execute to handle this automatically:
var result = Result.Execute(() => riskyOperation());
```

#### AggregateError
Contains multiple errors, typically from `AggregateResults`:

```csharp
var operations = new[] {
    Result.Failure<int>("Error 1"),
    Result.Failure<int>("Error 2"), 
    Result.Success(42)
};

var aggregated = operations.AggregateResults();
// Result fails with AggregateError containing "Error 1" and "Error 2"

if (aggregated.IsFailure && aggregated.Error is AggregateError aggError)
{
    foreach (var error in aggError.Errors)
    {
        Console.WriteLine($"Individual error: {error.Message}");
    }
}
```

#### MaybeNoneError
Default error when converting `Maybe.None` to `Result`:

```csharp
var maybe = Maybe.None<string>();
var result = maybe.MapToResult(); // Result<string> fails with MaybeNoneError

// Custom error instead:
var result2 = maybe.MapToResult(() => new LogicError("Value was not found"));
```

### Error Hierarchy Best Practices
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

As demonstrated in the project [FluentValidationSample](./sample/FluentValidationSample) the `FluentValidation` library 
can be used to model validation errors.

In contrast to `LogicError`s, `RuntimeError`s are generated by the `Result.Execute()` methods to encapsulate exceptions 
thrown by the application.

## Async Programming with Monads

Both `Result<T>` and `Maybe<T>` provide full async support for all major operations:

### Async Result Operations

```csharp
// Async Map
var result = await Result.Success("file.txt")
    .Map(async fileName => await File.ReadAllTextAsync(fileName));

// Async Bind
public async Task<Result<User>> GetUserAsync(int id) => 
    await ValidateId(id)
        .Bind(async validId => await database.GetUserAsync(validId));

// Async side effects
var result = await CreateUserAsync(email)
    .IfSuccess(async user => await SendWelcomeEmailAsync(user))
    .IfFailure(async error => await LogErrorAsync(error));

// Async Match
var message = await result.Match(
    async user => await FormatUserDetailsAsync(user),
    async error => await FormatErrorMessageAsync(error)
);

// Async Ensure
var validated = await result
    .Ensure(async user => await IsUserActiveAsync(user), 
            new LogicError("User is not active"));
```

### Async Maybe Operations

```csharp
// Async Map
var maybe = await Maybe.Some("data")
    .Map(async data => await ProcessDataAsync(data));

// Async Bind  
var result = await Maybe.Some(userId)
    .Bind(async id => await FindUserAsync(id));

// Async side effects
await maybe
    .IfSome(async value => await ProcessValueAsync(value))
    .IfNone(async () => await HandleMissingValueAsync());

// Async WithDefault
var withDefault = await Maybe.None<string>()
    .WithDefault(async () => await GetDefaultValueAsync());
```

### Task<Result<T>> and Task<Maybe<T>> Extensions

All methods work seamlessly with `Task<Result<T>>` and `Task<Maybe<T>>`:

```csharp
// Chain async operations
public async Task<Result<ProcessedData>> ProcessUserDataAsync(int userId)
{
    return await GetUserAsync(userId)          // Task<Result<User>>
        .Bind(async user => await GetUserDataAsync(user.Id))  // Chain with async
        .Map(async data => await ProcessDataAsync(data))      // Async transform
        .IfSuccess(async result => await CacheResultAsync(result)); // Async side effect
}

// Using Result.Execute for async operations
var result = await Result.Execute(async () => await RiskyAsyncOperation());
```

## Advanced Patterns and Best Practices

### Railway-Oriented Programming

Chain operations to create robust data processing pipelines:

```csharp
public async Task<Result<ProcessedOrder>> ProcessOrderAsync(OrderRequest request)
{
    return await ValidateOrderRequest(request)
        .Bind(ValidateCustomer)
        .Bind(ValidateInventory)
        .Bind(CalculatePricing)
        .Bind(async order => await SaveOrderAsync(order))
        .Bind(async order => await ProcessPaymentAsync(order))
        .IfSuccess(async order => await SendConfirmationAsync(order))
        .Match(
            order => Result.Success(order),
            async error => await HandleOrderErrorAsync(error)
        );
}
```

### Combining Maybe and Result

Convert between `Maybe<T>` and `Result<T>` as needed:

```csharp
public Result<UserProfile> GetUserProfile(int userId)
{
    return FindUser(userId)                    // Maybe<User>
        .MapToResult(() => new NotFoundError("User not found"))  // Result<User>
        .Bind(user => LoadUserProfile(user))   // Result<UserProfile>
        .Map(profile => EnrichProfile(profile)); // Result<UserProfile>
}
```

### Error Recovery Patterns

```csharp
// Fallback to default values
var config = LoadConfigFromFile()
    .RecoverWith(() => LoadConfigFromEnvironment())
    .RecoverWith(GetDefaultConfig());

// Retry with different strategies  
var result = await TryPrimaryService()
    .RecoverWith(async () => await TrySecondaryService())
    .RecoverWith(async () => await TryFallbackService());
```

### Validation Patterns

```csharp
public Result<ValidatedUser> ValidateUser(UserInput input)
{
    return ValidateEmail(input.Email)
        .Bind(() => ValidatePassword(input.Password))
        .Bind(() => ValidateAge(input.Age))
        .Map(() => new ValidatedUser(input));
}

// Or using Result.Ensure for inline validation
public Result<User> CreateUser(string email, string password)
{
    return Result.Success(new User(email, password))
        .Ensure(user => user.Email.Contains("@"), new ValidationError("Invalid email"))
        .Ensure(user => user.Password.Length >= 8, new ValidationError("Password too short"));
}
```

## Maybe&lt;T&gt; Monad

### Design Goals for `Maybe<T>`

Before discussing what can be achieved with the `Maybe<T>` monad, let's clarify that it is not intended as a 
replacement for `Nullable<T>`.
This is mainly due to fundamental libraries, such as Entity Framework, relying on `Nullable<T>` to model class
attributes, while support for structural types remains limited.

A pragmatic approach involves using `Nullable<T>` for modeling class attributes and `Maybe<T>` for modeling
return values and method parameters.

The advantage of using `Maybe<T>` over `Nullable<T>` is that `Maybe<T>` provides a set of methods that enable
chaining operations in a functional manner.
This becomes particularly useful when dealing with operations that can optionally return a value,
such as querying a database.

The implicit conversion from `Nullable<T>` to `Maybe<T>` allows for lifting `Nullable<T>` values to `Maybe<T>`
values and utilizing `Maybe<T>` methods for chaining operations.

> **Practical rule**: Use `Nullable<T>` to model class attributes and `Maybe<T>` to model return values and
> method parameters.

### Complete `Maybe<T>` API Reference

#### Core Methods

##### Map
Transforms the value if present:

```csharp
var maybe = Maybe.Some("hello");
var upper = maybe.Map(s => s.ToUpper()); // Maybe<string> with "HELLO"

var none = Maybe.None<string>();
var result = none.Map(s => s.ToUpper()); // Still None
```

##### Bind
Chains operations that return `Maybe<T>`:

```csharp
public Maybe<string> ValidateName(string name) =>
    !string.IsNullOrWhiteSpace(name) ? Maybe.Some(name.Trim()) : Maybe.None<string>();

var result = Maybe.Some("  John  ")
    .Bind(ValidateName); // Maybe<string> with "John"
```

##### Match
Handles both Some and None cases:

```csharp
var maybe = Maybe.Some("John");
var greeting = maybe.Match(
    name => $"Hello, {name}!",
    "Hello, stranger!"
);
```

##### GetValue
Retrieves value with fallback:

```csharp
var maybe = Maybe.None<string>();
var value = maybe.GetValue("default"); // Returns "default"
var value2 = maybe.GetValue(() => GetDefaultValue()); // Lazy evaluation
```

##### OfType
Safe type casting (note: both types must be reference types due to class constraints):

```csharp
Maybe<object> maybe = Maybe.Some("hello" as object);
var stringMaybe = maybe.OfType<string>(); // Maybe<string> with "hello"

// Note: OfType has type constraints that limit its use with value types
```

##### Side Effects: IfSome and IfNone

```csharp
var maybe = Maybe.Some("important data");
maybe
    .IfSome(data => Logger.Info($"Processing: {data}"))
    .IfNone(() => Logger.Warn("No data to process"));
```

##### Execute
Perform actions on the entire Maybe:

```csharp
var maybe = Maybe.Some(42);
maybe.Execute(m => Console.WriteLine($"Maybe contains: {m.IsSome}"));
```

##### Predicates and Filtering (class types only)

```csharp
var maybe = Maybe.Some("42");

// Check conditions (Satisfy works with class types)
var isNumeric = maybe.Satisfy(x => int.TryParse(x, out _)); // Returns true

// Filter with Where (works on nullable value types)
var evenNumber = (42 as int?).Where(x => x % 2 == 0); // Maybe<int> with 42
var oddNumber = (42 as int?).Where(x => x % 2 == 1); // Maybe<int> as None

// WhereNot (inverse filter)
var notEven = (42 as int?).WhereNot(x => x % 2 == 0); // Maybe<int> as None
```

##### WithDefault
Provide fallback values:

```csharp
var none = Maybe.None<string>();
var withDefault = none.WithDefault("fallback"); // Maybe<string> with "fallback"
var withLazyDefault = none.WithDefault(() => ExpensiveOperation());
```

#### Factory Methods

```csharp
// Create Some value
var some1 = Maybe.Some("value");
var some2 = new Maybe<string>("value"); // Equivalent

// Create None
var none1 = Maybe.None<string>();
var none2 = new Maybe<string>(); // Equivalent

// Create from nullable
string? nullable = null;
var maybe1 = Maybe.From(nullable); // Maybe<string> as None
var maybe2 = (Maybe<string>)nullable; // Implicit conversion
```

#### Collection Extensions

```csharp
// Convert IEnumerable to Maybe (first element or None)
var numbers = new[] { 1, 2, 3 };
var firstNumber = numbers.ToMaybe(); // Maybe<int> with 1

var empty = new int[0];
var noNumber = empty.ToMaybe(); // Maybe<int> as None
```

### Converting `Maybe<T>` to `Result<T>`

It is common to implement a pipeline of operations where an empty `Maybe<T>` instance should be interpreted as a failure,
in this case the `Maybe<T>` instance can be converted to a `Result<T>` instance by using the `MapToResult` method.

The `MapToResult` methods can accepts an error as a parameter and returns a `Result<T>` instance with the specified error
in case the `Maybe<T>` instance is empty.

For example, consider the following code snippet:

```csharp
var result = Maybe
    .From(someFactoryMethod())
    .MapToResult(() => new LogicError("Value not found"))
    .Bind(ValidateValue)
    .Bind(UpdateValue);

// Without custom error (uses default MaybeNoneError)
var result2 = Maybe.Some("value").MapToResult();
```
