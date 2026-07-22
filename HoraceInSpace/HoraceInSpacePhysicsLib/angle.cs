namespace HoraceInSpacePhysicsLib;


public struct angle(double value)
{
    public double Value = value;

    public static angle Zero => new(0);
    public static angle Deg90 => FromDegrees(90);
    public static angle Deg180 => FromDegrees(180);
    public static angle Deg270 => FromDegrees(270);
    
    public override string ToString() => $"{Value} rad";

    public static angle operator +(angle left, angle right) =>
        new(left.Value + right.Value);

    public static angle operator -(angle left, angle right) =>
        new(left.Value - right.Value);

    public static angle operator *(angle angle, double scalar) =>
        new(angle.Value * scalar);

    public static angle operator *(double scalar, angle angle) =>
        new(angle.Value * scalar);

    public static angle operator /(angle angle, double scalar) =>
        new(angle.Value / scalar);

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
        left.Value == right.Value;

    public static bool operator !=(angle left, angle right) =>
        left.Value != right.Value;

    public readonly double Sin() => Math.Sin(Value);

    public readonly double Cos() => Math.Cos(Value);

    public readonly double Tan() => Math.Tan(Value);

    public static angle FromDegrees(double degrees) =>
        new(degrees * Math.PI / 180.0);

    public double ToDegrees() =>
        Value * 180.0 / Math.PI;
}