using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib;

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
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public bool Equals(force other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is force other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value}, {nameof(Negative)}: {Negative}";

    public static force operator +(force left, force right) =>
        new(left.Value + right.Value);

    public static force operator -(force left, force right) =>
        new(left.Value - right.Value);

    public static force operator *(force f, double scalar) =>
        new(f.Value * scalar);

    public static force operator *(double scalar, force f) =>
        new(f.Value * scalar);

    public static force operator /(force f, double scalar) =>
        new(f.Value / scalar);
    
    public static double operator /(force left, force right) =>
        left.Value / right.Value;

    public static mass operator /(force f, acceleration a) =>
        new(f.Value / a.Value);
    
    // force / mass = acceleration
    public static acceleration operator /(force f, mass m) =>
        new(f.Value / m.Value);

    public static force operator -(force f) =>
        new(-f.Value);

    public force Abs() =>
        Negative ? -this : this;

    public static bool operator >(force left, force right) =>
        left.Value > right.Value;

    public static bool operator >=(force left, force right) =>
        left.Value >= right.Value;

    public static bool operator <(force left, force right) =>
        left.Value < right.Value;

    public static bool operator <=(force left, force right) =>
        left.Value <= right.Value;

    public static bool operator ==(force left, force right) =>
        left.Value == right.Value;

    public static bool operator !=(force left, force right) =>
        left.Value != right.Value;

    public int CompareTo(force other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} N";

    public static force Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

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
