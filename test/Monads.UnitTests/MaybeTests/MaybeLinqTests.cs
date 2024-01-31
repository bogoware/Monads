using System.Collections;

namespace Bogoware.Monads.UnitTests.MaybeTests;

public class MaybeLinqTests
{
	[Fact]
	public void Select_value_from_Some()
	{
		var sut = Maybe.Some(new Value(1));
		var values =
			from v in sut
			select v.Val;

		values.Should().BeEquivalentTo(new List<int> { 1 });
	}
	
	[Fact]
	public void Select_value_from_None()
	{
		var sut = Maybe.None<Value>();
		var values =
			from v in sut
			select v.Val;

		values.Should().BeEmpty();
	}
	
	[Fact]
	public void Where_satisfied_returns_a_nonEmpty_enumerable()
	{
		var sut = Maybe.Some(new Value(1));
		var values =
			from v in sut
			where v.Val == 1
			select v.Val;

		values.Should().BeEquivalentTo(new List<int> { 1 });
	}
	
	[Fact]
	public void Where_notSatisfied_returns_an_Empty_enumerable()
	{
		var sut = Maybe.Some(new Value(1));
		var values =
			from v in sut
			where v.Val == 0
			select v.Val;

		values.Should().BeEmpty();
	}
	
	[Fact]
	public void WhereNot_satisfied_returns_a_nonEmpty_enumerable()
	{
		var sut = Maybe.Some(new Value(1));
		var values =
			from v in sut
			where v.Val != 0
			select v.Val;

		values.Should().BeEquivalentTo(new List<int> { 1 });
	}
	
	[Fact]
	public void WhereNot_notSatisfied_returns_an_Empty_enumerable()
	{
		var sut = Maybe.Some(new Value(1));
		var values =
			from v in sut
			where v.Val != 1
			select v.Val;

		values.Should().BeEmpty();
	}
	
	[Fact]
	public void Aggregate_values()
	{
		var maybes = new List<Maybe<Value>>
		{
			Maybe.Some(new Value(1)),
			Maybe.Some(new Value(2)),
			Maybe<Value>.None,
			Maybe.Some(new Value(3)),
			Maybe.Some(new Value(4)),
		};
		
		// Pure linq style
		var values1 =
			from maybe in maybes
			from value in maybe
			where value.Val % 2 == 0
			select value.Val;
		values1.Sum().Should().Be(6);
		
		// filter in pure linq style
		var values2 =
			from maybe in maybes.Where(v => v.Val % 2 == 0)
			from v in maybe
			select v.Val;
		values2.Sum().Should().Be(6);
		
		// filter over monads
		var values3 =
			from maybe in maybes
			where maybe.Satisfy(v => v.Val % 2 == 0)
			from v in maybe
			select v.Val;
		values3.Sum().Should().Be(6);
		
		
	}

	[Fact]
	public void IEnumerable_coverage()
	{
		var sut = (IEnumerable) Maybe.Some(new Value(1));
		sut.GetEnumerator().Should().NotBeNull();
	}
}