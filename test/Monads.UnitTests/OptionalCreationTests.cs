namespace Bogoware.Monads.UnitTests;

public class OptionalCreationTests
{
	[Fact]
	public void Optional_with_null_produces_a_None()
	{
		
		Optional<string> sut = Optional<string>((string)null!);
		sut.HasValue.Should().BeFalse();
		sut.Should().BeEquivalentTo(None<string>());
	}
	
	[Fact]
	public void Optional_with_notnull_produces_a_Some()
	{
		
		Optional<string> sut = Optional<string>("Some");
		sut.HasValue.Should().BeTrue();
		sut.Should().BeEquivalentTo(Some("Some"));
	}
	
	[Fact]
	public void Some_with_notnull_is_successful()
	{
		
		Optional<string> sut = Some("Some");
		sut.HasValue.Should().BeTrue();
		sut.Should().BeEquivalentTo(Some("Some"));
	}
	
	[Fact]
	public void Some_with_null_is_failure()
	{
		string value = null!;
		Action act = () => Some(value);

		act.Should().Throw<OptionalValueMissingException>();
	}
}