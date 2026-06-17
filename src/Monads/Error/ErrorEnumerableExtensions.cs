using System.Runtime.CompilerServices;
// ReSharper disable UnusedMember.Global

namespace Bogoware.Monads;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/> of <see cref="Error"/>.
/// </summary>
public static class ErrorEnumerableExtensions
{
    /// <summary>
    /// Converts an enumerable of errors into an <see cref="AggregateError"/>.
    /// </summary>
    /// <param name="errors">The errors to aggregate.</param>
    /// <returns>An <see cref="AggregateError"/> containing all the provided errors.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AggregateError ToAggregateError(this IEnumerable<Error> errors)
        => new(errors);

    /// <summary>
    /// Converts an enumerable of errors into an <see cref="AggregateError"/> with a custom message.
    /// </summary>
    /// <param name="errors">The errors to aggregate.</param>
    /// <param name="message">The custom message for the aggregate error.</param>
    /// <returns>An <see cref="AggregateError"/> containing all the provided errors with the specified message.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AggregateError ToAggregateError(this IEnumerable<Error> errors, string message)
        => new(message, errors);
}