---
sidebar_position: 2
title: Changelog
---

# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [11.4.0] - 2026-01-01

### Added
- Multi-target support: `netstandard2.1`, `net8.0`, `net9.0`, `net10.0`
- Guard polyfill for `ArgumentNullException.ThrowIfNull` compatibility

### Changed
- Updated SDK to .NET 10.0 with `rollForward: latestFeature`
- Updated CI workflow to test against all supported runtimes (.NET 8, 9, 10)
- Updated test dependencies
- Improved README with verified code examples and platform requirements

### Removed
- Dropped `net6.0` and `net7.0` targets (EOL)

## [11.3.2] - 2024-12-15

### Fixed
- Fixed typos in codebase and test method naming
- Comprehensive README improvements with better examples

### Changed
- Updated dependencies: FluentAssertions 7.0.0, xunit.runner.visualstudio 3.0.0, Microsoft.NET.Test.Sdk 17.12.0

## [11.3.0] - 2024-11-20

### Added
- `Result.From()` static factory method for creating results

## [11.2.0] - 2024-11-15

### Added
- `Result<T>.Value` property as alternative to `GetValueOrThrow()`
- `Result<T>.Error` property as alternative to `GetErrorOrThrow()`

## [11.1.0] - 2024-11-10

### Added
- `Result` now supports nullable `Value` and `Error` properties via extension methods

## [11.0.0] - 2024-11-01

### Changed
- **BREAKING**: `ExecuteIfSuccess` renamed to `IfSuccess`
- **BREAKING**: `ExecuteIfFailure` renamed to `IfFailure`
- **BREAKING**: `ExecuteIfSome` renamed to `IfSome`
- **BREAKING**: `ExecuteIfNone` renamed to `IfNone`

## [10.2.0] - 2024-10-20

### Added
- Additional async overloads for `MapToResult`

## [10.1.0] - 2024-10-15

### Added
- Async support for `MapToResult` method

## [10.0.0] - 2024-10-01

### Changed
- **BREAKING**: `ToResult` renamed to `MapToResult` for API consistency

## [9.3.0] - 2024-09-25

### Changed
- Reapplied `readonly` modifier to structs

## [9.2.0] - 2024-09-20

### Changed
- Reapplied `readonly` modifier to structs

## [9.1.0] - 2024-09-15

### Fixed
- Results now behave correctly with value types

### Changed
- Removed `readonly` modifier from structs to support serialization
- Updated dependencies (MinVer 6.0.0, xunit 2.9.2, FluentAssertions 6.12.2)

### Added
- Dependabot configuration for automated dependency updates

## [9.0.3] - 2024-08-01

### Added
- `MapEach` extension method on `IEnumerable<Maybe<T>>` - maps each Maybe, preserving None values
- `BindEach` extension method on `IEnumerable<Maybe<T>>` - binds each Maybe, preserving None values
- `MatchEach` extension method on `IEnumerable<Maybe<T>>` - matches each Maybe in sequence

### Changed
- **BREAKING**: `Map` on `IEnumerable<Maybe<T>>` removed - use `MapEach` instead
- **BREAKING**: `Bind` on `IEnumerable<Maybe<T>>` removed - use `BindEach` instead
- **BREAKING**: `Match` on `IEnumerable<Maybe<T>>` renamed to `MatchEach`

## [9.0.1] - 2024-07-15

### Added
- Multi-targeting support for `netstandard2.1`, `net6.0`, `net7.0`

## [9.0.0] - 2024-07-01

### Added
- `MapEach` extension method on `IEnumerable<Result<T>>` - maps each Result, preserving failures
- `BindEach` extension method on `IEnumerable<Result<T>>` - binds each Result, preserving failures
- `MatchEach` extension method on `IEnumerable<Result<T>>` - matches each Result in sequence
- `AggregateResults` extension method - transforms `IEnumerable<Result<T>>` into `Result<IEnumerable<T>>`, aggregating errors

### Changed
- **BREAKING**: `Map` on `IEnumerable<Result<T>>` removed - use `MapEach` instead
- **BREAKING**: `Bind` on `IEnumerable<Result<T>>` removed - use `BindEach` instead
- **BREAKING**: `Match` on `IEnumerable<Result<T>>` renamed to `MatchEach`

## [8.0.0] - 2024-01-15

### Changed
- Upgraded to .NET 8.0
- Version scheme now aligns with .NET version

## [0.2.0] - 2023-07-10

### Added
- `Map` now supports nullable types

## [0.1.0] - 2023-04-18

### Added
- Initial release of Bogoware.Monads library
- `Maybe<T>` monad for modeling optional values
- `Result<T>` monad for modeling operations that can fail
- `Error` abstract class with `LogicError` and `RuntimeError` hierarchy
- Async support for all monad operations
- LINQ query syntax support
- Implicit conversions for ergonomic API
- NuGet packaging and CI/CD pipeline

[Unreleased]: https://github.com/bogoware/Monads/compare/v11.4.0...HEAD
[11.4.0]: https://github.com/bogoware/Monads/compare/v11.3.2...v11.4.0
[11.3.2]: https://github.com/bogoware/Monads/compare/v11.3.0...v11.3.2
[11.3.0]: https://github.com/bogoware/Monads/compare/v11.2.0...v11.3.0
[11.2.0]: https://github.com/bogoware/Monads/compare/v11.1.0...v11.2.0
[11.1.0]: https://github.com/bogoware/Monads/compare/v11.0.0...v11.1.0
[11.0.0]: https://github.com/bogoware/Monads/compare/v10.2.0...v11.0.0
[10.2.0]: https://github.com/bogoware/Monads/compare/v10.1.0...v10.2.0
[10.1.0]: https://github.com/bogoware/Monads/compare/v10.0.0...v10.1.0
[10.0.0]: https://github.com/bogoware/Monads/compare/v9.3.0...v10.0.0
[9.3.0]: https://github.com/bogoware/Monads/compare/v9.2.0...v9.3.0
[9.2.0]: https://github.com/bogoware/Monads/compare/v9.1.0...v9.2.0
[9.1.0]: https://github.com/bogoware/Monads/compare/v9.0.3...v9.1.0
[9.0.3]: https://github.com/bogoware/Monads/compare/v9.0.0...v9.0.3
[9.0.1]: https://github.com/bogoware/Monads/compare/v9.0.0...v9.0.1
[9.0.0]: https://github.com/bogoware/Monads/compare/v8.0.0...v9.0.0
[8.0.0]: https://github.com/bogoware/Monads/compare/v0.2.0...v8.0.0
[0.2.0]: https://github.com/bogoware/Monads/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/bogoware/Monads/releases/tag/v0.1.0
