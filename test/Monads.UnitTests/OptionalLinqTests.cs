using System.Net.Security;

namespace Bogoware.Monads.UnitTests;

public class OptionalLinqTests
{
	[Fact]
	public void Select_value_from_Some()
	{
		var sut = Some(new Value(1));
		var values =
			from v in sut
			select v.Val;

		values.Should().BeEquivalentTo(new List<int>() { 1 });
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

		values.Should().BeEquivalentTo(new List<int>() { 1 });
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

		values.Should().BeEquivalentTo(new List<int>() { 1 });
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
}