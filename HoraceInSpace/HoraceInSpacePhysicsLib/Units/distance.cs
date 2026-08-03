using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Represents a unit of distance and allows operations between itself and other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the unit.</param>
public readonly struct distance(double value) : 
   IEquatable<distance>, 
   IComparable<distance>, 
   IFormattable,
   IParsable<distance>,
   IAdditionOperators<distance, distance, distance>,
   ISubtractionOperators<distance, distance, distance>,
   IMultiplyOperators<distance, double, distance>,
   IDivisionOperators<distance, distance, double>,
   IDivisionOperators<distance, double, distance>
{
   /// <summary>
   /// Stores the underlying value of this unit as a double.
   /// </summary>
   public double Value { get; } = value;

    /// <summary>
    /// Returns true if two distances have the same value.
    /// </summary>
    /// <param name="other">Distance to compare.</param>
    /// <returns>True if values are equal. False otherwise.</returns>
    public bool Equals(distance other) =>
       Value.Equals(other.Value);

    /// <summary>
    /// Compares this object with another object.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both objects are distances with equal values. False otherwise.</returns>
    public override bool Equals(object? obj) =>
       obj is distance other && Equals(other);

    /// <summary>
    /// Generates a hash code from the stored value.
    /// </summary>
    /// <returns>Generated hash code of type int.</returns>
    public override int GetHashCode() =>
       Value.GetHashCode();

    /// <summary>
    /// Returns the string representation of this distance.
    /// </summary>
    /// <returns>Formatted distance value.</returns>
    public override string ToString() => 
       $"{Value} m";

    /// <summary>
    /// Returns true if the distance value is less than zero.
    /// </summary>
    public bool Negative => Value < 0;

    /// <summary>
    /// Divides a distance by a scalar value.
    /// </summary>
    /// <param name="dist">Distance to divide.</param>
    /// <param name="scalar">Scalar divisor.</param>
    /// <returns>The resulting distance.</returns>
    public static distance operator /(distance dist, double scalar) =>
       new(dist.Value / scalar);

    /// <summary>
    /// Returns the ratio between two distances.
    /// </summary>
    /// <param name="left">Left distance.</param>
    /// <param name="rightDistance">Right distance.</param>
    /// <returns>The ratio of the two distances.</returns>
    public static double operator /(distance left, distance rightDistance) =>
       left.Value / rightDistance.Value;

    /// <summary>
    /// Multiplies a distance by a scalar value.
    /// </summary>
    /// <param name="dist">Distance to multiply.</param>
    /// <param name="scalar">Scalar multiplier.</param>
    /// <returns>The resulting distance.</returns>
    public static distance operator *(distance dist, double scalar) =>
       new(dist.Value * scalar);

    /// <summary>
    /// Multiplies a distance by a scalar value.
    /// </summary>
    /// <param name="scalar">Scalar multiplier.</param>
    /// <param name="dist">Distance to multiply.</param>
    /// <returns>The resulting distance.</returns>
    public static distance operator *(double scalar, distance dist) =>
       new(dist.Value * scalar);

    /// <summary>
    /// Subtracts one distance from another.
    /// </summary>
    /// <param name="left">Left distance.</param>
    /// <param name="right">Right distance.</param>
    /// <returns>The difference between the distances.</returns>
    public static distance operator -(distance left, distance right) =>
       new(left.Value - right.Value);

    /// <summary>
    /// Adds two distances together.
    /// </summary>
    /// <param name="left">Left distance.</param>
    /// <param name="right">Right distance.</param>
    /// <returns>The sum of the distances.</returns>
    public static distance operator +(distance left, distance right) =>
       new(left.Value + right.Value);

    /// <summary>
    /// Determines whether one distance is greater than another.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>True if left is greater than right.</returns>
    public static bool operator >(distance left, distance right) =>
       left.Value > right.Value;

    /// <summary>
    /// Determines whether one distance is greater than or equal to another.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>True if left is greater than or equal to right.</returns>
    public static bool operator >=(distance left, distance right) =>
       left.Value > right.Value;

    /// <summary>
    /// Determines whether one distance is less than another.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>True if left is less than right.</returns>
    public static bool operator <(distance left, distance right) =>
       left.Value < right.Value;

    /// <summary>
    /// Determines whether one distance is less than or equal to another.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>True if left is less than or equal to right.</returns>
    public static bool operator <=(distance left, distance right) =>
       left.Value <= right.Value;

    /// <summary>
    /// Determines whether two distances are equal.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>True if both distances have equal values.</returns>
    public static bool operator ==(distance left, distance right) =>
       left.Value == right.Value;

    /// <summary>
    /// Determines whether two distances are not equal.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>True if values differ.</returns>
    public static bool operator !=(distance left, distance right) =>
       left.Value != right.Value;

    /// <summary>
    /// Negates the distance value.
    /// </summary>
    /// <param name="dist">Distance to negate.</param>
    /// <returns>A distance with the opposite sign.</returns>
    public static distance operator -(distance dist) =>
       new (-dist.Value);

    /// <summary>
    /// Returns the absolute value of this distance.
    /// </summary>
    /// <returns>A non-negative distance.</returns>
    public distance Abs() => Negative ? -this : this;

    /// <summary>
    /// Applies a modulo operation between two distances.
    /// </summary>
    /// <param name="left">Distance value.</param>
    /// <param name="right">Modulo distance.</param>
    /// <returns>The wrapped distance value.</returns>
    public static distance operator %(distance left, distance right)
    {
       double value = left.Value % right.Value;

       if (value < 0)
          value += right.Value;

       return new(value);
    }

    /// <summary>
    /// Calculates speed from distance divided by time.
    /// </summary>
    /// <param name="dist">Distance travelled.</param>
    /// <param name="t">Elapsed time.</param>
    /// <returns>The resulting speed.</returns>
    public static speed operator /(distance dist, TimeSpan t) =>
       new speed(dist.Value / t.TotalSeconds);

    /// <summary>
    /// Calculates area from multiplying two distances.
    /// </summary>
    /// <param name="left">First distance.</param>
    /// <param name="right">Second distance.</param>
    /// <returns>The resulting area.</returns>
    public static area operator *(distance left, distance right) =>
       new area(left.Value * right.Value);

    /// <summary>
    /// Compares this distance with another distance.
    /// </summary>
    /// <param name="other">Distance to compare against.</param>
    /// <returns>A value indicating the relative order of the distances.</returns>
    public int CompareTo(distance other) =>
       Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this distance to a formatted string.
    /// </summary>
    /// <param name="format">Numeric format string.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>Formatted distance string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
       $"{Value.ToString(format, provider)} m";

    /// <summary>
    /// Parses a string into a distance value.
    /// </summary>
    /// <param name="s">String representation of the distance.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>The parsed distance.</returns>
    public static distance Parse(string s, IFormatProvider? provider) =>
       new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a distance value.
    /// </summary>
    /// <param name="s">String representation of the distance.</param>
    /// <param name="provider">Format provider.</param>
    /// <param name="result">When this method returns, contains the parsed distance if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out distance result)
    {
       if (double.TryParse(s, provider, out double value))
       {
          result = new distance(value);
          return true;
       }

       result = default;
       return false;
    }
}