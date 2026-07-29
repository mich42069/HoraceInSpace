using System.Diagnostics.CodeAnalysis;

namespace HoraceInSpacePhysicsLib.Units;

/// <summary>
/// Represents the drag coefficient used in aerodynamic calculations.
/// Allows comparison, formatting, and parsing of drag coefficient values.
/// </summary>
/// <param name="value">Value that represents the drag coefficient.</param>
public readonly struct dragCoefficient(double value) :
    IEquatable<dragCoefficient>,
    IComparable<dragCoefficient>,
    IFormattable,
    IParsable<dragCoefficient>
{
    /// <summary>
    /// Stores the underlying value of this unit as a double.
    /// </summary>
    public double Value { get; } = value;

    /// <summary>
    /// Returns true if two drag coefficients have the same value.
    /// </summary>
    /// <param name="other">Drag coefficient to compare.</param>
    /// <returns>True if values are equal. False otherwise.</returns>
    public bool Equals(dragCoefficient other) =>
        Value.Equals(other.Value);

    /// <summary>
    /// Compares this object with another object.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns>True if both objects are drag coefficients with equal values. False otherwise.</returns>
    public override bool Equals(object? obj) =>
        obj is dragCoefficient other && Equals(other);

    /// <summary>
    /// Generates a hash code from the stored value.
    /// </summary>
    /// <returns>Generated hash code of type int.</returns>
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <summary>
    /// Returns the string representation of the drag coefficient.
    /// </summary>
    /// <returns>Formatted drag coefficient value.</returns>
    public override string ToString() =>
        $"{Value}";

    /// <summary>
    /// Compares this drag coefficient with another drag coefficient.
    /// </summary>
    /// <param name="other">Drag coefficient to compare against.</param>
    /// <returns>A value indicating the relative order of the values.</returns>
    public int CompareTo(dragCoefficient other) =>
        Value.CompareTo(other.Value);

    /// <summary>
    /// Converts this drag coefficient to a string using the specified format.
    /// </summary>
    /// <param name="format">Numeric format string.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>Formatted drag coefficient string.</returns>
    public string ToString(string? format, IFormatProvider? provider) =>
        Value.ToString(format, provider);

    /// <summary>
    /// Parses a string into a drag coefficient value.
    /// </summary>
    /// <param name="s">String representation of the drag coefficient.</param>
    /// <param name="provider">Format provider.</param>
    /// <returns>The parsed drag coefficient.</returns>
    public static dragCoefficient Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    /// <summary>
    /// Attempts to parse a string into a drag coefficient value.
    /// </summary>
    /// <param name="s">String representation of the drag coefficient.</param>
    /// <param name="provider">Format provider.</param>
    /// <param name="result">When this method returns, contains the parsed drag coefficient if successful.</param>
    /// <returns><c>true</c> if parsing succeeded; otherwise, <c>false</c>.</returns>
    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out dragCoefficient result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new dragCoefficient(value);
            return true;
        }

        result = default;
        return false;
    }
}