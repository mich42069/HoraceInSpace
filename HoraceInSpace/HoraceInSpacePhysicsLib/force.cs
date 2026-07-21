namespace HoraceInSpacePhysicsLib;

public struct force(double value)
{
    public double Value = value;
    public override string ToString() => $"{Value} N";

    public static force operator +(force a, force b) =>
        new(a.Value + b.Value);

    public static force operator -(force a, force b) =>
        new(a.Value - b.Value);

    public static force operator *(force f, double scalar) =>
        new(f.Value * scalar);

    public static force operator *(double scalar, force f) =>
        new(f.Value * scalar);

    public static force operator /(force f, double scalar) =>
        new(f.Value / scalar);

    // force / acceleration = mass
    public static mass operator /(force f, acceleration a) =>
        new mass(f.Value / a.Value);

    public static force AtmosphericDrag(
        density airDensity,
        dragCoefficient dragCoefficient,
        area crossSection,
        speed velocity)
    {
        double magnitude =
            0.5 *
            airDensity.Value *
            velocity.Value * velocity.Value *
            dragCoefficient.Value *
            crossSection.Value;

        return new force(magnitude);
    }
}

public struct dragCoefficient(double value)
{
    public double Value = value;
}