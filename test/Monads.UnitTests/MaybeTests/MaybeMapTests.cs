// ReSharper disable SuggestVarOrType_Elsewhere
namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeMapTests
{
	[Fact]
	public void Some_mapsTo_SomeValue()
	{
		var value = Maybe.Some("0");
		Maybe<string> newValue = value.Map("Hello");
		newValue.IsSome.Should().BeTrue();
	}
	
	[Fact]
	public void Some_mapsTo_SomeValue_by_action()
	{
		var value = Maybe.Some("0");
		Maybe<string> newValue = value.Map(() => "Hello");
		newValue.IsSome.Should().BeTrue();
	}
	
	[Fact]
	public void Some_mapsTo_SomeValue_by_function()
	{
		var value = Maybe.Some("0");
		Maybe<string> newValue = value.Map(num => $"Hello {num}");
		newValue.IsSome.Should().BeTrue();
	}
	
	[Fact]
	public void None_mapsTo_None_by_Value()
	{
		var value = Maybe.None<string>();
		value.IsNone.Should().BeTrue();
		Maybe<string> newValue = value.Map("Hello");
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_mapsTo_None_by_action()
	{
		var value = Maybe.None<string>();
		Maybe<string> newValue = value.Map(() => "Hello");
		newValue.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void None_mapsTo_None_by_function()
	{
		var value = Maybe.None<string>();
		Maybe<string> newValue = value.Map(num => $"Hello {num}");
		newValue.IsNone.Should().BeTrue();
	}
}