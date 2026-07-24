using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public static class SpaceValues
{
    public static dragCoefficient DragCoefficient => 2.DragCoefficient();
    public static density AtmosphericDensity => 1.KilogramsPerCubicMeter();
    public static position WorldSize {get; set;}
    public static density AsteroidDensity => 5_000.KilogramsPerCubicMeter();
    public static distance SmallAsteroidRadius => 20.Meters();
    public static distance MediumAsteroidRadius => 40.Meters();
    public static distance BigAsteroidRadius => 80.Meters();
    public static distance UfoRadius => 30.Meters();
}