namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeToResultTests
{
    [Fact]
    public void Some_ToResult_ShouldReturnAnError()
    {
        var sut = Maybe.Some<string>("SomeValue");

        var actual = sut.MapToResult();

        actual.IsSuccess.Should().BeTrue();
        actual.GetValueOrThrow().Should().Be("SomeValue");
    }
    
    [Fact]
    public void None_ToResult_ShouldReturnAnError()
    {
        var sut = Maybe.None<string>();

        var actual = sut.MapToResult();

        actual.IsFailure.Should().BeTrue();
        actual.GetErrorOrThrow().Should().BeOfType<MaybeNoneError>();
    }
    
    [Fact]
    public void Some_ToResultWithError_ShouldReturnAnError()
    {
        var sut = Maybe.Some<string>("SomeValue");

        var actual = sut.MapToResult(() => new LogicError("Not Found"));

        actual.IsSuccess.Should().BeTrue();
        actual.GetValueOrThrow().Should().Be("SomeValue");
    }

    [Fact]
    public void None_ToResultWithError_ShouldReturnAnError()
    {
        var sut = Maybe.None<string>();

        var actual = sut.MapToResult(() => new LogicError("Not Found"));

        actual.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Some_ToResult_ShouldReturnAnError_withAsync()
    {
        var sut = Maybe.Some<string>("SomeValue");
        var errorFunc = new Func<Task<Error>>(() => Task.FromResult<Error>(new LogicError("Not Found")));

        var actual = await sut.MapToResult(errorFunc);

        actual.IsSuccess.Should().BeTrue();
        actual.GetValueOrThrow().Should().Be("SomeValue");
    }

    [Fact]
    public async Task None_ToResult_ShouldReturnAnError_withAsync()
    {
        var sut = Maybe.None<string>();
        var errorFunc = new Func<Task<Error>>(() => Task.FromResult<Error>(new LogicError("Not Found")));

        var actual = await sut.MapToResult(errorFunc);

        actual.IsFailure.Should().BeTrue();
    }
}