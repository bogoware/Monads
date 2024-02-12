namespace Bogoware.Monads;

/// <summary>
/// Represents a collection of <see cref="Result{TValue}"/>s.
/// </summary>
internal class ResultCollection<TValue>
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error GetErrorOrThrow() => _error ?? throw new ResultSuccessException();

	private readonly List<Result<TValue>> _results;
	private readonly AggregateError? _error;

	internal ResultCollection(IEnumerable<Result<TValue>> results)
	{
		_results = [..results];
		IsSuccess = _results.Count == 0
		            || _results.All(r => r.IsSuccess);
		
		if (IsSuccess) return;
		
		var errors = _results.Where(r => r.IsFailure).Select(r => r.GetErrorOrThrow());
		_error = new (errors);
	}

	internal Result<IEnumerable<TValue?>> ToResult()
	{
		if (IsFailure) return Result.Failure<IEnumerable<TValue?>>(_error!);
		if (_results.Count == 0) return Result.Success(Enumerable.Empty<TValue?>());
		var values = _results.Select(r => r.Value);
		return Result.Success(values);
	}
}