using System;
using System.Numerics;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Represents a two-dimensional Position using distance units for both axes.
/// Allows operations between Positions and scalar values based on mathematical relationships.
/// </summary>
/// <param name="x">The X coordinate of the Position.</param>
/// <param name="y">The Y coordinate of the Position.</param>
public readonly struct Position(distance x, distance y) :
    IEquatable<Position>,
    IFormattable,
    IAdditionOperators<Position, Position, Position>,
    ISubtractionOperators<Position, Position, Position>,
    IMultiplyOperators<Position, double, Position>,
    IDivisionOperators<Position, double, Position>
{
    /// <summary>
    /// Gets the X coordinate of this Position.
    /// </summary>
    public distance X { get; } = x;

    /// <summary>
    /// Gets the Y coordinate of this Position.
    /// </summary>
    public distance Y { get; } = y;

    /// <summary>
    /// Returns the Position formatted as an X and Y coordinate pair.
    /// </summary>
    /// <returns>Correctly formatted string representation of the Position.</returns>
    public override string ToString() =>
        $"({X}, {Y})";

    /// <summary>
    /// Determines whether two Positions have the same coordinates.
    /// </summary>
    /// <param name="other">The Position to compare.</param>
    /// <returns>true if both Positions contain equal X and Y coordinates. False otherwise.</returns>
    public bool Equals(Position other) =>
        X.Equals(other.X) &&
        Y.Equals(other.Y);

    /// <summary>
    /// Compares two objects. True only if both are Position and contain equal coordinates.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both are the same type and contain equal coordinates. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is Position other && Equals(other);

    /// <summary>
    /// Hash code generated from the X and Y coordinates.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        HashCode.Combine(X, Y);

    /// <summary>
    /// Adds two Positions together coordinate by coordinate.
    /// </summary>
    /// <param name="left">Left Position.</param>
    /// <param name="right">Right Position.</param>
    /// <returns>The resulting Position.</returns>
    public static Position operator +(Position left, Position right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one Position from another coordinate by coordinate.
    /// </summary>
    /// <param name="left">Left Position.</param>
    /// <param name="right">Right Position.</param>
    /// <returns>The difference between both Positions.</returns>
    public static Position operator -(Position left, Position right) =>
        new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies a Position by a scalar value.
    /// </summary>
    /// <param name="pos">Input Position.</param>
    /// <param name="scalar">Multiplier value.</param>
    /// <returns>The scaled Position.</returns>
    public static Position operator *(Position pos, double scalar) =>
        new(pos.X * scalar, pos.Y * scalar);

    /// <summary>
    /// Multiplies a Position by a scalar value.
    /// </summary>
    /// <param name="scalar">Multiplier value.</param>
    /// <param name="pos">Input Position.</param>
    /// <returns>The scaled Position.</returns>
    public static Position operator *(double scalar, Position pos) =>
        new(pos.X * scalar, pos.Y * scalar);

    /// <summary>
    /// Divides both vectors of a Position by a scalar value.
    /// </summary>
    /// <param name="pos">Input Position.</param>
    /// <param name="scalar">Divisor value.</param>
    /// <returns>The scaled Position.</returns>
    public static Position operator /(Position pos, double scalar) =>
        new(pos.X / scalar, pos.Y / scalar);

    /// <summary>
    /// Determines whether two Positions are equal.
    /// </summary>
    /// <param name="left">The first Position to compare.</param>
    /// <param name="right">The second Position to compare.</param>
    /// <returns><c>true</c> if both Positions have the same coordinates; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Position left, Position right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two Positions are not equal.
    /// </summary>
    /// <param name="left">The first Position to compare.</param>
    /// <param name="right">The second Position to compare.</param>
    /// <returns><c>true</c> if Positions have different coordinates; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Position left, Position right) =>
        !left.Equals(right);

    /// <summary>
    /// Applies modulo operation to both coordinates of a Position.
    /// </summary>
    /// <param name="left">The Position value.</param>
    /// <param name="right">The Position bounds.</param>
    /// <returns>The wrapped Position within the given bounds.</returns>
    public static Position operator %(Position left, Position right) =>
        new(left.X % right.X, left.Y % right.Y);

    /// <summary>
    /// Calculates the straight-line distance between two Positions.
    /// </summary>
    /// <param name="other">The Position to calculate distance to.</param>
    /// <returns>The distance between this Position and the other Position.</returns>
    public distance DistanceTo(Position other)
    {
        distance dx = X - other.X;
        distance dy = Y - other.Y;

        return new distance(
            Math.Sqrt(
                dx.Value * dx.Value +
                dy.Value * dy.Value));
    }

    /// <summary>
    /// Converts this Position to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted Position string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"({X.ToString(format, provider)}, {Y.ToString(format, provider)})";
}