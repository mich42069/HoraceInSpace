using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

public readonly struct angle(double value) :
    IEquatable<angle>,
    IComparable<angle>,
    IFormattable,
    IParsable<angle>,
    IAdditionOperators<angle, angle, angle>,
    ISubtractionOperators<angle, angle, angle>,
    IMultiplyOperators<angle, double, angle>,
    IDivisionOperators<angle, double, angle>,
    IDivisionOperators<angle, angle, double>
{
    public double Value { get; } = value;

    public bool Negative => Value < 0;

    public static angle Deg0 => FromDegrees(0);
    public static angle Deg90 => FromDegrees(90);
    public static angle Deg180 => FromDegrees(180);
    public static angle Deg270 => FromDegrees(270);

    public static angle Pi => new (Math.PI);
    public static angle Tau => new (Math.Tau);
    
    public bool Equals(angle other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is angle other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{nameof(Value)}: {Value} rad";
    
    public static angle operator +(angle left, angle right) =>
        new(left.Value + right.Value);

    public static angle operator -(angle left, angle right) =>
        new(left.Value - right.Value);

    public static angle operator -(angle a) =>
        new(-a.Value);

    public static angle operator *(angle a, double scalar) =>
        new(a.Value * scalar);

    public static angle operator *(double scalar, angle a) =>
        new(a.Value * scalar);

    public static angle operator /(angle a, double scalar) =>
        new(a.Value / scalar);

    public static double operator /(angle left, angle right) =>
        left.Value / right.Value;

    public static bool operator >(angle left, angle right) =>
        left.Value > right.Value;

    public static bool operator >=(angle left, angle right) =>
        left.Value >= right.Value;

    public static bool operator <(angle left, angle right) =>
        left.Value < right.Value;

    public static bool operator <=(angle left, angle right) =>
        left.Value <= right.Value;

    public static bool operator ==(angle left, angle right) =>
        left.Equals(right);

    public static bool operator !=(angle left, angle right) =>
        !left.Equals(right);

    public angle Difference(angle differentAngle)
    {
        angle difference = differentAngle - this;

        while (difference > Pi)
            difference -= Tau;

        while (difference < -Pi)
            difference += Tau;

        return difference;
    }

    public angle Abs() =>
        Negative ? -this : this;
    
    public double Sin() =>
        Math.Sin(Value);

    public double Cos() =>
        Math.Cos(Value);

    public double Tan() =>
        Math.Tan(Value);

    public static angle FromDegrees(double degrees) =>
        new(degrees * Math.PI / 180.0);

    public double ToDegrees() =>
        Value * 180.0 / Math.PI;
    
    public int CompareTo(angle other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        $"{Value.ToString(format, provider)} rad";

    public static angle Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        out angle result)
    {
        if (double.TryParse(s, provider, out double value))
        {
            result = new angle(value);
            return true;
        }

        result = default;
        return false;
    }
}