namespace HoraceInSpacePhysicsLib;

public struct volume(double value)
{
    public double Value = value;

    public override string ToString() => $"{Value} m³";

    public static volume operator +(volume left, volume right) =>
        new(left.Value + right.Value);

    public static volume operator -(volume left, volume right) =>
        new(left.Value - right.Value);

    public static volume operator *(volume vol, double scalar) =>
        new(vol.Value * scalar);

    public static volume operator *(double scalar, volume vol) =>
        new(vol.Value * scalar);

    public static volume operator /(volume vol, double scalar) =>
        new(vol.Value / scalar);

    // volume / distance = area
    public static area operator /(volume vol, distance dist) =>
        new(vol.Value / dist.Value);

    public static bool operator >(volume left, volume right) =>
        left.Value > right.Value;

    public static bool operator >=(volume left, volume right) =>
        left.Value >= right.Value;

    public static bool operator <(volume left, volume right) =>
        left.Value < right.Value;

    public static bool operator <=(volume left, volume right) =>
        left.Value <= right.Value;

    public static bool operator ==(volume left, volume right) =>
        left.Value == right.Value;

    public static bool operator !=(volume left, volume right) =>
        left.Value != right.Value;
    
    // density = mass / volume
    public static density operator /(mass m, volume v) =>
        new(m.Value / v.Value);
    
    // mass = density * volume
    public static mass operator *(density d, volume v) =>
        new(d.Value * v.Value);
    
    // mass = volume * density
    public static mass operator *(volume v, density d) =>
        new(d.Value * v.Value);
}