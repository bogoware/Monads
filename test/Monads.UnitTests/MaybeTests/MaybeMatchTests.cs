namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeMatchTests
{
	[Fact]
	public void Match_some_with_values()
	{
		var sut = Maybe.Some("Hello World");
		var actual = sut.Match("some", "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public void Match_none_with_values()
	{
		var sut = Maybe.None<string>();
		var actual = sut.Match("some", "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public void Match_some_with_funcAndValue()
	{
		var sut = Maybe.Some("Hello World");
		var actual = sut.Match(_ => "some", "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public void Match_none_with_funcAndValue()
	{
		var sut = Maybe.None<string>();
		var actual = sut.Match(_ => "some", "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public void Match_some_with_funcs()
	{
		var sut = Maybe.Some("Hello World");
		var actual = sut.Match(_ => "some", () => "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public void Match_none_with_funcs()
	{
		var sut = Maybe.None<string>();
		var actual = sut.Match(_ => "some", () => "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public async Task Match_some_with_leftAsync()
	{
		var sut = Maybe.Some("Hello World");
		var actual = await sut.Match(_ => Task.FromResult("some"), () => "none");
		actual.Should().Be("some");
	}
	
	[Fact]
	public async Task Match_none_with_leftAsync()
	{
		var sut = Maybe.None<string>();
		var actual = await sut.Match(_ => Task.FromResult("some"), () => "none");
		actual.Should().Be("none");
	}
	
	[Fact]
	public async Task Match_some_with_rightAsync()
	{
		var sut = Maybe.Some("Hello World");
		var actual = await sut.Match(_ => "some", () => Task.FromResult("none"));
		actual.Should().Be("some");
	}
	
	[Fact]
	public async Task Match_none_with_rightAsync()
	{
		var sut = Maybe.None<string>();
		var actual = await sut.Match(_ => "some", () => Task.FromResult("none"));
		actual.Should().Be("none");
	}
	
	[Fact]
	public async Task Match_some_with_bothAsync()
	{
		var sut = Maybe.Some("Hello World");
		var actual = await sut.Match(_ => Task.FromResult("some"), () => Task.FromResult("none"));
		actual.Should().Be("some");
	}
	
	[Fact]
	public async Task Match_none_with_bothAsync()
	{
		var sut = Maybe.None<string>();
		var actual = await sut.Match(_ => Task.FromResult("some"), () => Task.FromResult("none"));
		actual.Should().Be("none");
	}
}