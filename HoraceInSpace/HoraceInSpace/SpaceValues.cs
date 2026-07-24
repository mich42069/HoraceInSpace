using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public static class SpaceValues
{
    public static dragCoefficient DragCoefficient => 2.DragCoefficient();
    public static density AtmosphericDensity => 1.KilogramsPerCubicMeter();
    public static position WorldSize {get; set;}
    public static mass MediumAsteroidMass => 16_000.Kilograms();
    public static mass SmallAsteroidMass => 3_500.Kilograms();
    public static mass BigAsteroidMass => 64_000.Kilograms();
}