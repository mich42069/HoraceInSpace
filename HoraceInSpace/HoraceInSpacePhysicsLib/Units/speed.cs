using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Speed, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit</param>
public readonly struct speed(double value) :
    IEquatable<speed>,
    IComparable<speed>,
    IFormattable,
    IParsable<speed>,
    IAdditionOperators<speed, speed, speed>,
    ISubtractionOperators<speed, speed, speed>,
    IMultiplyOperators<speed, double, speed>,
    IDivisionOperators<speed, double, speed>,
    IDivisionOperators<speed, speed, double>
{
    /// <summary>
    /// Stores the underlying Value of this unit as a double.
    /// </summary>
    public double Value { get; } = value;

    /// <summary>
    /// Returns true if unit is less than 0, else false.
    /// </summary>
    public bool Negative => Value < 0;

    /// <summary>
    /// Returns true if units of speed have same Value.
    /// </summary>
    /// <param name="other">Speed to compare.</param>
    /// <returns>true if equal Value. False otherwise.</returns>
    public bool Equals(speed other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are speed and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and of equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is speed other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns Value m/s.
    /// </summary>
    /// <returns>Correctly formatted string.</returns>
    public override string ToString() =>
        $"{Value} m/s";

    /// <summary>
    /// Divides speed by a double.
    /// </summary>
    /// <param name="s">Input speed.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Speed divided by a scalar.</returns>
    public static speed operator /(speed s, double scalar) =>
        new(s.Value / scalar);

    /// <summary>
    /// Returns a ratio of two speeds.
    /// </summary>
    /// <param name="left">Left speed.</param>
    /// <param name="right">Right speed.</param>
    /// <returns>Ratio of left to right speed.</returns>
    public static double operator /(speed left, speed right) =>
        left.Value / right.Value;

    /// <summary>
    /// Multiplies speed by a double.
    /// </summary>
    /// <param name="s">Input speed.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Speed multiplied by given scalar.</returns>
    public static speed operator *(speed s, double scalar) =>
        new(s.Value * scalar);

    /// <summary>
    /// Multiplies speed by a double.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="s">Input speed.</param>
    /// <returns>Speed multiplied by given scalar.</returns>
    public static speed operator *(double scalar, speed s) =>
        new(s.Value * scalar);

    /// <summary>
    /// Subtracts one speed from another.
    /// </summary>
    /// <param name="left">Left speed.</param>
    /// <param name="right">Right speed.</param>
    /// <returns>Difference of both values.</returns>
    public static speed operator -(speed left, speed right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Adds two speeds together.
    /// </summary>
    /// <param name="left">Left speed.</param>
    /// <param name="right">Right speed.</param>
    /// <returns>Sum of both speeds.</returns>
    public static speed operator +(speed left, speed right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Negates the speed value.
    /// </summary>
    /// <param name="s">Speed to negate.</param>
    /// <returns>A speed with the opposite sign.</returns>
    public static speed operator -(speed s) =>
        new(-s.Value);

    /// <summary>
    /// Gets the absolute value of this speed.
    /// </summary>
    /// <returns>A non-negative speed.</returns>
    public speed Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Determines whether one speed is greater than another.
    /// </summary>
    /// <param name="left">The first speed to compare.</param>
    /// <param name="right">The second speed to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(speed left, speed right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one speed is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first speed to compare.</param>
    /// <param name="right">The second speed to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(speed left, speed right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one speed is less than another.
    /// </summary>
    /// <param name="left">The first speed to compare.</param>
    /// <param name="right">The second speed to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(speed left, speed right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one speed is less than or equal to another.
    /// </summary>
    /// <param name="left">The first speed to compare.</param>
    /// <param name="right">The second speed to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(speed left, speed right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two speeds are equal.
    /// </summary>
    /// <param name="left">The first speed to compare.</param>
    /// <param name="right">The second speed to compare.</param>
    /// <returns><c>true</c> if the speeds have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(speed left, speed right) =>
        left.Value == right.Value;

    /// <summary>
    /// Determines whether two speeds are not equal.
    /// </summary>
    /// <param name="left">The first speed to compare.</param>
    /// <param name="right">The second speed to compare.</param>
    /// <returns><c>true</c> if the speeds have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(speed left, speed right) =>
        left.Value != right.Value;

    /// <summary>
    /// Calculates the distance traveled from speed and elapsed time.
    /// </summary>
    /// <param name="s">The speed.</param>
    /// <param name="t">The elapsed time.</param>
    /// <returns>The resulting distance.</returns>
    public static distance operator *(speed s, TimeSpan t) =>
        new(s.Value * t.TotalSeconds);

    /// <summary>
    /// Calculates acceleration from speed and elapsed time.
    /// </summary>
    /// <param name="s">The speed.</param>
    /// <param name="t">The elapsed time.</param>
    /// <returns>The resulting acceleration.</returns>
    public static acceleration operator /(speed s, TimeSpan t) =>
        new(s.Value / t.TotalSeconds);

    /// <summary>
    /// Calculates momentum from speed and mass.
    /// </summary>
    /// <param name="s">The speed.</param>
    /// <param name="m">The mass.</param>
    /// <returns>The resulting momentum.</returns>
    public static momentum operator *(speed s, mass m) =>
        new(s.Value * m.Value);

    /// <summary>
    /// Compares this speed with another speed.
    /// </summary>
    /// <param name="other">The speed to compare against.</param>
    /// <returns>A value indicating the relative order of the speeds.</returns>
    public int CompareTo(speed other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this speed to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted speed string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m/s";

    /// <summary>
    /// Parses a string into a speed value.
    /// </summary>
    /// <param name="s">The string representation of the speed.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed speed.</returns>
    public static speed Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a speed value.
    /// </summary>
    /// <param name="s">The string representation of the speed.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed speed if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out speed result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new speed(value);
            return true;
        }

        result = default;
        return false;
    }
}