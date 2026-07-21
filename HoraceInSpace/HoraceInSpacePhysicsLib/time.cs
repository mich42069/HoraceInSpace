namespace HoraceInSpacePhysicsLib;


public struct time(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} s";

    public static time operator /(time t, double scalar) =>
       new time(t.Value / scalar);

    public static double operator /(time left, time right) =>
       left.Value / right.Value;

    public static time operator *(time t, double scalar) =>
       new time(t.Value * scalar);

    public static time operator *(double scalar, time t) =>
       new(t.Value * scalar);

    public static time operator -(time left, time right) =>
       new time(left.Value - right.Value);

    public static time operator +(time left, time right) =>
       new time(left.Value + right.Value);

    public static bool operator >(time left, time right) =>
       left.Value > right.Value;

    public static bool operator <(time left, time right) =>
       left.Value < right.Value;

    public static bool operator ==(time left, time right) =>
       left.Value == right.Value;

    public static bool operator !=(time left, time right) =>
       left.Value != right.Value;
}
