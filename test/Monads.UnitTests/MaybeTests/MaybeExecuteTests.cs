using Moq;

namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeExecuteTests
{
    private readonly Mock<ICallInspector> _inspector = new();

    [Fact]
    public void IfSome_call_void_when_maybeIsSome()
    {
        var sut = Maybe.Some(new Value(0));
        sut.IfSome(_inspector.Object.MethodVoid);
        _inspector.Verify(i => i.MethodVoid());
    }

    [Fact]
    public void IfSome_doesntCall_void_when_maybeIsNone()
    {
        var sut = Maybe.None<Value>();
        sut.IfSome(_inspector.Object.MethodVoid);
        _inspector.VerifyNoOtherCalls();
    }

    [Fact]
    public void IfNone_doesntCall_void_when_maybeIsSome()
    {
        var sut = Maybe.None<Value>();
        sut.IfNone(_inspector.Object.MethodVoid);
        _inspector.Verify(i => i.MethodVoid());
    }

    [Fact]
    public void IfNone_call_void_when_maybeIsNone()
    {
        var sut = Maybe.Some(new Value(0));
        sut.IfNone(_inspector.Object.MethodVoid);
        _inspector.VerifyNoOtherCalls();
    }

    [Fact]
    public void IfSome_call_withArg_when_maybeIsSome()
    {
        var sut = Maybe.Some(new Value(0));
        sut.IfSome(_inspector.Object.MethodWithValueArg);
        _inspector.Verify(i => i.MethodWithValueArg(It.IsAny<Value>()));
    }

    [Fact]
    public void IfSome_doesntCall_withArg_when_maybeIsNone()
    {
        var sut = Maybe.None<Value>();
        sut.IfSome(_inspector.Object.MethodWithValueArg);
        _inspector.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task IfSome_call_async_when_maybeIsSome()
    {
        var sut = Maybe.Some(new Value(0));
        await sut.IfSome(_inspector.Object.MethodVoidAsync);
        _inspector.Verify(i => i.MethodVoidAsync());
    }

    [Fact]
    public async Task IfSome_doesntCall_async_when_maybeIsNone()
    {
        var sut = Maybe.None<Value>();
        await sut.IfSome(_inspector.Object.MethodVoidAsync);
        _inspector.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task IfNone_doesntCall_async_when_maybeIsSome()
    {
        var sut = Maybe.None<Value>();
        await sut.IfNone(_inspector.Object.MethodVoidAsync);
        _inspector.Verify(i => i.MethodVoidAsync());
    }

    [Fact]
    public async Task IfNone_call_void_async_maybeIsNone()
    {
        var sut = Maybe.Some(new Value(0));
        await sut.IfNone(_inspector.Object.MethodVoidAsync);
        _inspector.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task IfSome_call_asyncArg_when_maybeIsSome()
    {
        var sut = Maybe.Some(new Value(0));
        await sut.IfSome(_inspector.Object.MethodWithValueArgAsync);
        _inspector.Verify(i => i.MethodWithValueArgAsync(It.IsAny<Value>()));
    }

    [Fact]
    public async Task IfSome_doesntCall_asyncArg_when_maybeIsNone()
    {
        var sut = Maybe.None<Value>();
        await sut.IfSome(_inspector.Object.MethodWithValueArgAsync);
        _inspector.VerifyNoOtherCalls();
    }

    [Fact]
    public void Execute_works()
    {
        var sut = Maybe.None<Value>();
        sut.Execute(_inspector.Object.MethodWithMaybeArg);
        _inspector.Verify(i => i.MethodWithMaybeArg(It.IsAny<Maybe<Value>>()));
    }

    [Fact]
    public async Task Execute_async_works()
    {
        var sut = Maybe.None<Value>();
        await sut.Execute(_inspector.Object.MethodWithMaybeArgAsync);
        _inspector.Verify(i => i.MethodWithMaybeArgAsync(It.IsAny<Maybe<Value>>()));
    }
}