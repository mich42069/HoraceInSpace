namespace HoraceInSpacePhysicsLib;


public struct weight(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} kg";

    public static weight operator /(weight w, double scalar) =>
       new weight(w.Value / scalar);

    public static double operator /(weight left, weight right) =>
       left.Value / right.Value;

    public static weight operator *(weight w, double scalar) =>
       new weight(w.Value * scalar);

    public static weight operator *(double scalar, weight w) =>
       new(w.Value * scalar);

    public static weight operator -(weight left, weight right) =>
       new weight(left.Value - right.Value);

    public static weight operator +(weight left, weight right) =>
       new weight(left.Value + right.Value);

    public static bool operator >(weight left, weight right) =>
       left.Value > right.Value;

    public static bool operator <(weight left, weight right) =>
       left.Value < right.Value;

    public static bool operator ==(weight left, weight right) =>
       left.Value == right.Value;

    public static bool operator !=(weight left, weight right) =>
       left.Value != right.Value;
}

