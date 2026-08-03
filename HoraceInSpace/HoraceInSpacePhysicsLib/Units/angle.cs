using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Angle, allows for operations between itself and few other units based on given mathematical formulas.
/// </summary>
/// <param name="value">Value that represents the Unit in radians.</param>
public readonly struct angle(double value) :
    IEquatable<angle>,
    IComparable<angle>,
    IFormattable,
    IParsable<angle>,
    IAdditionOperators<angle, angle, angle>,
    ISubtractionOperators<angle, angle, angle>,
    IMultiplyOperators<angle, double, angle>,
    IDivisionOperators<angle, double, angle>,
    IDivisionOperators<angle, angle, double>
{
    /// <summary>
    /// Stores the underlying Value of this unit as a double in radians.
    /// </summary>
    public double Value { get; } = value;

    /// <summary>
    /// Returns true if unit is less than 0, else false.
    /// </summary>
    public bool Negative => Value < 0;

    /// <summary>
    /// Represents an angle of 0 degrees.
    /// </summary>
    public static angle Deg0 => FromDegrees(0);

    /// <summary>
    /// Represents an angle of 90 degrees.
    /// </summary>
    public static angle Deg90 => FromDegrees(90);

    /// <summary>
    /// Represents an angle of 180 degrees.
    /// </summary>
    public static angle Deg180 => FromDegrees(180);

    /// <summary>
    /// Represents an angle of 270 degrees.
    /// </summary>
    public static angle Deg270 => FromDegrees(270);

    /// <summary>
    /// Represents the angle Pi radians.
    /// </summary>
    public static angle Pi => new(Math.PI);

    /// <summary>
    /// Represents the angle Tau radians.
    /// </summary>
    public static angle Tau => new(Math.Tau);
    
    /// <summary>
    /// Returns true if units of angle have the same Value.
    /// </summary>
    /// <param name="other">Angle to compare.</param>
    /// <returns>True if equal Value. False otherwise.</returns>
    public bool Equals(angle other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are angle and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and have equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is angle other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the Value in radians.
    /// </summary>
    /// <returns>Formatted string representation.</returns>
    public override string ToString() =>
        $"{Value} rad";
    
    /// <summary>
    /// Adds two angles together.
    /// </summary>
    /// <param name="left">Left angle.</param>
    /// <param name="right">Right angle.</param>
    /// <returns>Sum of both angles.</returns>
    public static angle operator +(angle left, angle right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Subtracts one angle from another.
    /// </summary>
    /// <param name="left">Left angle.</param>
    /// <param name="right">Right angle.</param>
    /// <returns>Difference between both angles.</returns>
    public static angle operator -(angle left, angle right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Negates the angle value.
    /// </summary>
    /// <param name="a">Angle to negate.</param>
    /// <returns>Angle with the opposite sign.</returns>
    public static angle operator -(angle a) =>
        new(-a.Value);

    /// <summary>
    /// Multiplies an angle by a double scalar.
    /// </summary>
    /// <param name="a">Input angle.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Angle multiplied by the scalar.</returns>
    public static angle operator *(angle a, double scalar) =>
        new(a.Value * scalar);

    /// <summary>
    /// Multiplies an angle by a double scalar.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="a">Input angle.</param>
    /// <returns>Angle multiplied by the scalar.</returns>
    public static angle operator *(double scalar, angle a) =>
        new(a.Value * scalar);

    /// <summary>
    /// Divides an angle by a double scalar.
    /// </summary>
    /// <param name="a">Input angle.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Angle divided by the scalar.</returns>
    public static angle operator /(angle a, double scalar) =>
        new(a.Value / scalar);

    /// <summary>
    /// Returns the ratio of two angles.
    /// </summary>
    /// <param name="left">Left angle.</param>
    /// <param name="right">Right angle.</param>
    /// <returns>Ratio of left angle to right angle.</returns>
    public static double operator /(angle left, angle right) =>
        left.Value / right.Value;

    /// <summary>
    /// Determines whether one angle is greater than another.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns><c>true</c> if left is greater than right; otherwise, <c>false</c>.</returns>
    public static bool operator >(angle left, angle right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one angle is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns><c>true</c> if left is greater than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator >=(angle left, angle right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one angle is less than another.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns><c>true</c> if left is less than right; otherwise, <c>false</c>.</returns>
    public static bool operator <(angle left, angle right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one angle is less than or equal to another.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns><c>true</c> if left is less than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator <=(angle left, angle right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two angles are equal.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns><c>true</c> if the angles have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(angle left, angle right) =>
        left.Equals(right);

    /// <summary>
    /// Determines whether two angles are not equal.
    /// </summary>
    /// <param name="left">The first angle to compare.</param>
    /// <param name="right">The second angle to compare.</param>
    /// <returns><c>true</c> if the angles have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(angle left, angle right) =>
        !left.Equals(right);

    /// <summary>
    /// Calculates the shortest signed difference between this angle and another angle.
    /// The result is normalized between -Pi and Pi radians.
    /// </summary>
    /// <param name="differentAngle">The angle to compare against.</param>
    /// <returns>The shortest signed angular difference.</returns>
    public angle Difference(angle differentAngle)
    {
        angle difference = differentAngle - this;

        while (difference > Pi)
            difference -= Tau;

        while (difference < -Pi)
            difference += Tau;

        return difference;
    }

    /// <summary>
    /// Gets the absolute value of this angle.
    /// </summary>
    /// <returns>A non-negative angle.</returns>
    public angle Abs() =>
        Negative ? -this : this;
    
    /// <summary>
    /// Calculates the sine of this angle.
    /// </summary>
    /// <returns>Sine value of the angle.</returns>
    public double Sin() =>
        Math.Sin(Value);

    /// <summary>
    /// Calculates the cosine of this angle.
    /// </summary>
    /// <returns>Cosine value of the angle.</returns>
    public double Cos() =>
        Math.Cos(Value);

    /// <summary>
    /// Calculates the tangent of this angle.
    /// </summary>
    /// <returns>Tangent value of the angle.</returns>
    public double Tan() =>
        Math.Tan(Value);

    /// <summary>
    /// Creates an angle from degrees.
    /// </summary>
    /// <param name="degrees">Angle value in degrees.</param>
    /// <returns>Angle represented in radians.</returns>
    public static angle FromDegrees(double degrees) =>
        new(degrees * Math.PI / 180.0);

    /// <summary>
    /// Converts this angle from radians to degrees.
    /// </summary>
    /// <returns>Angle value in degrees.</returns>
    public double ToDegrees() =>
        Value * 180.0 / Math.PI;
    
    /// <summary>
    /// Compares this angle with another angle.
    /// </summary>
    /// <param name="other">Angle to compare against.</param>
    /// <returns>A value indicating the relative order of the angles.</returns>
    public int CompareTo(angle other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this angle to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted angle string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} rad";

    /// <summary>
    /// Parses a string into an angle value.
    /// </summary>
    /// <param name="s">The string representation of the angle.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed angle.</returns>
    public static angle Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into an angle value.
    /// </summary>
    /// <param name="s">The string representation of the angle.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed angle if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out angle result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new angle(value);
            return true;
        }

        result = default;
        return false;
    }
}