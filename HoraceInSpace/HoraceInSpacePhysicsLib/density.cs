namespace HoraceInSpacePhysicsLib;

public struct density(double value)
{
    public double Value = value;

    public override string ToString() => $"{Value} kg/m³";

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

    public static bool operator >(density left, density right) =>
        left.Value > right.Value;

    public static bool operator >=(density left, density right) =>
        left.Value >= right.Value;

    public static bool operator <(density left, density right) =>
        left.Value < right.Value;

    public static bool operator <=(density left, density right) =>
        left.Value <= right.Value;

    public static bool operator ==(density left, density right) =>
        left.Value == right.Value;

    public static bool operator !=(density left, density right) =>
        left.Value != right.Value;

}