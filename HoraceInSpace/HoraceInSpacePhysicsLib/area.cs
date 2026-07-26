namespace HoraceInSpacePhysicsLib;

public struct area(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} m^2";

    public static area operator /(area a, double scalar) =>
        new area(a.Value / scalar);

    public static double operator /(area left, area right) =>
        left.Value / right.Value;

    public static area operator *(area a, double scalar) =>
        new area(a.Value * scalar);

    public static area operator *(double scalar, area a) =>
        new(a.Value * scalar);

    public static area operator -(area left, area right) =>
        new area(left.Value - right.Value);

    public static area operator +(area left, area right) =>
        new area(left.Value + right.Value);

    public static bool operator >(area left, area right) =>
        left.Value > right.Value;

    public static bool operator <(area left, area right) =>
        left.Value < right.Value;

    public static bool operator ==(area left, area right) =>
        left.Value == right.Value;

    public static bool operator !=(area left, area right) =>
        left.Value != right.Value;

    // area / distance = distance
    public static distance operator /(area a, distance d) =>
        new distance(a.Value / d.Value);
    
    public static volume operator *(area a, distance d) =>
        new(a.Value * d.Value);
    
    public static volume operator *(distance d, area a) =>
        new(a.Value * d.Value);
    
    public distance SquareRoot() => new(Math.Sqrt(Value));
}