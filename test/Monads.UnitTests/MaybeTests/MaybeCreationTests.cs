// ReSharper disable SuggestVarOrType_Elsewhere
namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeCreationTests
{
	[Fact]
	public void Maybe_with_null_produces_a_None()
	{
		
		Maybe<string> sut = Maybe.From((string)null!);
		sut.IsSome.Should().BeFalse();
		sut.Should().BeEquivalentTo(Maybe.None<string>());
	}
	
	[Fact]
	public void Maybe_with_notnull_produces_a_Some()
	{
		
		var sut = Maybe.From("Some");
		sut.IsSome.Should().BeTrue();
		sut.Should().BeEquivalentTo(Maybe.Some("Some"));
	}
	
	[Fact]
	public void Some_with_notnull_is_successful()
	{
		
		Maybe<string> sut = Maybe.Some("Some");
		sut.IsSome.Should().BeTrue();
		sut.Should().BeEquivalentTo(Maybe.Some("Some"));
	}
	
	[Fact]
	public void Some_with_null_is_failure()
	{
		string value = null!;
		Action act = () => Maybe.Some(value);

		act.Should().Throw<ArgumentNullException>();
	}
	
	[Fact]
	public void Some_with_None_is_failure()
	{
		var value = Maybe.None<string>();
		Action act = () => Maybe.Some(value);

		act.Should().Throw<MaybeNoneException>();
	}

	[Fact]
	public void Maybe_copy_constructor()
	{
		var maybe = Maybe.From(new Value(0));
		var maybe2 = Maybe.From(maybe);
		
		maybe.Equals(maybe2).Should().BeTrue();
	}
}