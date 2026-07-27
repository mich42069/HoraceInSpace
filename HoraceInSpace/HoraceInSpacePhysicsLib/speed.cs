using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib;

public readonly struct speed(double value) :
    IEquatable<speed>,
    IComparable<speed>,
    IFormattable,
    IParsable<speed>,
    IAdditionOperators<speed, speed, speed>,
    ISubtractionOperators<speed, speed, speed>,
    IMultiplyOperators<speed, double, speed>,
    IDivisionOperators<speed, double, speed>,
    IDivisionOperators<speed, speed, double>
{
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(speed other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is speed other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static speed operator /(speed s, double scalar) =>
        new(s.Value / scalar);

    public static double operator /(speed left, speed right) =>
        left.Value / right.Value;

    public static speed operator *(speed s, double scalar) =>
        new(s.Value * scalar);

    public static speed operator *(double scalar, speed s) =>
        new(s.Value * scalar);

    public static speed operator -(speed left, speed right) =>
        new(left.Value - right.Value);

    public static speed operator +(speed left, speed right) =>
        new(left.Value + right.Value);

    public static speed operator -(speed s) =>
        new(-s.Value);

    public speed Abs() =>
        Negative ? -this : this;

    public static bool operator >(speed left, speed right) =>
        left.Value > right.Value;

    public static bool operator >=(speed left, speed right) =>
        left.Value >= right.Value;

    public static bool operator <(speed left, speed right) =>
        left.Value < right.Value;

    public static bool operator <=(speed left, speed right) =>
        left.Value <= right.Value;

    public static bool operator ==(speed left, speed right) =>
        left.Value == right.Value;

    public static bool operator !=(speed left, speed right) =>
        left.Value != right.Value;
    
    // speed * time = distance
    public static distance operator *(speed s, TimeSpan t) =>
        new(s.Value * t.TotalSeconds);

    // speed / time = acceleration
    public static acceleration operator /(speed s, TimeSpan t) =>
        new(s.Value / t.TotalSeconds);

    // speed * mass = momentum
    public static momentum operator *(speed s, mass m) =>
        new(s.Value * m.Value);
    
    public int CompareTo(speed other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} m/s";

    public static speed Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out speed result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new speed(value);
            return true;
        }

        result = default;
        return false;
    }
}