namespace Bogoware.Monads.UnitTests;

public class MaybeCreationTests
{
	[Fact]
	public void Maybe_with_null_produces_a_None()
	{
		
		Maybe<string> sut = Maybe<string>((string)null!);
		sut.IsSome.Should().BeFalse();
		sut.Should().BeEquivalentTo(None<string>());
	}
	
	[Fact]
	public void Maybe_with_notnull_produces_a_Some()
	{
		
		Maybe<string> sut = Maybe<string>("Some");
		sut.IsSome.Should().BeTrue();
		sut.Should().BeEquivalentTo(Some("Some"));
	}
	
	[Fact]
	public void Some_with_notnull_is_successful()
	{
		
		Maybe<string> sut = Some("Some");
		sut.IsSome.Should().BeTrue();
		sut.Should().BeEquivalentTo(Some("Some"));
	}
	
	[Fact]
	public void Some_with_null_is_failure()
	{
		string value = null!;
		Action act = () => Some(value);

		act.Should().Throw<ArgumentNullException>();
	}
	
	[Fact]
	public void Some_with_None_is_failure()
	{
		var value = None<string>();
		Action act = () => Some(value);

		act.Should().Throw<MaybeNoneException>();
	}

	[Fact]
	public void Maybe_copy_constructor()
	{
		Maybe<Value> maybe = Maybe(new Value(0));
		Maybe<Value> maybe2 = Maybe(maybe);

		maybe.Equals(maybe2).Should().BeTrue();
	}
}