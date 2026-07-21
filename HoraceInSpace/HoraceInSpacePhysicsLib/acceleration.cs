namespace HoraceInSpacePhysicsLib;


public struct acceleration(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} m/s^2";

    public static acceleration operator /(acceleration a, double scalar) =>
       new acceleration(a.Value / scalar);

    public static double operator /(acceleration left, acceleration right) =>
       left.Value / right.Value;

    public static acceleration operator *(acceleration a, double scalar) =>
       new acceleration(a.Value * scalar);

    public static acceleration operator *(double scalar, acceleration a) =>
       new(a.Value * scalar);

    public static acceleration operator -(acceleration left, acceleration right) =>
       new acceleration(left.Value - right.Value);

    public static acceleration operator +(acceleration left, acceleration right) =>
       new acceleration(left.Value + right.Value);

    public static bool operator >(acceleration left, acceleration right) =>
       left.Value > right.Value;

    public static bool operator <(acceleration left, acceleration right) =>
       left.Value < right.Value;

    public static bool operator ==(acceleration left, acceleration right) =>
       left.Value == right.Value;

    public static bool operator !=(acceleration left, acceleration right) =>
       left.Value != right.Value;

    // acceleration * time = speed
    public static speed operator *(acceleration a, time t) =>
       new speed(a.Value * t.Value);

    public static speed operator *(time t, acceleration a) =>
       new speed(a.Value * t.Value);

    // weight * acceleration = force (represented in newtons via momentum-style struct not needed here)
}