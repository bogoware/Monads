namespace Bogoware.Monads.UnitTests;

public class OptionalFlatMapTests
{
	[Fact]
	public void Some_flatMapsTo_SomeValue_by_action()
	{
		var value = Some(new Value(0));
		var newValue = value.FlatMap(() => Some(new AnotherValue(0)));
		newValue.GetType().Should().Be<Optional<AnotherValue>>();
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void Some_slatMapsTo_SomeValue_by_function()
	{
		var value = Some(new Value(0));
		var newValue = value.FlatMap(val => Some(new AnotherValue(val.Val + 1)));
		newValue.GetType().Should().Be<Optional<AnotherValue>>();
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void Some_flatMapsTo_None_by_action()
	{
		var value = Some(new Value(0));
		var newValue = value.FlatMap(None<AnotherValue>);
		newValue.GetType().Should().Be<Optional<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void Some_flatMapsTo_None_by_function()
	{
		var value = Some(new Value(0));
		var newValue = value.FlatMap(_ => None<AnotherValue>());
		newValue.GetType().Should().Be<Optional<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_flatMapsTo_None_by_action()
	{
		var value = None<Value>();
		var newValue = value.FlatMap(() => Some(new AnotherValue(0)));
		newValue.GetType().Should().Be<Optional<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_slatMapsTo_None_by_function()
	{
		var value = None<Value>();
		var newValue = value.FlatMap(val => Some(new AnotherValue(val.Val + 1)));
		newValue.GetType().Should().Be<Optional<AnotherValue>>();
		newValue.IsNone.Should().BeTrue();
	}
}