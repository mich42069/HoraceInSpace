using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Represents a two-dimensional position using distance units for both axes.
/// Allows operations between positions and scalar values based on mathematical relationships.
/// </summary>
/// <param name="x">The X coordinate of the position.</param>
/// <param name="y">The Y coordinate of the position.</param>
public readonly struct position(distance x, distance y) :
    IEquatable<position>,
    IFormattable,
    IAdditionOperators<position, position, position>,
    ISubtractionOperators<position, position, position>,
    IMultiplyOperators<position, double, position>,
    IDivisionOperators<position, double, position>
{
    /// <summary>
    /// Gets the X coordinate of this position.
    /// </summary>
    public distance X { get; } = x;

    /// <summary>
    /// Gets the Y coordinate of this position.
    /// </summary>
    public distance Y { get; } = y;

    /// <summary>
    /// Returns the position formatted as an X and Y coordinate pair.
    /// </summary>
    /// <returns>Correctly formatted string representation of the position.</returns>
    public override string ToString() =>
        $"({X}, {Y})";

    /// <summary>
    /// Determines whether two positions have the same coordinates.
    /// </summary>
    /// <param name="other">The position to compare.</param>
    /// <returns>true if both positions contain equal X and Y coordinates. False otherwise.</returns>
    public bool Equals(position other) =>
        X.Equals(other.X) &&
        Y.Equals(other.Y);

    /// <summary>
    /// Compares two objects. True only if both are position and contain equal coordinates.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both are the same type and contain equal coordinates. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is position other && Equals(other);

    /// <summary>
    /// Hash code generated from the X and Y coordinates.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        HashCode.Combine(X, Y);

    /// <summary>
    /// Adds two positions together coordinate by coordinate.
    /// </summary>
    /// <param name="left">Left position.</param>
    /// <param name="right">Right position.</param>
    /// <returns>The resulting position.</returns>
    public static position operator +(position left, position right) =>
        new(left.X + right.X, left.Y + right.Y);

    /// <summary>
    /// Subtracts one position from another coordinate by coordinate.
    /// </summary>
    /// <param name="left">Left position.</param>
    /// <param name="right">Right position.</param>
    /// <returns>The difference between both positions.</returns>
    public static position operator -(position left, position right) =>
        new(left.X - right.X, left.Y - right.Y);

    /// <summary>
    /// Multiplies a position by a scalar value.
    /// </summary>
    /// <param name="pos">Input position.</param>
    /// <param name="scalar">Multiplier value.</param>
    /// <returns>The scaled position.</returns>
    public static position operator *(position pos, double scalar) =>
        new(pos.X * scalar, pos.Y * scalar);

    /// <summary>
    /// Multiplies a position by a scalar value.
    /// </summary>
    /// <param name="scalar">Multiplier value.</param>
    /// <param name="pos">Input position.</param>
    /// <returns>The scaled position.</returns>
    public static position operator *(double scalar, position pos) =>
        new(pos.X * scalar, pos.Y * scalar);

    /// <summary>
    /// Divides both vectors of a position by a scalar value.
    /// </summary>
    /// <param name="pos">Input position.</param>
    /// <param name="scalar">Divisor value.</param>
    /// <returns>The scaled position.</returns>
    public static position operator /(position pos, double scalar) =>
        new(pos.X / scalar, pos.Y / scalar);

    /// <summary>
    /// Determines whether two positions are equal.
    /// </summary>
    /// <param name="left">The first position to compare.</param>
    /// <param name="right">The second position to compare.</param>
    /// <returns><c>true</c> if both positions have the same coordinates; otherwise, <c>false</c>.</returns>
    public static bool operator ==(position left, position right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two positions are not equal.
    /// </summary>
    /// <param name="left">The first position to compare.</param>
    /// <param name="right">The second position to compare.</param>
    /// <returns><c>true</c> if positions have different coordinates; otherwise, <c>false</c>.</returns>
    public static bool operator !=(position left, position right) =>
        !left.Equals(right);

    /// <summary>
    /// Applies modulo operation to both coordinates of a position.
    /// </summary>
    /// <param name="left">The position value.</param>
    /// <param name="right">The position bounds.</param>
    /// <returns>The wrapped position within the given bounds.</returns>
    public static position operator %(position left, position right) =>
        new(left.X % right.X, left.Y % right.Y);

    /// <summary>
    /// Calculates the straight-line distance between two positions.
    /// </summary>
    /// <param name="other">The position to calculate distance to.</param>
    /// <returns>The distance between this position and the other position.</returns>
    public distance DistanceTo(position other)
    {
        distance dx = X - other.X;
        distance dy = Y - other.Y;

        return new distance(
            Math.Sqrt(
                dx.Value * dx.Value +
                dy.Value * dy.Value));
    }

    /// <summary>
    /// Converts this position to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted position string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"({X.ToString(format, provider)}, {Y.ToString(format, provider)})";
}