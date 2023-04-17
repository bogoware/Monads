﻿namespace Bogoware.Monads;

public sealed class Unit
{
	public static readonly Unit Instance = new();

	private Unit()
	{
	}

	public override bool Equals(object? obj) => obj is Unit;
	private bool Equals(Unit other) => true;

	public override int GetHashCode() => 0;

	public static bool operator ==(Unit? left, Unit? right) => true;

	public static bool operator !=(Unit? left, Unit? right) => false;

	public override string ToString() => nameof(Unit);
}