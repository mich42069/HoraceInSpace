using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Class that holds constants for density and radius of most entities, as well as other otherwise non-relevant constants.
/// </summary>
public static class SpaceValues
{
    public static float Scale = 20;
    public static double ScaleConversion => ScreenSize.X / WorldSize.X.Value;
    private static distance GameWidth => ScreenSize.X.Meters();
    private static distance GameHeight => ScreenSize.Y.Meters();
    public static Position WorldSize => (GameWidth, GameHeight).At() / Scale;
    public static Vector2 ScreenSize { get; set; }
    
    public static dragCoefficient DragCoefficient => 2.DragCoefficient();
    public static density AtmosphericDensity => 1.KilogramsPerCubicMeter();
    public static density AsteroidDensity => 5_000.KilogramsPerCubicMeter();
    public static density BulletDensity => 7_850.KilogramsPerCubicMeter();
    public static density HoraceDensity => 1_200.KilogramsPerCubicMeter();
    public static density UfoDensity => 2_000.KilogramsPerCubicMeter();
    public static distance HoraceRadius => 2.Meters();
    public static distance BulletRadius => 0.2.Meters();
    public static distance SmallAsteroidRadius => 1.Meters();
    public static distance MediumAsteroidRadius => 2.Meters();
    public static distance BigAsteroidRadius => 4.Meters();
    public static distance GiantAsteroidRadius => 6.Meters();
    public static distance EnormousAsteroidRadius => 8.Meters();
    public static distance UfoRadius => 1.5.Meters();
    public static force HoraceThrusterForce => 1.MegaNewtons();
    public static speed MaxEntitySpawnSpeed => 10.MetersPerSecond();
    public static speed MaxAsteroidSplitSpeed => MaxEntitySpawnSpeed * 4;
    public static speed BulletInitialSpeed => 65.MetersPerSecond();
}