using Moq;

namespace Bogoware.Monads.UnitTests;

public class OptionalIfSomeIfNoneTapTests
{
	private readonly Mock<ICallInspector> _inspector = new();

	[Fact]
	public void IfSome_call_void_when_optionalIsSome()
	{
		var sut = Some(new Value(0));
		sut.IfSome(_inspector.Object.MethodVoid);
		_inspector.Verify(_ => _.MethodVoid());
	}

	[Fact]
	public void IfSome_doesntCall_void_when_optionalIsNone()
	{
		var sut = None<Value>();
		sut.IfSome(_inspector.Object.MethodVoid);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void IfNone_doesntCall_void_when_optionalIsSome()
	{
		var sut = None<Value>();
		sut.IfNone(_inspector.Object.MethodVoid);
		_inspector.Verify(_ => _.MethodVoid());
	}

	[Fact]
	public void IfNone_call_void_when_optionalIsNone()
	{
		var sut = Some(new Value(0));
		sut.IfNone(_inspector.Object.MethodVoid);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void IfSome_call_withArg_when_optionalIsSome()
	{
		var sut = Some(new Value(0));
		sut.IfSome(_inspector.Object.MethodWithValueArg);
		_inspector.Verify(_ => _.MethodWithValueArg(It.IsAny<Value>()));
	}

	[Fact]
	public void IfSome_doesntCall_withArg_when_optionalIsNone()
	{
		var sut = None<Value>();
		sut.IfSome(_inspector.Object.MethodWithValueArg);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task IfSome_call_async_when_optionalIsSome()
	{
		var sut = Some(new Value(0));
		await sut.IfSome(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(_ => _.MethodVoidAsync());
	}

	[Fact]
	public async Task IfSome_doesntCall_async_when_optionalIsNone()
	{
		var sut = None<Value>();
		await sut.IfSome(_inspector.Object.MethodVoidAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task IfNone_doesntCall_async_when_optionalIsSome()
	{
		var sut = None<Value>();
		await sut.IfNone(_inspector.Object.MethodVoidAsync);
		_inspector.Verify(_ => _.MethodVoidAsync());
	}

	[Fact]
	public async Task IfNone_call_void_async_optionalIsNone()
	{
		var sut = Some(new Value(0));
		await sut.IfNone(_inspector.Object.MethodVoidAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task IfSome_call_asyncArg_when_optionalIsSome()
	{
		var sut = Some(new Value(0));
		await sut.IfSome(_inspector.Object.MethodWithValueArgAsync);
		_inspector.Verify(_ => _.MethodWithValueArgAsync(It.IsAny<Value>()));
	}

	[Fact]
	public async Task IfSome_doesntCall_asyncArg_when_optionalIsNone()
	{
		var sut = None<Value>();
		await sut.IfSome(_inspector.Object.MethodWithValueArgAsync);
		_inspector.VerifyNoOtherCalls();
	}

	[Fact]
	public void Tap_works()
	{
		var sut = None<Value>();
		sut.Tap(_inspector.Object.MethodWithOptionalArg);
		_inspector.Verify(_ => _.MethodWithOptionalArg(It.IsAny<Optional<Value>>()));
	}

	[Fact]
	public async Task Tap_async_works()
	{
		var sut = None<Value>();
		await sut.Tap(_inspector.Object.MethodWithOptionalArgAsync);
		_inspector.Verify(_ => _.MethodWithOptionalArgAsync(It.IsAny<Optional<Value>>()));
	}
}