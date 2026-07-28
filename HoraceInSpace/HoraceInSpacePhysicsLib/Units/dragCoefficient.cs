using System.Diagnostics.CodeAnalysis;

namespace HoraceInSpacePhysicsLib.Units;


public readonly struct dragCoefficient(double value) :
    IEquatable<dragCoefficient>,
    IComparable<dragCoefficient>,
    IFormattable,
    IParsable<dragCoefficient>
{
    public double Value { get; } = value;

    public bool Equals(dragCoefficient other) =>
        Value.Equals(other.Value);

    public override bool Equals(object? obj) =>
        obj is dragCoefficient other && Equals(other);

    public override int GetHashCode() =>
        Value.GetHashCode();

    public override string ToString() =>
        $"{Value}";

    public int CompareTo(dragCoefficient other) =>
        Value.CompareTo(other.Value);

    public string ToString(string? format, IFormatProvider? provider) =>
        Value.ToString(format, provider);

    public static dragCoefficient Parse(string s, IFormatProvider? provider) =>
        new(double.Parse(s, provider));

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