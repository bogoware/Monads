# Bogoware Monads Changelog

## 9.0.3

### New Features

- The following extension methods on `IEnumerable<Maybe<T>>` have been introduced:
- `MapEach`: maps each `Maybe` in the sequence, preserving the `None` values
- `BindEach`: binds each `Maybe` in the sequence, preserving the `None` values
- `MatchEach`: matches each `Maybe` in the sequence

### Breaking Changes

- The following extension methods on `IEnumerable<Maybe<T>>` have been removed:
  - `Map`: use `MapEach` instead. The latter will preserve the `None` values
  - `Bind`: use `BindEach` instead. The latter will preserve the `None` values
  - `Macth` renamed to `MatchEach`.

## 9.0.1

Added support for:
- netstandard2.1
- NET 6.0
- NET 7.0

## 9.0.0

### New Features

- The following extension methods on `IEnumerable<Result<T>>` have been introduced:
  - `MapEach`: maps each `Result` in the sequence, preserving the failed `Result`s
  - `BindEach`: binds each `Result` in the sequence, preserving the failed `Result`s
  - `MatchEach`: matches each `Result` in the sequence
  - `AggregateResults`: transforms a sequence of `Result`s into a single `Result` that contains a sequence of the successful values. If the original sequence contains any `Error` then will return a failed `Result` with an `AggregateError` containing all the errors found. 

### Breaking Changes
- The following extension methods on `IEnumerable<Result<T>>` have been removed:
  - `Map`: use `MapEach` instead. The latter will preserve the failed `Result`s
  - `Bind`: use `BindEach` instead. The latter will preserve the failed `Result`s
  - `Macth` renamed to `MatchEach`.