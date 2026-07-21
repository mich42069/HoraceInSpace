namespace HoraceInSpacePhysicsLib;



public struct momentum(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} kg·m/s";

    public static momentum operator /(momentum m, double scalar) =>
       new momentum(m.Value / scalar);

    public static double operator /(momentum left, momentum right) =>
       left.Value / right.Value;

    public static momentum operator *(momentum m, double scalar) =>
       new momentum(m.Value * scalar);

    public static momentum operator *(double scalar, momentum m) =>
       new(m.Value * scalar);

    public static momentum operator -(momentum left, momentum right) =>
       new momentum(left.Value - right.Value);

    public static momentum operator +(momentum left, momentum right) =>
       new momentum(left.Value + right.Value);

    public static bool operator >(momentum left, momentum right) =>
       left.Value > right.Value;

    public static bool operator <(momentum left, momentum right) =>
       left.Value < right.Value;

    public static bool operator ==(momentum left, momentum right) =>
       left.Value == right.Value;

    public static bool operator !=(momentum left, momentum right) =>
       left.Value != right.Value;

    // momentum / weight = speed
    public static speed operator /(momentum m, mass w) =>
       new speed(m.Value / w.Value);

    // momentum / speed = weight
    public static mass operator /(momentum m, speed s) =>
       new mass(m.Value / s.Value);
}
