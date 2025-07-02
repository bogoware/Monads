using System;
using Bogoware.Monads;
using Xunit;
using FluentAssertions;
using Bogoware.Monads.UnitTests.Boilerplate;

namespace Bogoware.Monads.UnitTests.AnalyzerTests
{
    /// <summary>
    /// Tests to validate that static analyzer warnings are working correctly.
    /// These tests demonstrate the proper usage patterns and the warnings for suboptimal patterns.
    /// </summary>
    public class LazyEvaluationWarningTests
    {
        private static int callCount = 0;
        
        private static string ExpensiveMethod()
        {
            callCount++;
            return $"Result {callCount}";
        }
        
        [Fact]
        public void Map_WithDirectValue_ShouldGenerateWarning()
        {
            // This test documents that using direct values generates warnings
            // The warning will appear at compile time, not runtime
            var maybe = Maybe.Some("test");
            
#pragma warning disable CS0618 // Suppress the obsolete warning for this test
            var result = maybe.Map("direct value");
#pragma warning restore CS0618
            
            result.IsSome.Should().BeTrue();
        }
        
        [Fact]
        public void Map_WithLambda_ShouldNotGenerateWarning()
        {
            // This test shows the preferred approach that doesn't generate warnings
            var maybe = Maybe.Some("test");
            
            var result = maybe.Map(() => "lambda value");
            
            result.IsSome.Should().BeTrue();
        }
        
        [Fact]
        public void WithDefault_WithDirectValue_ShouldGenerateWarning()
        {
            // This test documents that using direct values generates warnings
            var maybe = Maybe.None<string>();
            
#pragma warning disable CS0618 // Suppress the obsolete warning for this test
            var result = maybe.WithDefault("direct default");
#pragma warning restore CS0618
            
            result.IsSome.Should().BeTrue();
        }
        
        [Fact]
        public void WithDefault_WithLambda_ShouldNotGenerateWarning()
        {
            // This test shows the preferred approach that doesn't generate warnings
            var maybe = Maybe.None<string>();
            
            var result = maybe.WithDefault(() => "lambda default");
            
            result.IsSome.Should().BeTrue();
        }
        
        [Fact]
        public void RecoverWith_WithDirectValue_ShouldGenerateWarning()
        {
            // This test documents that using direct values generates warnings for Result monad
            var result = Result.Failure<string>(new LogicError("test error"));
            
#pragma warning disable CS0618 // Suppress the obsolete warning for this test
            var recovered = result.RecoverWith<string, Error>("direct recovery value");
#pragma warning restore CS0618
            
            recovered.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void RecoverWith_WithLambda_ShouldNotGenerateWarning()
        {
            // This test shows the preferred approach that doesn't generate warnings for Result monad
            var result = Result.Failure<string>(new LogicError("test error"));
            
            var recovered = result.RecoverWith<string, Error>(() => "lambda recovery value");
            
            recovered.IsSuccess.Should().BeTrue();
        }
        
        [Fact]
        public void LazyEvaluation_DemonstrateBenefit_WithMaybeNone()
        {
            // This test demonstrates the benefit of lazy evaluation
            callCount = 0;
            var maybe = Maybe.None<string>();
            
            // Using direct method call - method is called even though maybe is None
#pragma warning disable CS0618
            callCount = 0;
            var resultDirect = maybe.Map(ExpensiveMethod());
            var directCallCount = callCount;
#pragma warning restore CS0618
            
            // Using lambda - method is NOT called because maybe is None
            callCount = 0;
            var resultLazy = maybe.Map(() => ExpensiveMethod());
            var lazyCallCount = callCount;
            
            // Verify that direct approach calls the method unnecessarily
            directCallCount.Should().Be(1, "Direct approach should call ExpensiveMethod even when Maybe is None");
            lazyCallCount.Should().Be(0, "Lazy approach should NOT call ExpensiveMethod when Maybe is None");
            
            // Both results should be None
            resultDirect.IsNone.Should().BeTrue();
            resultLazy.IsNone.Should().BeTrue();
        }
        
        [Fact]
        public void LazyEvaluation_DemonstrateBenefit_WithMaybeSome()
        {
            // This test shows that with Some, both approaches work but lazy is still preferred
            callCount = 0;
            var maybe = Maybe.Some("test");
            
            // Using direct method call
#pragma warning disable CS0618
            callCount = 0;
            var resultDirect = maybe.Map(ExpensiveMethod());
            var directCallCount = callCount;
#pragma warning restore CS0618
            
            // Using lambda
            callCount = 0;
            var resultLazy = maybe.Map(() => ExpensiveMethod());
            var lazyCallCount = callCount;
            
            // Both should call the method once when Maybe is Some
            directCallCount.Should().Be(1, "Direct approach should call ExpensiveMethod when Maybe is Some");
            lazyCallCount.Should().Be(1, "Lazy approach should call ExpensiveMethod when Maybe is Some");
            
            // Both results should be Some
            resultDirect.IsSome.Should().BeTrue();
            resultLazy.IsSome.Should().BeTrue();
        }
        
        [Fact]
        public void LazyEvaluation_DemonstrateBenefit_WithResultSuccess()
        {
            // This test demonstrates lazy evaluation benefit with Result.Success
            callCount = 0;
            var result = Result.Success("test");
            
            // Using direct method call - method is called even though result is Success
#pragma warning disable CS0618
            callCount = 0;
            var recoveredDirect = result.RecoverWith<string, Error>(ExpensiveMethod());
            var directCallCount = callCount;
#pragma warning restore CS0618
            
            // Using lambda - method is NOT called because result is Success
            callCount = 0;
            var recoveredLazy = result.RecoverWith<string, Error>(() => ExpensiveMethod());
            var lazyCallCount = callCount;
            
            // Verify that direct approach calls the method unnecessarily
            directCallCount.Should().Be(1, "Direct approach should call ExpensiveMethod even when Result is Success");
            lazyCallCount.Should().Be(0, "Lazy approach should NOT call ExpensiveMethod when Result is Success");
            
            // Both results should be Success with original value
            recoveredDirect.IsSuccess.Should().BeTrue();
            recoveredLazy.IsSuccess.Should().BeTrue();
        }
    }
}