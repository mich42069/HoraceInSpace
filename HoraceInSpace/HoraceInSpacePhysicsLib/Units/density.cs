using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Represents a unit of density and allows operations between itself and other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the unit.</param>
public readonly struct density(double value) :
    IEquatable<density>,
    IComparable<density>,
    IFormattable,
    IParsable<density>,
    IAdditionOperators<density, density, density>,
    ISubtractionOperators<density, density, density>,
    IMultiplyOperators<density, double, density>,
    IDivisionOperators<density, double, density>,
    IDivisionOperators<density, density, double>
{
    /// <summary>
    /// Stores the underlying value of this unit as a double.
    /// </summary>
    public double Value { get; } = value;

    /// <summary>
    /// Returns true if the density value is less than zero.
    /// </summary>
    public bool Negative => Value < 0;

    /// <summary>
    /// Returns true if two densities have the same value.
    /// </summary>
    /// <param name="other">Density to compare.</param>
    /// <returns>True if values are equal. False otherwise.</returns>
    public bool Equals(density other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are density and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both objects are the same type and have equal values. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is density other && Equals(other);

    /// <summary>
    /// Generates a hash code from the stored value.
    /// </summary>
    /// <returns>Generated hash code of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the string representation of this density.
    /// </summary>
    /// <returns>Formatted density value.</returns>
    public override string ToString() =>
        $"{Value} kg/m³";

    /// <summary>
    /// Adds two density values together.
    /// </summary>
    /// <param name="left">Left density.</param>
    /// <param name="right">Right density.</param>
    /// <returns>The sum of the densities.</returns>
    public static density operator +(density left, density right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Subtracts one density value from another.
    /// </summary>
    /// <param name="left">Left density.</param>
    /// <param name="right">Right density.</param>
    /// <returns>The difference between the densities.</returns>
    public static density operator -(density left, density right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Multiplies density by a scalar value.
    /// </summary>
    /// <param name="d">Density to multiply.</param>
    /// <param name="scalar">Scalar multiplier.</param>
    /// <returns>The resulting density.</returns>
    public static density operator *(density d, double scalar) =>
        new(d.Value * scalar);

    /// <summary>
    /// Multiplies density by a scalar value.
    /// </summary>
    /// <param name="scalar">Scalar multiplier.</param>
    /// <param name="d">Density to multiply.</param>
    /// <returns>The resulting density.</returns>
    public static density operator *(double scalar, density d) =>
        new(d.Value * scalar);

    /// <summary>
    /// Divides density by a scalar value.
    /// </summary>
    /// <param name="d">Density to divide.</param>
    /// <param name="scalar">Scalar divisor.</param>
    /// <returns>The resulting density.</returns>
    public static density operator /(density d, double scalar) =>
        new(d.Value / scalar);

    /// <summary>
    /// Returns the ratio between two density values.
    /// </summary>
    /// <param name="left">Left density.</param>
    /// <param name="right">Right density.</param>
    /// <returns>The ratio of the two densities.</returns>
    public static double operator /(density left, density right) =>
        left.Value / right.Value;

    /// <summary>
    /// Negates the density value.
    /// </summary>
    /// <param name="d">Density to negate.</param>
    /// <returns>A density with the opposite sign.</returns>
    public static density operator -(density d) =>
        new(-d.Value);

    /// <summary>
    /// Returns the absolute value of this density.
    /// </summary>
    /// <returns>A non-negative density.</returns>
    public density Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Calculates mass from density and volume.
    /// </summary>
    /// <param name="d">Density of the object.</param>
    /// <param name="v">Volume of the object.</param>
    /// <returns>The resulting mass.</returns>
    public static mass operator *(density d, volume v) =>
        new(d.Value * v.Value);

    /// <summary>
    /// Determines whether one density is greater than another.
    /// </summary>
    /// <param name="left">First density.</param>
    /// <param name="right">Second density.</param>
    /// <returns>True if left is greater than right.</returns>
    public static bool operator >(density left, density right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one density is greater than or equal to another.
    /// </summary>
    /// <param name="left">First density.</param>
    /// <param name="right">Second density.</param>
    /// <returns>True if left is greater than or equal to right.</returns>
    public static bool operator >=(density left, density right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one density is less than another.
    /// </summary>
    /// <param name="left">First density.</param>
    /// <param name="right">Second density.</param>
    /// <returns>True if left is less than right.</returns>
    public static bool operator <(density left, density right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one density is less than or equal to another.
    /// </summary>
    /// <param name="left">First density.</param>
    /// <param name="right">Second density.</param>
    /// <returns>True if left is less than or equal to right.</returns>
    public static bool operator <=(density left, density right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two densities are equal.
    /// </summary>
    /// <param name="left">First density.</param>
    /// <param name="right">Second density.</param>
    /// <returns>True if both densities have equal values.</returns>
    public static bool operator ==(density left, density right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two densities are not equal.
    /// </summary>
    /// <param name="left">First density.</param>
    /// <param name="right">Second density.</param>
    /// <returns>True if values differ.</returns>
    public static bool operator !=(density left, density right) =>
        !left.Equals(right);

    /// <summary>
    /// Compares this density with another density.
    /// </summary>
    /// <param name="other">Density to compare against.</param>
    /// <returns>A value indicating the relative order of the densities.</returns>
    public int CompareTo(density other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this density to a formatted string using the specified format.
    /// </summary>
    /// <param name="format">Numeric format string.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>Formatted density string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} kg/m³";

    /// <summary>
    /// Parses a string into a density value.
    /// </summary>
    /// <param name="s">String representation of the density.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>The parsed density.</returns>
    public static density Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a density value.
    /// </summary>
    /// <param name="s">String representation of the density.</param>
    /// <param name="provider">Format provider.</param>
    /// <param name="result">When this method returns, contains the parsed density if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out density result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new density(value);
            return true;
        }

        result = default;
        return false;
    }

}