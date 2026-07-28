using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

public readonly struct momentum(double value) :
    IEquatable<momentum>,
    IComparable<momentum>,
    IFormattable,
    IParsable<momentum>,
    IAdditionOperators<momentum, momentum, momentum>,
    ISubtractionOperators<momentum, momentum, momentum>,
    IMultiplyOperators<momentum, double, momentum>,
    IDivisionOperators<momentum, double, momentum>,
    IDivisionOperators<momentum, momentum, double>
{
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(momentum other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is momentum other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static momentum operator /(momentum m, double scalar) =>
        new(m.Value / scalar);

    public static double operator /(momentum left, momentum right) =>
        left.Value / right.Value;

    public static momentum operator *(momentum m, double scalar) =>
        new(m.Value * scalar);

    public static momentum operator *(double scalar, momentum m) =>
        new(m.Value * scalar);

    public static momentum operator +(momentum left, momentum right) =>
        new(left.Value + right.Value);

    public static momentum operator -(momentum left, momentum right) =>
        new(left.Value - right.Value);

    public static momentum operator -(momentum m) =>
        new(-m.Value);

    public momentum Abs() =>
        Negative ? -this : this;

    public static bool operator >(momentum left, momentum right) =>
        left.Value > right.Value;

    public static bool operator >=(momentum left, momentum right) =>
        left.Value >= right.Value;

    public static bool operator <(momentum left, momentum right) =>
        left.Value < right.Value;

    public static bool operator <=(momentum left, momentum right) =>
        left.Value <= right.Value;

    public static bool operator ==(momentum left, momentum right) =>
        left.Equals(right);

    public static bool operator !=(momentum left, momentum right) =>
        !left.Equals(right);


    // momentum / mass = speed
    public static speed operator /(momentum m, mass w) =>
        new(m.Value / w.Value);

    // momentum / speed = mass
    public static mass operator /(momentum m, speed s) =>
        new(m.Value / s.Value);
    
    public int CompareTo(momentum other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} kg·m/s";

    public static momentum Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out momentum result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new momentum(value);
            return true;
        }

        result = default;
        return false;
    }
}