namespace Bogoware.Monads.UnitTests;

public class MaybeExtensionsTests
{
	
	[Fact]
	public void NonEmptyEnumerable_produces_a_Some()
	{
		List<Value> values = new() { new(1), new(2) };
		var result = values.ToMaybe();
		result.IsSome.Should().BeTrue();
		result.GetValue(new Value(0)).Should().Be(new Value(1));
	}
	
	[Fact]
	public void EmptyEnumerable_produces_a_None()
	{
		// ReSharper disable once CollectionNeverUpdated.Local
		List<Value> values = new();
		var result = values.ToMaybe();
		result.IsNone.Should().BeTrue();
	}
	
	[Fact]
	public void Where_satisfied_produces_a_Some()
	{
		var value = new Value(0);
		var outcome = value.Where(v => v.Val == 0);
		outcome.Equals(Some(new Value(0))).Should().BeTrue();
	}
	
	[Fact]
	public void Where_notSatisfied_produces_a_None()
	{
		var value = new Value(0);
		var outcome = value.Where(v => v.Val == 1);
		outcome.Equals(None<Value>()).Should().BeTrue();
	}
	
	[Fact]
	public void WhereNot_satisfied_produces_a_Some()
	{
		var value = new Value(0);
		var outcome = value.WhereNot(v => v.Val == 1);
		outcome.Equals(Some(new Value(0))).Should().BeTrue();
	}
	
	[Fact]
	public void WhereNot_notSatisfied_produces_a_None()
	{
		var value = new Value(0);
		var outcome = value.WhereNot(v => v.Val == 0);
		outcome.Equals(None<Value>()).Should().BeTrue();
	}
}