using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Extension library to allow to adapt PhysicsLib to Monogame
/// </summary>
public static class PhysicsLibToMonogame
{
    /// <summary>
    /// Convert Position to Vector2, 1:1.
    /// </summary>
    /// <param name="pos">Position we want to convert.</param>
    /// <returns>Given Position rewritten as Vector2 coordinates.</returns>
    public static Vector2 ToVector2(this Position pos)
    {
        return new Vector2((float)pos.X.Value, (float)pos.Y.Value);
    }
}