using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Area, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit.</param>
public readonly struct area(double value) :
    IEquatable<area>,
    IComparable<area>,
    IFormattable,
    IParsable<area>,
    IAdditionOperators<area, area, area>,
    ISubtractionOperators<area, area, area>,
    IMultiplyOperators<area, double, area>,
    IDivisionOperators<area, double, area>,
    IDivisionOperators<area, area, double>
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
    /// Returns true if units of area have the same Value.
    /// </summary>
    /// <param name="other">Area to compare.</param>
    /// <returns>True if equal Value. False otherwise.</returns>
    public bool Equals(area other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are area and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and have equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is area other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the Value and Negative state of this area.
    /// </summary>
    /// <returns>Formatted string representation.</returns>
    public override string ToString() =>
        $"{Value} m²";

    /// <summary>
    /// Divides area by a double scalar.
    /// </summary>
    /// <param name="a">Input area.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Area divided by the given scalar.</returns>
    public static area operator /(area a, double scalar) =>
        new(a.Value / scalar);

    /// <summary>
    /// Returns the ratio of two areas.
    /// </summary>
    /// <param name="left">Left area.</param>
    /// <param name="right">Right area.</param>
    /// <returns>Ratio of left area to right area.</returns>
    public static double operator /(area left, area right) =>
        left.Value / right.Value;

    /// <summary>
    /// Multiplies area by a double scalar.
    /// </summary>
    /// <param name="a">Input area.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Area multiplied by given scalar.</returns>
    public static area operator *(area a, double scalar) =>
        new(a.Value * scalar);

    /// <summary>
    /// Multiplies area by a double scalar.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="a">Input area.</param>
    /// <returns>Area multiplied by given scalar.</returns>
    public static area operator *(double scalar, area a) =>
        new(a.Value * scalar);

    /// <summary>
    /// Subtracts one area from another.
    /// </summary>
    /// <param name="left">Left area.</param>
    /// <param name="right">Right area.</param>
    /// <returns>Difference between both areas.</returns>
    public static area operator -(area left, area right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Adds two areas together.
    /// </summary>
    /// <param name="left">Left area.</param>
    /// <param name="right">Right area.</param>
    /// <returns>Sum of both areas.</returns>
    public static area operator +(area left, area right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Negates the area value.
    /// </summary>
    /// <param name="a">The area to negate.</param>
    /// <returns>An area with the opposite sign.</returns>
    public static area operator -(area a) =>
        new(-a.Value);

    /// <summary>
    /// Gets the absolute value of this area.
    /// </summary>
    /// <returns>A non-negative area.</returns>
    public area Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Determines whether one area is greater than another.
    /// </summary>
    /// <param name="left">The first area to compare.</param>
    /// <param name="right">The second area to compare.</param>
    /// <returns><c>true</c> if left is greater than right; otherwise, <c>false</c>.</returns>
    public static bool operator >(area left, area right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one area is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first area to compare.</param>
    /// <param name="right">The second area to compare.</param>
    /// <returns><c>true</c> if left is greater than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator >=(area left, area right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one area is less than another.
    /// </summary>
    /// <param name="left">The first area to compare.</param>
    /// <param name="right">The second area to compare.</param>
    /// <returns><c>true</c> if left is less than right; otherwise, <c>false</c>.</returns>
    public static bool operator <(area left, area right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one area is less than or equal to another.
    /// </summary>
    /// <param name="left">The first area to compare.</param>
    /// <param name="right">The second area to compare.</param>
    /// <returns><c>true</c> if left is less than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator <=(area left, area right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two areas are equal.
    /// </summary>
    /// <param name="left">The first area to compare.</param>
    /// <param name="right">The second area to compare.</param>
    /// <returns><c>true</c> if the areas have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(area left, area right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two areas are not equal.
    /// </summary>
    /// <param name="left">The first area to compare.</param>
    /// <param name="right">The second area to compare.</param>
    /// <returns><c>true</c> if the areas have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(area left, area right) =>
        !left.Equals(right);
    
    /// <summary>
    /// Calculates distance from area and distance using the formula area / distance = distance.
    /// </summary>
    /// <param name="a">The area.</param>
    /// <param name="d">The distance.</param>
    /// <returns>The resulting distance.</returns>
    public static distance operator /(area a, distance d) =>
        new(a.Value / d.Value);

    /// <summary>
    /// Calculates volume from area and distance using the formula area * distance = volume.
    /// </summary>
    /// <param name="a">The area.</param>
    /// <param name="d">The distance.</param>
    /// <returns>The resulting volume.</returns>
    public static volume operator *(area a, distance d) =>
        new(a.Value * d.Value);

    /// <summary>
    /// Calculates the square root of this area, returning the equivalent distance.
    /// </summary>
    /// <returns>The square root of the area as distance.</returns>
    public distance SquareRoot() =>
        new(Math.Sqrt(Value));

    /// <summary>
    /// Compares this area with another area.
    /// </summary>
    /// <param name="other">The area to compare against.</param>
    /// <returns>A value indicating the relative order of the areas.</returns>
    public int CompareTo(area other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this area to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted area string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m²";

    /// <summary>
    /// Parses a string into an area value.
    /// </summary>
    /// <param name="s">The string representation of the area.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed area.</returns>
    public static area Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into an area value.
    /// </summary>
    /// <param name="s">The string representation of the area.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed area if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out area result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new area(value);
            return true;
        }

        result = default;
        return false;
    }
}