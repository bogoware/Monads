// ReSharper disable SuggestVarOrType_Elsewhere

namespace Bogoware.Monads.UnitTests.ResultTests;

public class EnumerableResultExtensionsTests
{
	private static readonly List<Result<Value>> _allResultSuccess = new()
	{
		Result.Success(new Value(0)),
		Result.Success(new Value(1)),
		Result.Success(new Value(2))
	};

	private static readonly List<IResult> _allIResultSuccess = new()
	{
		Result.Success(new Value(0)),
		Result.Success(new Value(1)),
		Result.Success(new Value(2))
	};

	private static readonly List<Result<Value>> _allResultFailure = new()
	{
		Result.Failure<Value>("Error 1"),
		Result.Failure<Value>("Error 2"),
		Result.Failure<Value>("Error 3")
	};

	private static readonly List<IResult> _allIResultFailure = new()
	{
		Result.Failure<Value>("Error 1"),
		Result.Failure<Value>("Error 2"),
		Result.Failure<Value>("Error 3")
	};

	private static readonly List<Result<Value>> _resultMixed = new()
	{
		Result.Success(new Value(0)),
		Result.Failure<Value>("Error"),
		Result.Success(new Value(2)),
	};

	private static readonly List<IResult> _resultIMixed = new()
	{
		Result.Success(new Value(0)),
		Result.Failure<Value>("Error"),
		Result.Success(new Value(2)),
	};

	[Fact]
	public void AllSuccess_returns_true()
	{
		_allResultSuccess.AllSuccess().Should().BeTrue();
		_allIResultSuccess.AllSuccess().Should().BeTrue();
	}

	[Fact]
	public void AllSuccess_returns_false()
	{
		_resultMixed.AllSuccess().Should().BeFalse();
		_resultIMixed.AllSuccess().Should().BeFalse();
	}

	[Fact]
	public void AllFailure_returns_true()
	{
		_allResultFailure.AllFailure().Should().BeTrue();
		_allIResultFailure.AllFailure().Should().BeTrue();
	}

	[Fact]
	public void AllFailure_returns_false()
	{
		_resultMixed.AllFailure().Should().BeFalse();
		_resultIMixed.AllFailure().Should().BeFalse();
	}

	[Fact]
	public void AnySuccess_returns_true()
	{
		_resultMixed.AnySuccess().Should().BeTrue();
		_resultIMixed.AnySuccess().Should().BeTrue();
		_allResultSuccess.AnySuccess().Should().BeTrue();
		_allIResultSuccess.AnySuccess().Should().BeTrue();
	}

	[Fact]
	public void AnySuccess_returns_false()
	{
		_allResultFailure.AnySuccess().Should().BeFalse();
		_allIResultFailure.AnySuccess().Should().BeFalse();
	}

	[Fact]
	public void AnyFailure_returns_true()
	{
		_resultMixed.AnyFailure().Should().BeTrue();
		_resultIMixed.AnyFailure().Should().BeTrue();
		_allResultFailure.AnyFailure().Should().BeTrue();
		_allIResultFailure.AnyFailure().Should().BeTrue();
	}

	[Fact]
	public void AnyFailure_returns_false()
	{
		_allResultSuccess.AnyFailure().Should().BeFalse();
		_allIResultSuccess.AnyFailure().Should().BeFalse();
	}

	[Fact]
	public void SelectValues_discards_Nones()
	{
		_resultMixed.Should().HaveCount(3);
		_resultMixed.SelectValues().Should().HaveCount(2);
	}

	[Fact]
	public void Map_remap_values_to_new_Some()
	{
		IEnumerable<Result<AnotherValue>> actual = _resultMixed.Map(v => new AnotherValue(v.Val));
		actual.Should().HaveCount(2);
	}

	[Fact]
	public void Bind_remap_values_to_new_Some()
	{
		IEnumerable<Result<AnotherValue>> actual = _resultMixed.Bind(v => Result.Success(new AnotherValue(v.Val)));
		actual.Should().HaveCount(2);
	}

	[Fact]
	public void Where_works()
	{
		IEnumerable<Result<Value>> results = new List<Result<Value>>
		{
			Result.Success(new Value(0)),
			Result.Success(new Value(1)),
			Result.Success(new Value(2)),
			Result.Success(new Value(3))
		};

		IEnumerable<Result<Value>> even = results.Where(v => v.Val % 2 == 0);
		even.Should().HaveCount(2);
	}
	
	[Fact]
	public void WhereNot_works()
	{
		IEnumerable<Result<Value>> results = new List<Result<Value>>
		{
			Result.Success(new Value(0)),
			Result.Success(new Value(1)),
			Result.Success(new Value(2)),
			Result.Success(new Value(3)),
			Result.Success(new Value(5))
		};

		IEnumerable<Result<Value>> even = results.WhereNot(v => v.Val % 2 == 0);
		even.Should().HaveCount(3);
	}
}