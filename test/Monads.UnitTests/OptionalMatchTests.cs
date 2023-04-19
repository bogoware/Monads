namespace Bogoware.Monads.UnitTests;

public class OptionalMatchTests
{
	[Fact]
	public void Match_some_with_values()
	{
		var sut = Some("Hello World");
		var actual = sut.Match("some", "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public void Match_none_with_values()
	{
		var sut = None<string>();
		var actual = sut.Match("some", "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public void Match_some_with_funcAndValue()
	{
		var sut = Some("Hello World");
		var actual = sut.Match(_ => "some", "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public void Match_none_with_funcAndValue()
	{
		var sut = None<string>();
		var actual = sut.Match(_ => "some", "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public void Match_some_with_funcs()
	{
		var sut = Some("Hello World");
		var actual = sut.Match(_ => "some", () => "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public void Match_none_with_funcs()
	{
		var sut = None<string>();
		var actual = sut.Match(_ => "some", () => "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public async Task Match_some_with_leftAsync()
	{
		var sut = Some("Hello World");
		var actual = await sut.Match(_ => Task.FromResult("some"), () => "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public async Task Match_none_with_leftAsync()
	{
		var sut = None<string>();
		var actual = await sut.Match(_ => Task.FromResult("some"), () => "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public async Task Match_some_with_rightAsync()
	{
		var sut = Some("Hello World");
		var actual = await sut.Match(_ => "some", () => Task.FromResult("none"));
		actual.Should().Be("some");
	}
	
	[Fact]
	public async Task Match_none_with_rightAsync()
	{
		var sut = None<string>();
		var actual = await sut.Match(_ => "some", () => Task.FromResult("none"));
		actual.Should().Be("none");
	}
	
	[Fact]
	public async Task Match_some_with_bothAsync()
	{
		var sut = Some("Hello World");
		var actual = await sut.Match(_ => Task.FromResult("some"), () => Task.FromResult("none"));
		actual.Should().Be("some");
	}
	
	[Fact]
	public async Task Match_none_with_bothAsync()
	{
		var sut = None<string>();
		var actual = await sut.Match(_ => Task.FromResult("some"), () => Task.FromResult("none"));
		actual.Should().Be("none");
	}
}