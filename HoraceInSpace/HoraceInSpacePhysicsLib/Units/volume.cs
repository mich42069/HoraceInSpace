using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Volume, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit</param>
public readonly struct volume(double value) :
    IEquatable<volume>,
    IComparable<volume>,
    IFormattable,
    IParsable<volume>,
    IAdditionOperators<volume, volume, volume>,
    ISubtractionOperators<volume, volume, volume>,
    IMultiplyOperators<volume, double, volume>,
    IDivisionOperators<volume, double, volume>,
    IDivisionOperators<volume, volume, double>
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
    /// Returns true if units of volume have same Value.
    /// </summary>
    /// <param name="other">Volume to compare.</param>
    /// <returns>true if equal Value. False otherwise.</returns>
    public bool Equals(volume other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are volume and equal
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and of equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is volume other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns Value m³
    /// </summary>
    /// <returns>Correctly formatted string</returns>
    public override string ToString() =>
        $"{Value} m³";

    /// <summary>
    /// Addition operation based on Value.
    /// </summary>
    /// <param name="left">Left volume</param>
    /// <param name="right">Right volume</param>
    /// <returns>Sum of both volumes.</returns>
    public static volume operator +(volume left, volume right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Substraction operation based on Value.
    /// </summary>
    /// <param name="left">Left Volume</param>
    /// <param name="right">Right Volume</param>
    /// <returns>Difference of both Values.</returns>
    public static volume operator -(volume left, volume right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Multiplies volume by a double.
    /// </summary>
    /// <param name="vol">Input volume</param>
    /// <param name="scalar">Multiplier double</param>
    /// <returns>Volume multiplied by given scalar.</returns>
    public static volume operator *(volume vol, double scalar) =>
        new(vol.Value * scalar);

    /// <summary>
    /// Multiplies volume by a double.
    /// </summary>
    /// <param name="scalar">Multiplier double</param>
    /// <param name="vol">Input volume</param>
    /// <returns>Volume multiplied by given scalar.</returns>
    public static volume operator *(double scalar, volume vol) =>
        new(vol.Value * scalar);

    /// <summary>
    /// Divides volume by a double
    /// </summary>
    /// <param name="vol">Input volume</param>
    /// <param name="scalar">Divisor double</param>
    /// <returns>Volume divided by a scalar.</returns>
    public static volume operator /(volume vol, double scalar) =>
        new(vol.Value / scalar);

    /// <summary>
    /// Returns a Ratio of two volumes.
    /// </summary>
    /// <param name="left">Left volume</param>
    /// <param name="right">Right volume</param>
    /// <returns>Ratio of left to right volume</returns>
    public static double operator /(volume left, volume right) =>
        left.Value / right.Value;

    /// <summary>
    /// Calculates the cross-sectional area from a volume and a length.
    /// </summary>
    /// <param name="vol">The volume.</param>
    /// <param name="dist">The length.</param>
    /// <returns>The resulting area.</returns>
    public static area operator /(volume vol, distance dist) =>
        new(vol.Value / dist.Value);

    /// <summary>
    /// Calculates the mass of a body from its volume and density.
    /// </summary>
    /// <param name="vol">The volume.</param>
    /// <param name="den">The density.</param>
    /// <returns>The resulting mass.</returns>
    public static mass operator *(volume vol, density den) =>
        new(den.Value * vol.Value);

    /// <summary>
    /// Determines whether one volume is greater than another.
    /// </summary>
    /// <param name="left">The first volume to compare.</param>
    /// <param name="right">The second volume to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(volume left, volume right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one volume is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first volume to compare.</param>
    /// <param name="right">The second volume to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(volume left, volume right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one volume is less than another.
    /// </summary>
    /// <param name="left">The first volume to compare.</param>
    /// <param name="right">The second volume to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(volume left, volume right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one volume is less than or equal to another.
    /// </summary>
    /// <param name="left">The first volume to compare.</param>
    /// <param name="right">The second volume to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(volume left, volume right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two volumes are equal.
    /// </summary>
    /// <param name="left">The first volume to compare.</param>
    /// <param name="right">The second volume to compare.</param>
    /// <returns><c>true</c> if the volumes have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(volume left, volume right) =>
        left.Value == right.Value;

    /// <summary>
    /// Determines whether two volumes are not equal.
    /// </summary>
    /// <param name="left">The first volume to compare.</param>
    /// <param name="right">The second volume to compare.</param>
    /// <returns><c>true</c> if the volumes have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(volume left, volume right) =>
        left.Value != right.Value;

    /// <summary>
    /// Negates the volume value.
    /// </summary>
    /// <param name="vol">The volume to negate.</param>
    /// <returns>A volume with the opposite sign.</returns>
    public static volume operator -(volume vol) =>
        new(-vol.Value);

    /// <summary>
    /// Gets the absolute value of this volume.
    /// </summary>
    /// <returns>A non-negative volume.</returns>
    public volume Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Compares this volume with another volume.
    /// </summary>
    /// <param name="other">The volume to compare against.</param>
    /// <returns>A value indicating the relative order of the volumes.</returns>
    public int CompareTo(volume other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this volume to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted volume string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m³";

    /// <summary>
    /// Parses a string into a volume value.
    /// </summary>
    /// <param name="s">The string representation of the volume.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed volume.</returns>
    public static volume Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a volume value.
    /// </summary>
    /// <param name="s">The string representation of the volume.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed volume if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out volume result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new volume(value);
            return true;
        }

        result = default;
        return false;
    }
}