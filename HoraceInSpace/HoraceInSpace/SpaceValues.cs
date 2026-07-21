using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public static class SpaceValues
{
    public static dragCoefficient DragCoefficient => 1.DragCoefficient();
    public static density AtmosphericDensity => 0.000001.KilogramsPerCubicMeter() * 1000;
}