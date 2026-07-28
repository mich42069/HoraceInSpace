using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

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
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(mass other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is mass other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static mass operator /(mass m, double scalar) =>
        new(m.Value / scalar);

    public static double operator /(mass left, mass right) =>
        left.Value / right.Value;

    public static mass operator *(mass m, double scalar) =>
        new(m.Value * scalar);

    public static mass operator *(double scalar, mass m) =>
        new(m.Value * scalar);

    public static mass operator -(mass left, mass right) =>
        new(left.Value - right.Value);

    public static mass operator +(mass left, mass right) =>
        new(left.Value + right.Value);

    public static mass operator -(mass m) =>
        new(-m.Value);

    public mass Abs() =>
        Negative ? -this : this;

    public static bool operator >(mass left, mass right) =>
        left.Value > right.Value;

    public static bool operator >=(mass left, mass right) =>
        left.Value >= right.Value;

    public static bool operator <(mass left, mass right) =>
        left.Value < right.Value;

    public static bool operator <=(mass left, mass right) =>
        left.Value <= right.Value;

    public static bool operator ==(mass left, mass right) =>
        left.Value == right.Value;

    public static bool operator !=(mass left, mass right) =>
        left.Value != right.Value;

    // mass / density = volume
    public static volume operator /(mass m, density d) =>
        new(m.Value / d.Value);

    // mass * acceleration = force
    public static force operator *(mass m, acceleration a) =>
        new(m.Value * a.Value);
    
    // mass * speed = momentum
    public static momentum operator *(mass m, speed s) =>
        new(m.Value * s.Value);

    public int CompareTo(mass other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} kg";

    public static mass Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

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