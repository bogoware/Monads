using Moq;

namespace Bogoware.Monads.UnitTests;

public class MaybeIfSomeIfNoneTapTests
{
	private readonly Mock<ICallInspector> _inspector = new();

	[Fact]
	public void IfSome_call_void_when_maybeIsSome()
	{
		var sut = Some(new Value(0));
		sut.IfSome(_inspector.Object.MethodVoid);
		_inspector.Verify(_ => _.MethodVoid());
	}

	[Fact]
	public void IfSome_doesntCall_void_when_maybeIsNone()
	{
		var sut = None<Value>();
		sut.IfSome(_inspector.Object.MethodVoid);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void IfNone_doesntCall_void_when_maybeIsSome()
	{
		var sut = None<Value>();
		sut.IfNone(_inspector.Object.MethodVoid);
		_inspector.Verify(_ => _.MethodVoid());
	}

	[Fact]
	public void IfNone_call_void_when_maybeIsNone()
	{
		var sut = Some(new Value(0));
		sut.IfNone(_inspector.Object.MethodVoid);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void IfSome_call_withArg_when_maybeIsSome()
	{
		var sut = Some(new Value(0));
		sut.IfSome(_inspector.Object.MethodWithValueArg);
		_inspector.Verify(_ => _.MethodWithValueArg(It.IsAny<Value>()));
	}

	[Fact]
	public void IfSome_doesntCall_withArg_when_maybeIsNone()
	{
		var sut = None<Value>();
		sut.IfSome(_inspector.Object.MethodWithValueArg);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task IfSome_call_async_when_maybeIsSome()
	{
		var sut = Some(new Value(0));
		await sut.IfSome(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(_ => _.MethodVoidAsync());
	}

	[Fact]
	public async Task IfSome_doesntCall_async_when_maybeIsNone()
	{
		var sut = None<Value>();
		await sut.IfSome(_inspector.Object.MethodVoidAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task IfNone_doesntCall_async_when_maybeIsSome()
	{
		var sut = None<Value>();
		await sut.IfNone(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(_ => _.MethodVoidAsync());
	}

	[Fact]
	public async Task IfNone_call_void_async_maybeIsNone()
	{
		var sut = Some(new Value(0));
		await sut.IfNone(_inspector.Object.MethodVoidAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task IfSome_call_asyncArg_when_maybeIsSome()
	{
		var sut = Some(new Value(0));
		await sut.IfSome(_inspector.Object.MethodWithValueArgAsync);
		_inspector.Verify(_ => _.MethodWithValueArgAsync(It.IsAny<Value>()));
	}

	[Fact]
	public async Task IfSome_doesntCall_asyncArg_when_maybeIsNone()
	{
		var sut = None<Value>();
		await sut.IfSome(_inspector.Object.MethodWithValueArgAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void Tap_works()
	{
		var sut = None<Value>();
		sut.Tap(_inspector.Object.MethodWithMaybeArg);
		_inspector.Verify(_ => _.MethodWithMaybeArg(It.IsAny<Maybe<Value>>()));
	}

	[Fact]
	public async Task Tap_async_works()
	{
		var sut = None<Value>();
		await sut.Tap(_inspector.Object.MethodWithMaybeArgAsync);
		_inspector.Verify(_ => _.MethodWithMaybeArgAsync(It.IsAny<Maybe<Value>>()));
	}
}