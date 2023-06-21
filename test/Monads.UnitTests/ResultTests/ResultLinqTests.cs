namespace Bogoware.Monads.UnitTests.ResultTests;

public class ResultLinqTests
{
	[Fact]
	public void Select_value_from_Success()
	{
		var success1 = Success(new Value(1));
		var success2 = Success(new Value(1));
		var values =
			from value1 in success1
			from value2 in success2
			select value1.Val + value2.Val;

		values.Should().BeEquivalentTo(new List<int> { 2 });
	}

	[Fact]
	public void Select_value_from_Failure()
	{
		var success = Success(new Value(1));
		var failure = Failure<Value>("something wet wrong");
		var values =
			from value1 in success
			from value2 in failure
			select value1.Val + value2.Val;
		
		values.Should().BeEmpty();
	}
	
	[Fact]
	public void Aggregate_values()
	{
		var results = new List<Result<Value, LogicError>>
		{
			Success(new Value(1)),
			Success(new Value(2)),
			Failure<Value>("something went wrong"),
			Success(new Value(3)),
			Success(new Value(4)),
		};
		
		// style 1: filter over monads
		var values =
			from result in results
			where result.Satisfy(_ => _.Val % 2 == 0)
			from v in result
			select v.Val;

		values.Sum().Should().Be(6);
		
		// style 1: filter in pure linq style
		var values2 =
			from result in results
			from v in result
			where v.Val % 2 == 0
			select v.Val;

		values2.Sum().Should().Be(6);
	}
}