using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Momentum, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit</param>
public readonly struct momentum(double value) :
    IEquatable<momentum>,
    IComparable<momentum>,
    IFormattable,
    IParsable<momentum>,
    IAdditionOperators<momentum, momentum, momentum>,
    ISubtractionOperators<momentum, momentum, momentum>,
    IMultiplyOperators<momentum, double, momentum>,
    IDivisionOperators<momentum, double, momentum>,
    IDivisionOperators<momentum, momentum, double>
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
    /// Returns true if units of momentum have same Value.
    /// </summary>
    /// <param name="other">Momentum to compare.</param>
    /// <returns>true if equal Value. False otherwise.</returns>
    public bool Equals(momentum other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are momentum and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and of equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is momentum other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the Value and sign information of this momentum.
    /// </summary>
    /// <returns>Correctly formatted string.</returns>
    public override string ToString() =>
        $"{Value} kg*m/s";

    /// <summary>
    /// Divides momentum by a double.
    /// </summary>
    /// <param name="m">Input momentum.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Momentum divided by a scalar.</returns>
    public static momentum operator /(momentum m, double scalar) =>
        new(m.Value / scalar);

    /// <summary>
    /// Returns a ratio of two momenta.
    /// </summary>
    /// <param name="left">Left momentum.</param>
    /// <param name="right">Right momentum.</param>
    /// <returns>Ratio of left to right momentum.</returns>
    public static double operator /(momentum left, momentum right) =>
        left.Value / right.Value;

    /// <summary>
    /// Multiplies momentum by a double.
    /// </summary>
    /// <param name="m">Input momentum.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Momentum multiplied by given scalar.</returns>
    public static momentum operator *(momentum m, double scalar) =>
        new(m.Value * scalar);

    /// <summary>
    /// Multiplies momentum by a double.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="m">Input momentum.</param>
    /// <returns>Momentum multiplied by given scalar.</returns>
    public static momentum operator *(double scalar, momentum m) =>
        new(m.Value * scalar);

    /// <summary>
    /// Adds two momenta together.
    /// </summary>
    /// <param name="left">Left momentum.</param>
    /// <param name="right">Right momentum.</param>
    /// <returns>Sum of both momenta.</returns>
    public static momentum operator +(momentum left, momentum right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Subtracts one momentum from another.
    /// </summary>
    /// <param name="left">Left momentum.</param>
    /// <param name="right">Right momentum.</param>
    /// <returns>Difference of both momenta.</returns>
    public static momentum operator -(momentum left, momentum right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Negates the momentum value.
    /// </summary>
    /// <param name="m">Momentum to negate.</param>
    /// <returns>A momentum with the opposite sign.</returns>
    public static momentum operator -(momentum m) =>
        new(-m.Value);

    /// <summary>
    /// Gets the absolute value of this momentum.
    /// </summary>
    /// <returns>A non-negative momentum.</returns>
    public momentum Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Determines whether one momentum is greater than another.
    /// </summary>
    /// <param name="left">The first momentum to compare.</param>
    /// <param name="right">The second momentum to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(momentum left, momentum right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one momentum is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first momentum to compare.</param>
    /// <param name="right">The second momentum to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(momentum left, momentum right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one momentum is less than another.
    /// </summary>
    /// <param name="left">The first momentum to compare.</param>
    /// <param name="right">The second momentum to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(momentum left, momentum right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one momentum is less than or equal to another.
    /// </summary>
    /// <param name="left">The first momentum to compare.</param>
    /// <param name="right">The second momentum to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(momentum left, momentum right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two momenta are equal.
    /// </summary>
    /// <param name="left">The first momentum to compare.</param>
    /// <param name="right">The second momentum to compare.</param>
    /// <returns><c>true</c> if the momenta have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(momentum left, momentum right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two momenta are not equal.
    /// </summary>
    /// <param name="left">The first momentum to compare.</param>
    /// <param name="right">The second momentum to compare.</param>
    /// <returns><c>true</c> if the momenta have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(momentum left, momentum right) =>
        !left.Equals(right);

    /// <summary>
    /// Calculates speed from momentum and mass.
    /// </summary>
    /// <param name="m">The momentum.</param>
    /// <param name="w">The mass.</param>
    /// <returns>The resulting speed.</returns>
    // momentum / mass = speed
    public static speed operator /(momentum m, mass w) =>
        new(m.Value / w.Value);

    /// <summary>
    /// Calculates mass from momentum and speed.
    /// </summary>
    /// <param name="m">The momentum.</param>
    /// <param name="s">The speed.</param>
    /// <returns>The resulting mass.</returns>
    // momentum / speed = mass
    public static mass operator /(momentum m, speed s) =>
        new(m.Value / s.Value);

    /// <summary>
    /// Compares this momentum with another momentum.
    /// </summary>
    /// <param name="other">The momentum to compare against.</param>
    /// <returns>A value indicating the relative order of the momenta.</returns>
    public int CompareTo(momentum other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this momentum to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted momentum string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} kg*m/s";

    /// <summary>
    /// Parses a string into a momentum value.
    /// </summary>
    /// <param name="s">The string representation of the momentum.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed momentum.</returns>
    public static momentum Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a momentum value.
    /// </summary>
    /// <param name="s">The string representation of the momentum.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed momentum if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out momentum result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new momentum(value);
            return true;
        }

        result = default;
        return false;
    }
}