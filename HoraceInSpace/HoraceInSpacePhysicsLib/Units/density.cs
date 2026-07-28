using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

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
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(density other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is density other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static density operator +(density left, density right) =>
        new(left.Value + right.Value);

    public static density operator -(density left, density right) =>
        new(left.Value - right.Value);

    public static density operator *(density d, double scalar) =>
        new(d.Value * scalar);

    public static density operator *(double scalar, density d) =>
        new(d.Value * scalar);

    public static density operator /(density d, double scalar) =>
        new(d.Value / scalar);
    
    public static double operator /(density left, density right) =>
        left.Value / right.Value;

    public static density operator -(density d) =>
        new(-d.Value);

    public density Abs() =>
        Negative ? -this : this;
    
    // density * volume = mass
    public static mass operator *(density d, volume v) =>
        new(d.Value * v.Value);

    public static bool operator >(density left, density right) =>
        left.Value > right.Value;

    public static bool operator >=(density left, density right) =>
        left.Value >= right.Value;

    public static bool operator <(density left, density right) =>
        left.Value < right.Value;

    public static bool operator <=(density left, density right) =>
        left.Value <= right.Value;

    public static bool operator ==(density left, density right) =>
        left.Equals(right);

    public static bool operator !=(density left, density right) =>
        !left.Equals(right);
    
    public int CompareTo(density other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} kg/m³";

    public static density Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

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