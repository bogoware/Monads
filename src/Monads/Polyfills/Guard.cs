using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Bogoware.Monads;

/// <summary>
/// Internal guard helper for null argument validation.
/// Provides consistent behavior across all target frameworks.
/// </summary>
internal static class Guard
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NETSTANDARD2_1
    public static void ThrowIfNull([NotNull] object? argument, string? paramName = null)
    {
        if (argument is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
#else
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
    }
#endif
}