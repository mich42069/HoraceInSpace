using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Class that holds constants for density and radius of most entities, as well as other otherwise non-relevant constants.
/// </summary>
public static class SpaceValues
{
    public static dragCoefficient DragCoefficient => 2.DragCoefficient();
    public static density AtmosphericDensity => 1.KilogramsPerCubicMeter();
    public static position WorldSize {get; set;}
    public static density AsteroidDensity => 5_000.KilogramsPerCubicMeter();
    public static density BulletDensity => 7_850.KilogramsPerCubicMeter();
    public static density HoraceDensity => 1_200.KilogramsPerCubicMeter();
    public static density UfoDensity => 2_000.KilogramsPerCubicMeter();
    public static distance HoraceRadius => 40.Meters();
    public static distance BulletRadius => 1.Meters();
    public static distance SmallAsteroidRadius => 20.Meters();
    public static distance MediumAsteroidRadius => 40.Meters();
    public static distance BigAsteroidRadius => 80.Meters();
    public static distance GiantAsteroidRadius => 120.Meters();
    public static distance EnormousAsteroidRadius => 160.Meters();
    public static distance UfoRadius => 30.Meters();
}