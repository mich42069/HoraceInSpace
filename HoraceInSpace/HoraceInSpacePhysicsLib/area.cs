using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib;

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
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(area other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is area other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static area operator /(area a, double scalar) =>
        new(a.Value / scalar);

    public static double operator /(area left, area right) =>
        left.Value / right.Value;

    public static area operator *(area a, double scalar) =>
        new(a.Value * scalar);

    public static area operator *(double scalar, area a) =>
        new(a.Value * scalar);

    public static area operator -(area left, area right) =>
        new(left.Value - right.Value);

    public static area operator +(area left, area right) =>
        new(left.Value + right.Value);

    public static area operator -(area a) =>
        new(-a.Value);

    public area Abs() =>
        Negative ? -this : this;
    
    public static bool operator >(area left, area right) =>
        left.Value > right.Value;

    public static bool operator >=(area left, area right) =>
        left.Value >= right.Value;

    public static bool operator <(area left, area right) =>
        left.Value < right.Value;

    public static bool operator <=(area left, area right) =>
        left.Value <= right.Value;

    public static bool operator ==(area left, area right) =>
        left.Equals(right);

    public static bool operator !=(area left, area right) =>
        !left.Equals(right);
    
    // area / distance = distance
    public static distance operator /(area a, distance d) =>
        new(a.Value / d.Value);

    // area * distance = volume
    public static volume operator *(area a, distance d) =>
        new(a.Value * d.Value);
    
    public distance SquareRoot() =>
        new(Math.Sqrt(Value));
    
    public int CompareTo(area other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m²";

    public static area Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

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