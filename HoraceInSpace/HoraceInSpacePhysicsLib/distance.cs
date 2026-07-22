namespace HoraceInSpacePhysicsLib;


public struct distance(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} m";

    public static distance operator /(distance dist, double scalar) =>
       new(dist.Value / scalar);

    public static double operator /(distance left, distance rightDistance) =>
       left.Value / rightDistance.Value;

    public static distance operator *(distance dist, double scalar) =>
       new(dist.Value * scalar);

    public static distance operator *(double scalar, distance dist) =>
       new(dist.Value * scalar);

    public static distance operator -(distance left, distance right) =>
       new(left.Value - right.Value);

    public static distance operator +(distance left, distance right) =>
       new(left.Value + right.Value);

    public static bool operator >(distance left, distance right) =>
       left.Value > right.Value;
    
    public static bool operator >=(distance left, distance right) =>
       left.Value > right.Value;

    public static bool operator <(distance left, distance right) =>
       left.Value < right.Value;
    
    public static bool operator <=(distance left, distance right) =>
       left.Value <= right.Value;

    public static bool operator ==(distance left, distance right) =>
       left.Value == right.Value;

    public static bool operator !=(distance left, distance right) =>
       left.Value != right.Value;

    public static distance operator %(distance left, distance right)
    {
       while (left.Value < 0) left.Value += right.Value;
       return new(left.Value % right.Value);
    }

    // distance / time = speed
    public static speed operator /(distance dist, TimeSpan t) =>
       new speed(dist.Value / t.TotalSeconds);
    
    // distance * distance = area
    public static area operator *(distance left, distance right) =>
       new area(left.Value * right.Value);
}
