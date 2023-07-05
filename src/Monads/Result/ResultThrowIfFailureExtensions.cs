using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

public static class ResultThrowIfFailureExtensions
{
	/// <summary>
	/// Throws a <see cref="ResultFailedException"/> if the <see cref="Result{TValue}"/> is a <c>Failure</c>.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfFailure<TValue>(this Result<TValue> result)
	{
		if (result.IsFailure) throw new ResultFailedException(result.Error!);
	}
	
	/// <inheritdoc cref="ThrowIfFailure{TValue}(Bogoware.Monads.Result{TValue})"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static async Task ThrowIfFailure<TValue>(this Task<Result<TValue>> resultTask)
	{
		var result = await resultTask;
		if (result.IsFailure) throw new ResultFailedException(result.Error!);
	}
}