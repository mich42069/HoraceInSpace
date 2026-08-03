using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Acceleration, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit.</param>
public readonly struct acceleration(double value) :
    IEquatable<acceleration>,
    IComparable<acceleration>,
    IFormattable,
    IParsable<acceleration>,
    IAdditionOperators<acceleration, acceleration, acceleration>,
    ISubtractionOperators<acceleration, acceleration, acceleration>,
    IMultiplyOperators<acceleration, double, acceleration>,
    IDivisionOperators<acceleration, double, acceleration>,
    IDivisionOperators<acceleration, acceleration, double>
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
    /// Returns true if units of acceleration have the same Value.
    /// </summary>
    /// <param name="other">Acceleration to compare.</param>
    /// <returns>True if equal Value. False otherwise.</returns>
    public bool Equals(acceleration other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are acceleration and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and have equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is acceleration other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the Value and Negative state of this acceleration.
    /// </summary>
    /// <returns>Formatted string representation.</returns>
    public override string ToString() =>
        $"{Value} m/s²";


    /// <summary>
    /// Divides acceleration by a double scalar.
    /// </summary>
    /// <param name="a">Input acceleration.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Acceleration divided by the given scalar.</returns>
    public static acceleration operator /(acceleration a, double scalar) =>
        new(a.Value / scalar);

    /// <summary>
    /// Returns the ratio of two accelerations.
    /// </summary>
    /// <param name="left">Left acceleration.</param>
    /// <param name="right">Right acceleration.</param>
    /// <returns>Ratio of left acceleration to right acceleration.</returns>
    public static double operator /(acceleration left, acceleration right) =>
        left.Value / right.Value;

    /// <summary>
    /// Multiplies acceleration by a double scalar.
    /// </summary>
    /// <param name="a">Input acceleration.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Acceleration multiplied by the given scalar.</returns>
    public static acceleration operator *(acceleration a, double scalar) =>
        new(a.Value * scalar);

    /// <summary>
    /// Multiplies acceleration by a double scalar.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="a">Input acceleration.</param>
    /// <returns>Acceleration multiplied by the given scalar.</returns>
    public static acceleration operator *(double scalar, acceleration a) =>
        new(a.Value * scalar);

    /// <summary>
    /// Adds two accelerations together.
    /// </summary>
    /// <param name="left">Left acceleration.</param>
    /// <param name="right">Right acceleration.</param>
    /// <returns>Sum of both accelerations.</returns>
    public static acceleration operator +(acceleration left, acceleration right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Subtracts one acceleration from another.
    /// </summary>
    /// <param name="left">Left acceleration.</param>
    /// <param name="right">Right acceleration.</param>
    /// <returns>Difference between both accelerations.</returns>
    public static acceleration operator -(acceleration left, acceleration right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Negates the acceleration value.
    /// </summary>
    /// <param name="a">Acceleration to negate.</param>
    /// <returns>Acceleration with the opposite sign.</returns>
    public static acceleration operator -(acceleration a) =>
        new(-a.Value);

    /// <summary>
    /// Gets the absolute value of this acceleration.
    /// </summary>
    /// <returns>A non-negative acceleration.</returns>
    public acceleration Abs() =>
        Negative ? -this : this;


    /// <summary>
    /// Determines whether one acceleration is greater than another.
    /// </summary>
    /// <param name="left">The first acceleration to compare.</param>
    /// <param name="right">The second acceleration to compare.</param>
    /// <returns><c>true</c> if left is greater than right; otherwise, <c>false</c>.</returns>
    public static bool operator >(acceleration left, acceleration right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one acceleration is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first acceleration to compare.</param>
    /// <param name="right">The second acceleration to compare.</param>
    /// <returns><c>true</c> if left is greater than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator >=(acceleration left, acceleration right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one acceleration is less than another.
    /// </summary>
    /// <param name="left">The first acceleration to compare.</param>
    /// <param name="right">The second acceleration to compare.</param>
    /// <returns><c>true</c> if left is less than right; otherwise, <c>false</c>.</returns>
    public static bool operator <(acceleration left, acceleration right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one acceleration is less than or equal to another.
    /// </summary>
    /// <param name="left">The first acceleration to compare.</param>
    /// <param name="right">The second acceleration to compare.</param>
    /// <returns><c>true</c> if left is less than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator <=(acceleration left, acceleration right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two accelerations are equal.
    /// </summary>
    /// <param name="left">The first acceleration to compare.</param>
    /// <param name="right">The second acceleration to compare.</param>
    /// <returns><c>true</c> if the accelerations have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(acceleration left, acceleration right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two accelerations are not equal.
    /// </summary>
    /// <param name="left">The first acceleration to compare.</param>
    /// <param name="right">The second acceleration to compare.</param>
    /// <returns><c>true</c> if the accelerations have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(acceleration left, acceleration right) =>
        !left.Equals(right);


    /// <summary>
    /// Calculates speed from acceleration and time using the formula acceleration * time = speed.
    /// </summary>
    /// <param name="a">The acceleration.</param>
    /// <param name="t">The time interval.</param>
    /// <returns>The resulting speed.</returns>
    public static speed operator *(acceleration a, TimeSpan t) =>
        new(a.Value * t.TotalSeconds);


    /// <summary>
    /// Compares this acceleration with another acceleration.
    /// </summary>
    /// <param name="other">The acceleration to compare against.</param>
    /// <returns>A value indicating the relative order of the accelerations.</returns>
    public int CompareTo(acceleration other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this acceleration to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted acceleration string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m/s²";

    /// <summary>
    /// Parses a string into an acceleration value.
    /// </summary>
    /// <param name="s">The string representation of the acceleration.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed acceleration.</returns>
    public static acceleration Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into an acceleration value.
    /// </summary>
    /// <param name="s">The string representation of the acceleration.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed acceleration if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out acceleration result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new acceleration(value);
            return true;
        }

        result = default;
        return false;
    }
}