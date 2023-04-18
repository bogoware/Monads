// ReSharper disable ArrangeObjectCreationWhenTypeEvident
namespace Bogoware.Monads.UnitTests;

public class OptionalAsyncTests
{
	private AnotherValue Function() => new AnotherValue(0);
	private AnotherValue Function(Value value) => new AnotherValue(value.Val);
	private Optional<AnotherValue> FlatFunctionSome() => Some(new AnotherValue(0));
	private Optional<AnotherValue> FlatFunctionSome(Value value) => Some(new AnotherValue(value.Val));
	private Optional<AnotherValue> FlatFunctionNone() => None<AnotherValue>();
	private Optional<AnotherValue> FlatFunctionNone(Value value) => None<AnotherValue>();
	
	private Task<AnotherValue> AsyncFunction() => Task.FromResult(new AnotherValue(0));
	private Task<AnotherValue> AsyncFunction(Value value) => Task.FromResult(new AnotherValue(value.Val));
	private Task<Optional<AnotherValue>> AsyncFlatFunctionSome() => Task.FromResult(Some(new AnotherValue(0)));
	private Task<Optional<AnotherValue>> AsyncFlatFunctionSome(Value value) => Task.FromResult(Some(new AnotherValue(value.Val)));
	private Task<Optional<AnotherValue>> AsyncFlatFunctionNone() => Task.FromResult(None<AnotherValue>());
	private Task<Optional<AnotherValue>> AsyncFlatFunctionNone(Value value) => Task.FromResult(None<AnotherValue>());
	
	[Fact]
	public async Task Async_map_asyncAction_withSome()
	{
		var sut = Some(new Value(0));
		var actual = await sut.Map(() => AsyncFunction());
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_map_asyncFunction_withSome()
	{
		var sut = Some(new Value(0));
		var actual = await sut.Map(v => AsyncFunction(v));
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_map_asyncAction_withNone()
	{
		var sut = None<Value>();
		var actual = await sut.Map(() => AsyncFunction());
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_map_asyncFunction_withNone()
	{
		var sut = None<Value>();
		var actual = await sut.Map(v => AsyncFunction(v));
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_flatMap_asyncAction_withSome()
	{
		var sut = Some(new Value(0));
		var actual = await sut.FlatMap(() => AsyncFlatFunctionSome());
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_flatMap_asyncFunction_withSome()
	{
		var sut = Some(new Value(0));
		var actual = await sut.FlatMap(v => AsyncFlatFunctionSome(v));
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_flatMap_asyncAction_withNone()
	{
		var sut = None<Value>();
		var actual = await sut.FlatMap(() => AsyncFlatFunctionNone());
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task Async_flatMap_asyncFunction_withNone()
	{
		var sut = None<Value>();
		var actual = await sut.FlatMap(v => AsyncFlatFunctionNone(v));
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_asyncAction_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.Map(() => AsyncFunction());
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_asyncFunction_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.Map(v => AsyncFunction(v));
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_asyncAction_withNone()
	{
		var sut = Task.FromResult(None<Value>());
		var actual = await sut.Map(() => AsyncFunction());
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_asyncFunction_withNone()
	{
		var sut = Task.FromResult(None<Value>());
		var actual = await sut.Map(v => AsyncFunction(v));
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_flatMap_asyncAction_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.FlatMap(() => AsyncFlatFunctionSome());
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_flatMap_asyncFunction_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.FlatMap(v => AsyncFlatFunctionSome(v));
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_Action_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.Map(() => Function());
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_Function_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.Map(v => Function(v));
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_Action_withNone()
	{
		var sut = Task.FromResult(None<Value>());
		var actual = await sut.Map(() => Function());
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_map_Function_withNone()
	{
		var sut = Task.FromResult(None<Value>());
		var actual = await sut.Map(v => Function(v));
		actual.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_flatMap_Action_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.FlatMap(() => FlatFunctionSome());
		actual.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public async Task AsyncExtensions_flatMap_Function_withSome()
	{
		var sut = Task.FromResult(Some(new Value(0)));
		var actual = await sut.FlatMap(v => FlatFunctionSome(v));
		actual.HasValue.Should().BeTrue();
	}
}