using System.Collections;
using System.Reflection.Metadata.Ecma335;

namespace Bogoware.Monads.UnitTests;

public class MaybeLinqTests
{
	[Fact]
	public void Select_value_from_Some()
	{
		var sut = Some(new Value(1));
		var values =
			from v in sut
			select v.Val;

		values.Should().BeEquivalentTo(new List<int> { 1 });
	}
	
	[Fact]
	public void Select_value_from_None()
	{
		var sut = None<Value>();
		var values =
			from v in sut
			select v.Val;

		values.Should().BeEmpty();
	}
	
	[Fact]
	public void Where_satisfied_returns_a_nonEmpty_enumerable()
	{
		var sut = Some(new Value(1));
		var values =
			from v in sut
			where v.Val == 1
			select v.Val;

		values.Should().BeEquivalentTo(new List<int> { 1 });
	}
	
	[Fact]
	public void Where_notSatisfied_returns_an_Empty_enumerable()
	{
		var sut = Some(new Value(1));
		var values =
			from v in sut
			where v.Val == 0
			select v.Val;

		values.Should().BeEmpty();
	}
	
	[Fact]
	public void WhereNot_satisfied_returns_a_nonEmpty_enumerable()
	{
		var sut = Some(new Value(1));
		var values =
			from v in sut
			where v.Val != 0
			select v.Val;

		values.Should().BeEquivalentTo(new List<int> { 1 });
	}
	
	[Fact]
	public void WhereNot_notSatisfied_returns_an_Empty_enumerable()
	{
		var sut = Some(new Value(1));
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
			Some(new Value(1)),
			Some(new Value(2)),
			Maybe<Value>.None,
			Some(new Value(3)),
			Some(new Value(4)),
		};
		
		// style 1: filter over monads
		var values =
			from maybe in maybes
			where maybe.Satisfy(_ => _.Val % 2 == 0)
			from v in maybe
			select v.Val;

		values.Sum().Should().Be(6);
		
		// style 1: filter in pure linq style
		var values2 =
			from maybe in maybes
			from v in maybe
			where v.Val % 2 == 0
			select v.Val;

		values2.Sum().Should().Be(6);
	}

	[Fact]
	public void IEnumerable_coverage()
	{
		var sut = (IEnumerable) Some(new Value(1));
		sut.GetEnumerator().Should().NotBeNull();
	}
}