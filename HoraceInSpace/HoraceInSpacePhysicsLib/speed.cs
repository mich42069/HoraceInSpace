namespace HoraceInSpacePhysicsLib;


public struct speed(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} m/s";

    public static speed operator /(speed s, double scalar) =>
       new speed(s.Value / scalar);

    public static double operator /(speed left, speed right) =>
       left.Value / right.Value;

    public static speed operator *(speed s, double scalar) =>
       new speed(s.Value * scalar);

    public static speed operator *(double scalar, speed s) =>
       new(s.Value * scalar);

    public static speed operator -(speed left, speed right) =>
       new speed(left.Value - right.Value);

    public static speed operator +(speed left, speed right) =>
       new speed(left.Value + right.Value);

    public static bool operator >(speed left, speed right) =>
       left.Value > right.Value;

    public static bool operator <(speed left, speed right) =>
       left.Value < right.Value;

    public static bool operator ==(speed left, speed right) =>
       left.Value == right.Value;

    public static bool operator !=(speed left, speed right) =>
       left.Value != right.Value;

    // speed * time = distance
    public static distance operator *(speed s, time t) =>
       new distance(s.Value * t.Value);

    public static distance operator *(time t, speed s) =>
       new distance(s.Value * t.Value);

    // speed / time = acceleration
    public static acceleration operator /(speed s, time t) =>
       new acceleration(s.Value / t.Value);

    // weight * speed = momentum
    public static momentum operator *(speed s, mass w) =>
       new momentum(s.Value * w.Value);

    public static momentum operator *(mass w, speed s) =>
       new momentum(s.Value * w.Value);
}
