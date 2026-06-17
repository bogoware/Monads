using FluentAssertions;

namespace Bogoware.Monads.UnitTests.ErrorTests;

public class ErrorEnumerableExtensionsTests
{
    [Fact]
    public void ToAggregateError_WithMultipleErrors_ReturnsAggregateErrorWithAllErrors()
    {
        // Arrange
        var errors = new List<Error>
        {
            new LogicError("Error 1"),
            new LogicError("Error 2"),
            new RuntimeError(new Exception("Error 3"))
        };

        // Act
        var result = errors.ToAggregateError();

        // Assert
        result.Should().BeOfType<AggregateError>();
        result.Errors.Should().HaveCount(3);
        result.Errors.Should().BeEquivalentTo(errors);
        result.Message.Should().Be("Multiple errors occurred");
    }

    [Fact]
    public void ToAggregateError_WithSingleError_ReturnsAggregateErrorWithSingleError()
    {
        // Arrange
        var errors = new List<Error> { new LogicError("Single error") };

        // Act
        var result = errors.ToAggregateError();

        // Assert
        result.Should().BeOfType<AggregateError>();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().BeEquivalentTo(errors.First());
        result.Message.Should().Be("Multiple errors occurred");
    }

    [Fact]
    public void ToAggregateError_WithEmptyCollection_ReturnsAggregateErrorWithNoErrors()
    {
        // Arrange
        var errors = new List<Error>();

        // Act
        var result = errors.ToAggregateError();

        // Assert
        result.Should().BeOfType<AggregateError>();
        result.Errors.Should().BeEmpty();
        result.Message.Should().Be("Multiple errors occurred");
    }

    [Fact]
    public void ToAggregateError_WithCustomMessage_ReturnsAggregateErrorWithCustomMessage()
    {
        // Arrange
        var errors = new List<Error>
        {
            new LogicError("Error 1"),
            new LogicError("Error 2")
        };
        const string customMessage = "Custom aggregate error message";

        // Act
        var result = errors.ToAggregateError(customMessage);

        // Assert
        result.Should().BeOfType<AggregateError>();
        result.Errors.Should().HaveCount(2);
        result.Errors.Should().BeEquivalentTo(errors);
        result.Message.Should().Be(customMessage);
    }

    [Fact]
    public void ToAggregateError_WithCustomMessage_AndEmptyCollection_ReturnsAggregateErrorWithCustomMessage()
    {
        // Arrange
        var errors = new List<Error>();
        const string customMessage = "No errors found";

        // Act
        var result = errors.ToAggregateError(customMessage);

        // Assert
        result.Should().BeOfType<AggregateError>();
        result.Errors.Should().BeEmpty();
        result.Message.Should().Be(customMessage);
    }

    [Fact]
    public void ToAggregateError_WithNullCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<Error> errors = null!;

        // Act & Assert
        var action = () => errors.ToAggregateError();
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToAggregateError_WithCustomMessage_AndNullCollection_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<Error> errors = null!;
        const string customMessage = "Test message";

        // Act & Assert
        var action = () => errors.ToAggregateError(customMessage);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToAggregateError_WithCustomMessage_AndNullMessage_ThrowsArgumentNullException()
    {
        // Arrange
        var errors = new List<Error> { new LogicError("Error 1") };

        // Act & Assert
        var action = () => errors.ToAggregateError(null!);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToAggregateError_WithMixedErrorTypes_PreservesErrorTypes()
    {
        // Arrange
        var logicError = new LogicError("Logic error");
        var runtimeError = new RuntimeError(new Exception("Runtime error"));
        var maybeNoneError = new MaybeNoneError("Maybe none error");
        var errors = new List<Error> { logicError, runtimeError, maybeNoneError };

        // Act
        var result = errors.ToAggregateError();

        // Assert
        result.Should().BeOfType<AggregateError>();
        result.Errors.Should().HaveCount(3);
        result.Errors.Should().Contain(e => e is LogicError);
        result.Errors.Should().Contain(e => e is RuntimeError);
        result.Errors.Should().Contain(e => e is MaybeNoneError);
    }
}