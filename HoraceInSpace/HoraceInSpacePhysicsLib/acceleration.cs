using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib;

public readonly struct acceleration(double value) :
    IEquatable<acceleration>,
    IComparable<acceleration>,
    IFormattable,
    IParsable<acceleration>,
    IAdditionOperators<acceleration, acceleration, acceleration>,
    ISubtractionOperators<acceleration, acceleration, acceleration>,
    IMultiplyOperators<acceleration, double, acceleration>,
    IDivisionOperators<acceleration, double, acceleration>,
    IDivisionOperators<acceleration, acceleration, double>
{
    public double Value { get; } = value;

    public bool Negative => Value < 0;


    public bool Equals(acceleration other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is acceleration other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";


    public static acceleration operator /(acceleration a, double scalar) =>
        new(a.Value / scalar);

    public static double operator /(acceleration left, acceleration right) =>
        left.Value / right.Value;

    public static acceleration operator *(acceleration a, double scalar) =>
        new(a.Value * scalar);

    public static acceleration operator *(double scalar, acceleration a) =>
        new(a.Value * scalar);

    public static acceleration operator +(acceleration left, acceleration right) =>
        new(left.Value + right.Value);

    public static acceleration operator -(acceleration left, acceleration right) =>
        new(left.Value - right.Value);

    public static acceleration operator -(acceleration a) =>
        new(-a.Value);

    public acceleration Abs() =>
        Negative ? -this : this;


    public static bool operator >(acceleration left, acceleration right) =>
        left.Value > right.Value;

    public static bool operator >=(acceleration left, acceleration right) =>
        left.Value >= right.Value;

    public static bool operator <(acceleration left, acceleration right) =>
        left.Value < right.Value;

    public static bool operator <=(acceleration left, acceleration right) =>
        left.Value <= right.Value;

    public static bool operator ==(acceleration left, acceleration right) =>
        left.Equals(right);

    public static bool operator !=(acceleration left, acceleration right) =>
        !left.Equals(right);


    // acceleration * time = speed
    public static speed operator *(acceleration a, TimeSpan t) =>
        new(a.Value * t.TotalSeconds);


    public int CompareTo(acceleration other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m/s²";

    public static acceleration Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out acceleration result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new acceleration(value);
            return true;
        }

        result = default;
        return false;
    }
}