using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

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
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(volume other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is volume other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static volume operator +(volume left, volume right) =>
        new(left.Value + right.Value);

    public static volume operator -(volume left, volume right) =>
        new(left.Value - right.Value);

    public static volume operator *(volume vol, double scalar) =>
        new(vol.Value * scalar);

    public static volume operator *(double scalar, volume vol) =>
        new(vol.Value * scalar);

    public static volume operator /(volume vol, double scalar) =>
        new(vol.Value / scalar);

    public static double operator /(volume left, volume right) => 
        left.Value / right.Value;

    // volume / distance = area
    public static area operator /(volume vol, distance dist) =>
        new(vol.Value / dist.Value);

    public static mass operator *(volume v, density d) =>
        new(d.Value * v.Value);

    public static bool operator >(volume left, volume right) =>
        left.Value > right.Value;

    public static bool operator >=(volume left, volume right) =>
        left.Value >= right.Value;

    public static bool operator <(volume left, volume right) =>
        left.Value < right.Value;

    public static bool operator <=(volume left, volume right) =>
        left.Value <= right.Value;

    public static bool operator ==(volume left, volume right) =>
        left.Value == right.Value;

    public static bool operator !=(volume left, volume right) =>
        left.Value != right.Value;

    public static volume operator -(volume vol) =>
        new(-vol.Value);

    public volume Abs() =>
        Negative ? -this : this;

    public int CompareTo(volume other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m³";

    public static volume Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

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