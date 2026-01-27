---
sidebar_position: 2
title: Working with Collections
---

# Working with Collections

The library provides extension methods for working with sequences of `Maybe<T>` and `Result<T>` instances.

## IEnumerable&lt;Maybe&lt;T&gt;&gt; Extensions

### SelectValues

Extract all Some values, discarding None:

```csharp
var books = new List<Maybe<Book>> {
    Maybe.Some(new Book("1984")),
    Maybe.None<Book>(),
    Maybe.Some(new Book("Brave New World"))
};

var validBooks = books.SelectValues(); // IEnumerable<Book> with 2 books
```

### MapEach

Transform each Maybe, preserving None values:

```csharp
var upperTitles = books.MapEach(book => book.Title.ToUpper());
// IEnumerable<Maybe<string>> with 2 Some values and 1 None
```

### BindEach

Chain operations on each Maybe:

```csharp
var authors = books.BindEach(book => book.Author);
// IEnumerable<Maybe<Author>>
```

### MatchEach

Transform all Maybes to a common type:

```csharp
var descriptions = books.MatchEach(
    book => $"Book: {book.Title}",
    "No book"
); // IEnumerable<string>
```

### Predicates

```csharp
var names = new[] { Maybe.Some("Alice"), Maybe.None<string>(), Maybe.Some("Bob") };

var allHaveValues = names.AllSome(); // false
var allEmpty = names.AllNone(); // false
```

## IEnumerable&lt;Result&lt;T&gt;&gt; Extensions

### SelectValues

Extract all successful values:

```csharp
var operations = new[] {
    Result.Success("file1.txt"),
    Result.Failure<string>("Access denied"),
    Result.Success("file3.txt")
};

var successfulFiles = operations.SelectValues(); // 2 files
```

### MapEach / BindEach / MatchEach

Same patterns as Maybe:

```csharp
var processed = operations.MapEach(file => file.ToUpper());
var contents = operations.BindEach(ReadFileContent);
var messages = operations.MatchEach(
    file => $"Processed: {file}",
    error => $"Error: {error.Message}"
);
```

### Predicates

```csharp
var allSucceeded = results.AllSuccess();
var allFailed = results.AllFailure();
var anySucceeded = results.AnySuccess();
var anyFailed = results.AnyFailure();
```

### AggregateResults

Combine all results into a single Result:

```csharp
var userOperations = new[] {
    CreateUser("john@example.com"),
    CreateUser("jane@example.com"),
    CreateUser("invalid-email") // Fails
};

var aggregated = userOperations.AggregateResults();
// Result<IEnumerable<User>> - fails with AggregateError

// If all succeed:
var allSuccess = new[] {
    Result.Success(1),
    Result.Success(2),
    Result.Success(3)
};
var combined = allSuccess.AggregateResults();
// Result<IEnumerable<int>> with [1, 2, 3]
```
