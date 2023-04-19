namespace Bogoware.Monads.UnitTests;

public class OptionalWithDefaultTests
{
	[Fact]
	public void WithDefault_value()
	{
		var none = None<string>();
		var some = none.WithDefault("Some");
		some.HasValue.Should().BeTrue();
		some.GetValue("").Should().Be("Some");
	}
	
	[Fact]
	public void WithDefault_function()
	{
		var none = None<string>();
		var some = none.WithDefault(() => "Some");
		some.HasValue.Should().BeTrue();
		some.GetValue("").Should().Be("Some");
	}
	
	[Fact]
	public async Task WithDefault_asyncFunction()
	{
		var none = None<string>();
		var some = await none.WithDefault(() => Task.FromResult("Some"));
		some.HasValue.Should().BeTrue();
		some.GetValue("").Should().Be("Some");
	}
}