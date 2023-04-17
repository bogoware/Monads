namespace Bogoware.Monads.UnitTests;

public class OptionalMapTests
{
	[Fact]
	public void Some_mapsTo_SomeValue()
	{
		var value = Some("0");
		Optional<string> newValue = value.Map("Hello");
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void Some_mapsTo_SomeValue_by_action()
	{
		var value = Some("0");
		Optional<string> newValue = value.Map(() => "Hello");
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void Some_mapsTo_SomeValue_by_function()
	{
		var value = Some("0");
		Optional<string> newValue = value.Map(num => $"Hello {num}");
		newValue.HasValue.Should().BeTrue();
	}
	
	[Fact]
	public void None_mapsTo_None_by_Value()
	{
		var value = None<string>();
		value.IsNone.Should().BeTrue();
		Optional<string> newValue = value.Map("Hello");
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_mapsTo_None_by_action()
	{
		var value = None<string>();
		Optional<string> newValue = value.Map(() => "Hello");
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_mapsTo_None_by_function()
	{
		var value = None<string>();
		Optional<string> newValue = value.Map(num => $"Hello {num}");
		newValue.IsNone.Should().BeTrue();
	}
}