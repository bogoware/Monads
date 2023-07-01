// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Global
namespace Bogoware.Monads;

/// <summary>
/// The Unit type is used to represent the absence of a specific value
/// </summary>
public sealed class Unit: IEquatable<Unit>
{
	public static readonly Unit Instance = new();

	private Unit()
	{
	}

	public override bool Equals(object? obj) => obj is Unit;
	public bool Equals(Unit? other) => other is not null;

	public override int GetHashCode() => 0;

	public static bool operator ==(Unit? left, Unit? right) => true;

	public static bool operator !=(Unit? left, Unit? right) => false;

	public override string ToString() => nameof(Unit);
}