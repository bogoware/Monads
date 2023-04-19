namespace Bogoware.Monads.UnitTests;

public class MaybeCreationTests
{
	[Fact]
	public void Maybe_with_null_produces_a_None()
	{
		
		Maybe<string> sut = Maybe<string>((string)null!);
		sut.HasValue.Should().BeFalse();
		sut.Should().BeEquivalentTo(None<string>());
	}
	
	[Fact]
	public void Maybe_with_notnull_produces_a_Some()
	{
		
		Maybe<string> sut = Maybe<string>("Some");
		sut.HasValue.Should().BeTrue();
		sut.Should().BeEquivalentTo(Some("Some"));
	}
	
	[Fact]
	public void Some_with_notnull_is_successful()
	{
		
		Maybe<string> sut = Some("Some");
		sut.HasValue.Should().BeTrue();
		sut.Should().BeEquivalentTo(Some("Some"));
	}
	
	[Fact]
	public void Some_with_null_is_failure()
	{
		string value = null!;
		Action act = () => Some(value);

		act.Should().Throw<MaybeValueMissingException>();
	}
}