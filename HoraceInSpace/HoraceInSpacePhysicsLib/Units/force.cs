using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Force, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit</param>
public readonly struct force(double value) :
    IEquatable<force>,
    IComparable<force>,
    IFormattable,
    IParsable<force>,
    IAdditionOperators<force, force, force>,
    ISubtractionOperators<force, force, force>,
    IMultiplyOperators<force, double, force>,
    IDivisionOperators<force, double, force>,
    IDivisionOperators<force, force, double>
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
    /// Returns true if units of force have same Value.
    /// </summary>
    /// <param name="other">Force to compare.</param>
    /// <returns>true if equal Value. False otherwise.</returns>
    public bool Equals(force other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are force and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and of equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is force other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the Value and sign information of this force.
    /// </summary>
    /// <returns>Correctly formatted string.</returns>
    public override string ToString() =>
        $"{Value} N";

    /// <summary>
    /// Adds two forces together.
    /// </summary>
    /// <param name="left">Left force.</param>
    /// <param name="right">Right force.</param>
    /// <returns>Sum of both forces.</returns>
    public static force operator +(force left, force right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Subtracts one force from another.
    /// </summary>
    /// <param name="left">Left force.</param>
    /// <param name="right">Right force.</param>
    /// <returns>Difference of both forces.</returns>
    public static force operator -(force left, force right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Multiplies force by a double.
    /// </summary>
    /// <param name="f">Input force.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Force multiplied by given scalar.</returns>
    public static force operator *(force f, double scalar) =>
        new(f.Value * scalar);

    /// <summary>
    /// Multiplies force by a double.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="f">Input force.</param>
    /// <returns>Force multiplied by given scalar.</returns>
    public static force operator *(double scalar, force f) =>
        new(f.Value * scalar);

    /// <summary>
    /// Divides force by a double.
    /// </summary>
    /// <param name="f">Input force.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Force divided by a scalar.</returns>
    public static force operator /(force f, double scalar) =>
        new(f.Value / scalar);

    /// <summary>
    /// Returns a ratio of two forces.
    /// </summary>
    /// <param name="left">Left force.</param>
    /// <param name="right">Right force.</param>
    /// <returns>Ratio of left to right force.</returns>
    public static double operator /(force left, force right) =>
        left.Value / right.Value;

    /// <summary>
    /// Calculates mass from force and acceleration.
    /// </summary>
    /// <param name="f">The force.</param>
    /// <param name="a">The acceleration.</param>
    /// <returns>The resulting mass.</returns>
    public static mass operator /(force f, acceleration a) =>
        new(f.Value / a.Value);

    /// <summary>
    /// Calculates acceleration from force and mass.
    /// </summary>
    /// <param name="f">The force.</param>
    /// <param name="m">The mass.</param>
    /// <returns>The resulting acceleration.</returns>
    // force / mass = acceleration
    public static acceleration operator /(force f, mass m) =>
        new(f.Value / m.Value);

    /// <summary>
    /// Negates the force value.
    /// </summary>
    /// <param name="f">Force to negate.</param>
    /// <returns>A force with the opposite sign.</returns>
    public static force operator -(force f) =>
        new(-f.Value);

    /// <summary>
    /// Gets the absolute value of this force.
    /// </summary>
    /// <returns>A non-negative force.</returns>
    public force Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Determines whether one force is greater than another.
    /// </summary>
    /// <param name="left">The first force to compare.</param>
    /// <param name="right">The second force to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(force left, force right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one force is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first force to compare.</param>
    /// <param name="right">The second force to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(force left, force right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one force is less than another.
    /// </summary>
    /// <param name="left">The first force to compare.</param>
    /// <param name="right">The second force to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(force left, force right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one force is less than or equal to another.
    /// </summary>
    /// <param name="left">The first force to compare.</param>
    /// <param name="right">The second force to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(force left, force right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two forces are equal.
    /// </summary>
    /// <param name="left">The first force to compare.</param>
    /// <param name="right">The second force to compare.</param>
    /// <returns><c>true</c> if the forces have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(force left, force right) =>
        left.Value == right.Value;

    /// <summary>
    /// Determines whether two forces are not equal.
    /// </summary>
    /// <param name="left">The first force to compare.</param>
    /// <param name="right">The second force to compare.</param>
    /// <returns><c>true</c> if the forces have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(force left, force right) =>
        left.Value != right.Value;

    /// <summary>
    /// Compares this force with another force.
    /// </summary>
    /// <param name="other">The force to compare against.</param>
    /// <returns>A value indicating the relative order of the forces.</returns>
    public int CompareTo(force other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this force to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted force string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} N";

    /// <summary>
    /// Parses a string into a force value.
    /// </summary>
    /// <param name="s">The string representation of the force.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed force.</returns>
    public static force Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a force value.
    /// </summary>
    /// <param name="s">The string representation of the force.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed force if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out force result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new force(value);
            return true;
        }

        result = default;
        return false;
    }

    /// <summary>
    /// Calculates aerodynamic drag force using the drag equation.
    /// </summary>
    /// <param name="airDensity">The density of the surrounding fluid.</param>
    /// <param name="dragCoefficient">The drag coefficient of the object.</param>
    /// <param name="crossSection">The cross-sectional area of the object.</param>
    /// <param name="velocity">The velocity of the object through the fluid.</param>
    /// <returns>The resulting drag force.</returns>
    public static force AtmosphericDrag(
        density airDensity,
        dragCoefficient dragCoefficient,
        area crossSection,
        speed velocity)
    {
        double magnitude =
            0.5 *
            airDensity.Value *
            velocity.Value * velocity.Value *
            dragCoefficient.Value *
            crossSection.Value;

        return new force(magnitude);
    }
}