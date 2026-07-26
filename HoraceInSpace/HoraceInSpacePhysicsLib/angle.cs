namespace HoraceInSpacePhysicsLib;


public struct angle(double value)
{
    public double Value = value;
    public bool Negative => Value < 0;
    public static angle Deg0 => FromDegrees(0);
    public static angle Deg90 => FromDegrees(90);
    public static angle Deg180 => FromDegrees(180);
    public static angle Deg270 => FromDegrees(270);
    public static angle Pi => Math.PI.Radians();
    public static angle Tau => Math.Tau.Radians();
    
    public override string ToString() => $"{Value} rad";

    public angle Difference(angle differentAngle)
    {
        angle difference = differentAngle - this;

        while (difference > Pi)
            difference -= Tau;

        while (difference < -Pi)
            difference += Tau;

        return difference;
    }
    
    public angle Abs() => Negative ? -this : this;

    public static angle operator +(angle left, angle right) =>
        new(left.Value + right.Value);

    public static angle operator -(angle left, angle right) =>
        new(left.Value - right.Value);
    public static angle operator -(angle left) =>
        new(-left.Value);

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