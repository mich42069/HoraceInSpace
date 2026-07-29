using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Is the Unit of Mass, allows for operations between itself and few other units based on given physics formulas.
/// </summary>
/// <param name="value">Value that represents the Unit</param>
public readonly struct mass(double value) :
    IEquatable<mass>,
    IComparable<mass>,
    IFormattable,
    IParsable<mass>,
    IAdditionOperators<mass, mass, mass>,
    ISubtractionOperators<mass, mass, mass>,
    IMultiplyOperators<mass, double, mass>,
    IDivisionOperators<mass, double, mass>,
    IDivisionOperators<mass, mass, double>
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
    /// Returns true if units of mass have same Value.
    /// </summary>
    /// <param name="other">Mass to compare.</param>
    /// <returns>true if equal Value. False otherwise.</returns>
    public bool Equals(mass other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares two objects. True only if both are mass and equal.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both same type and of equal Value. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is mass other && Equals(other);

    /// <summary>
    /// Hash code generated from Value.
    /// </summary>
    /// <returns>Generated HashCode of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the Value and sign information of this mass.
    /// </summary>
    /// <returns>Correctly formatted string.</returns>
    public override string ToString() =>
        $"{Value} kg";

    /// <summary>
    /// Divides mass by a double.
    /// </summary>
    /// <param name="m">Input mass.</param>
    /// <param name="scalar">Divisor double.</param>
    /// <returns>Mass divided by a scalar.</returns>
    public static mass operator /(mass m, double scalar) =>
        new(m.Value / scalar);

    /// <summary>
    /// Returns a ratio of two masses.
    /// </summary>
    /// <param name="left">Left mass.</param>
    /// <param name="right">Right mass.</param>
    /// <returns>Ratio of left to right mass.</returns>
    public static double operator /(mass left, mass right) =>
        left.Value / right.Value;

    /// <summary>
    /// Multiplies mass by a double.
    /// </summary>
    /// <param name="m">Input mass.</param>
    /// <param name="scalar">Multiplier double.</param>
    /// <returns>Mass multiplied by given scalar.</returns>
    public static mass operator *(mass m, double scalar) =>
        new(m.Value * scalar);

    /// <summary>
    /// Multiplies mass by a double.
    /// </summary>
    /// <param name="scalar">Multiplier double.</param>
    /// <param name="m">Input mass.</param>
    /// <returns>Mass multiplied by given scalar.</returns>
    public static mass operator *(double scalar, mass m) =>
        new(m.Value * scalar);

    /// <summary>
    /// Subtracts one mass from another.
    /// </summary>
    /// <param name="left">Left mass.</param>
    /// <param name="right">Right mass.</param>
    /// <returns>Difference of both masses.</returns>
    public static mass operator -(mass left, mass right) =>
        new(left.Value - right.Value);

    /// <summary>
    /// Adds two masses together.
    /// </summary>
    /// <param name="left">Left mass.</param>
    /// <param name="right">Right mass.</param>
    /// <returns>Sum of both masses.</returns>
    public static mass operator +(mass left, mass right) =>
        new(left.Value + right.Value);

    /// <summary>
    /// Negates the mass value.
    /// </summary>
    /// <param name="m">Mass to negate.</param>
    /// <returns>A mass with the opposite sign.</returns>
    public static mass operator -(mass m) =>
        new(-m.Value);

    /// <summary>
    /// Gets the absolute value of this mass.
    /// </summary>
    /// <returns>A non-negative mass.</returns>
    public mass Abs() =>
        Negative ? -this : this;

    /// <summary>
    /// Determines whether one mass is greater than another.
    /// </summary>
    /// <param name="left">The first mass to compare.</param>
    /// <param name="right">The second mass to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(mass left, mass right) =>
        left.Value > right.Value;

    /// <summary>
    /// Determines whether one mass is greater than or equal to another.
    /// </summary>
    /// <param name="left">The first mass to compare.</param>
    /// <param name="right">The second mass to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(mass left, mass right) =>
        left.Value >= right.Value;

    /// <summary>
    /// Determines whether one mass is less than another.
    /// </summary>
    /// <param name="left">The first mass to compare.</param>
    /// <param name="right">The second mass to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(mass left, mass right) =>
        left.Value < right.Value;

    /// <summary>
    /// Determines whether one mass is less than or equal to another.
    /// </summary>
    /// <param name="left">The first mass to compare.</param>
    /// <param name="right">The second mass to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(mass left, mass right) =>
        left.Value <= right.Value;

    /// <summary>
    /// Determines whether two masses are equal.
    /// </summary>
    /// <param name="left">The first mass to compare.</param>
    /// <param name="right">The second mass to compare.</param>
    /// <returns><c>true</c> if the masses have the same value; otherwise, <c>false</c>.</returns>
    public static bool operator ==(mass left, mass right) =>
        left.Value == right.Value;

    /// <summary>
    /// Determines whether two masses are not equal.
    /// </summary>
    /// <param name="left">The first mass to compare.</param>
    /// <param name="right">The second mass to compare.</param>
    /// <returns><c>true</c> if the masses have different values; otherwise, <c>false</c>.</returns>
    public static bool operator !=(mass left, mass right) =>
        left.Value != right.Value;

    /// <summary>
    /// Calculates volume from mass and density.
    /// </summary>
    /// <param name="m">The mass.</param>
    /// <param name="d">The density.</param>
    /// <returns>The resulting volume.</returns>
    // mass / density = volume
    public static volume operator /(mass m, density d) =>
        new(m.Value / d.Value);

    /// <summary>
    /// Calculates force from mass and acceleration.
    /// </summary>
    /// <param name="m">The mass.</param>
    /// <param name="a">The acceleration.</param>
    /// <returns>The resulting force.</returns>
    // mass * acceleration = force
    public static force operator *(mass m, acceleration a) =>
        new(m.Value * a.Value);

    /// <summary>
    /// Calculates momentum from mass and speed.
    /// </summary>
    /// <param name="m">The mass.</param>
    /// <param name="s">The speed.</param>
    /// <returns>The resulting momentum.</returns>
    // mass * speed = momentum
    public static momentum operator *(mass m, speed s) =>
        new(m.Value * s.Value);

    /// <summary>
    /// Compares this mass with another mass.
    /// </summary>
    /// <param name="other">The mass to compare against.</param>
    /// <returns>A value indicating the relative order of the masses.</returns>
    public int CompareTo(mass other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this mass to its string representation using the specified format.
    /// </summary>
    /// <param name="format">The numeric format string.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The formatted mass string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} kg";

    /// <summary>
    /// Parses a string into a mass value.
    /// </summary>
    /// <param name="s">The string representation of the mass.</param>
    /// <param name="provider">The format provider.</param>
    /// <returns>The parsed mass.</returns>
    public static mass Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a mass value.
    /// </summary>
    /// <param name="s">The string representation of the mass.</param>
    /// <param name="provider">The format provider.</param>
    /// <param name="result">When this method returns, contains the parsed mass if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out mass result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new mass(value);
            return true;
        }

        result = default;
        return false;
    }
}