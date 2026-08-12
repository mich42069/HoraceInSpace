using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Helpers;

public static class PositionExtension
{
    /// <summary>
    /// Creates a position from coordinate values.
    /// </summary>
    /// <param name="coords">The x and y coordinates in meters.</param>
    /// <returns>A position containing the specified coordinates.</returns>
    public static Position At(this (double x, double y) coords) => new Position(coords.x.Meters(), coords.y.Meters());

    /// <summary>
    /// Creates a position from distance coordinates.
    /// </summary>
    /// <param name="coords">The x and y distance coordinates.</param>
    /// <returns>A position containing the specified coordinates.</returns>
    public static Position At(this (distance x, distance y) coords) => new Position(coords.x, coords.y);

    /// <summary>
    /// Creates a position from integer coordinate values.
    /// </summary>
    /// <param name="coords">The x and y coordinates in meters.</param>
    /// <returns>A position containing the specified coordinates.</returns>
    public static Position At(this (int x, int y) coords) => new Position(coords.x.Meters(), coords.y.Meters());
}