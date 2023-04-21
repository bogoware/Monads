// ReSharper disable SuggestVarOrType_Elsewhere

namespace Bogoware.Monads.UnitTests;

public class EnumerableMaybeExtensionsTests
{
	private static readonly List<Maybe<Value>> _allMaybeSome = new()
	{
		Some(new Value(0)),
		Some(new Value(1)),
		Some(new Value(2)),
	};

	private static readonly List<IMaybe> _allIMaybeSome = new()
	{
		Some(new Value(0)),
		Some(new Value(1)),
		Some(new Value(2)),
	};

	private static readonly List<Maybe<Value>> _allMaybeNone = new()
	{
		Maybe<Value>.None,
		Maybe<Value>.None,
		Maybe<Value>.None,
	};

	private static readonly List<IMaybe> _allIMaybeNone = new()
	{
		Maybe<Value>.None,
		Maybe<Value>.None,
		Maybe<Value>.None,
	};

	private static readonly List<Maybe<Value>> _maybeMixed = new()
	{
		Some(new Value(0)),
		Maybe<Value>.None,
		Some(new Value(2)),
	};

	private static readonly List<IMaybe> _maybeIMixed = new()
	{
		Some(new Value(0)),
		Maybe<Value>.None,
		Some(new Value(2)),
	};

	[Fact]
	public void IsAllSome_returns_true()
	{
		_allMaybeSome.IsAllSome().Should().BeTrue();
		_allIMaybeSome.IsAllSome().Should().BeTrue();
	}

	[Fact]
	public void IsAllSome_returns_false()
	{
		_maybeMixed.IsAllSome().Should().BeFalse();
		_maybeIMixed.IsAllSome().Should().BeFalse();
	}

	[Fact]
	public void IsAllNone_returns_true()
	{
		_allMaybeNone.IsAllNone().Should().BeTrue();
		_allIMaybeNone.IsAllNone().Should().BeTrue();
	}

	[Fact]
	public void IsAllNone_returns_false()
	{
		_maybeMixed.IsAllNone().Should().BeFalse();
		_maybeIMixed.IsAllNone().Should().BeFalse();
	}

	[Fact]
	public void IsAnySome_returns_true()
	{
		_maybeMixed.IsAnySome().Should().BeTrue();
		_maybeIMixed.IsAnySome().Should().BeTrue();
		_allMaybeSome.IsAnySome().Should().BeTrue();
		_allIMaybeSome.IsAnySome().Should().BeTrue();
	}

	[Fact]
	public void IsAnySome_returns_false()
	{
		_allMaybeNone.IsAnySome().Should().BeFalse();
		_allIMaybeNone.IsAnySome().Should().BeFalse();
	}

	[Fact]
	public void IsAnyNone_returns_true()
	{
		_maybeMixed.IsAnyNone().Should().BeTrue();
		_maybeIMixed.IsAnyNone().Should().BeTrue();
		_allMaybeNone.IsAnyNone().Should().BeTrue();
		_allIMaybeNone.IsAnyNone().Should().BeTrue();
	}

	[Fact]
	public void IsAnyNone_returns_false()
	{
		_allMaybeSome.IsAnyNone().Should().BeFalse();
		_allIMaybeSome.IsAnyNone().Should().BeFalse();
	}

	[Fact]
	public void SelectValues_discards_Nones()
	{
		_maybeMixed.Should().HaveCount(3);
		_maybeMixed.SelectValues().Should().HaveCount(2);
	}

	[Fact]
	public void Map_remap_values_to_new_Some()
	{
		IEnumerable<Maybe<AnotherValue>> actual = _maybeMixed.Map(v => new AnotherValue(v.Val));
		actual.Should().HaveCount(2);
	}

	[Fact]
	public void Bind_remap_values_to_new_Some()
	{
		IEnumerable<Maybe<AnotherValue>> actual = _maybeMixed.Bind(v => Some(new AnotherValue(v.Val)));
		actual.Should().HaveCount(2);
	}

	[Fact]
	public void Where_works()
	{
		IEnumerable<Maybe<Value>> maybes = new List<Maybe<Value>>()
		{
			new Value(0),
			new Value(1),
			new Value(2),
			new Value(3)
		};

		IEnumerable<Maybe<Value>> even = maybes.Where(v => v.Val % 2 == 0);
		even.Should().HaveCount(2);
	}
	
	[Fact]
	public void WhereNot_works()
	{
		IEnumerable<Maybe<Value>> maybes = new List<Maybe<Value>>()
		{
			new Value(0),
			new Value(1),
			new Value(2),
			new Value(3),
			new Value(5)
		};

		IEnumerable<Maybe<Value>> even = maybes.WhereNot(v => v.Val % 2 == 0);
		even.Should().HaveCount(3);
	}
}