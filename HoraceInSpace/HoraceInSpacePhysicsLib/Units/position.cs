using System.Numerics;

namespace HoraceInSpacePhysicsLib.Units;

public readonly struct position(distance x, distance y) :
    IEquatable<position>,
    IFormattable,
    IAdditionOperators<position, position, position>,
    ISubtractionOperators<position, position, position>,
    IMultiplyOperators<position, double, position>,
    IDivisionOperators<position, double, position>
{
    public distance X { get; } = x;
    public distance Y { get; } = y;

    public override string ToString() =>
        $"({X}, {Y})";

    public bool Equals(position other) =>
        X.Equals(other.X) &&
        Y.Equals(other.Y);

    public override bool Equals(object? obj) =>
        obj is position other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(X, Y);

    public static position operator +(position left, position right) =>
        new(left.X + right.X, left.Y + right.Y);

    public static position operator -(position left, position right) =>
        new(left.X - right.X, left.Y - right.Y);

    public static position operator *(position pos, double scalar) =>
        new(pos.X * scalar, pos.Y * scalar);

    public static position operator *(double scalar, position pos) =>
        new(pos.X * scalar, pos.Y * scalar);

    public static position operator /(position pos, double scalar) =>
        new(pos.X / scalar, pos.Y / scalar);

    public static bool operator ==(position left, position right) =>
        left.Equals(right);

    public static bool operator !=(position left, position right) =>
        !left.Equals(right);

    public static position operator %(position left, position right) =>
        new(left.X % right.X, left.Y % right.Y);
    
    public distance DistanceTo(position other)
    {
        distance dx = X - other.X;
        distance dy = Y - other.Y;

        return new distance(
            Math.Sqrt(
                dx.Value * dx.Value +
                dy.Value * dy.Value));
    }

    public string ToString(string? format, IFormatProvider? provider) =>
        $"({X.ToString(format, provider)}, {Y.ToString(format, provider)})";
}