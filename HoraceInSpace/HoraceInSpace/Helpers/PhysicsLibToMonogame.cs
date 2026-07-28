using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Helpers;

public static class PhysicsLibToMonogame
{
    public static Vector2 ToVector2(this position pos)
    {
        return new Vector2((float)pos.X.Value, (float)pos.Y.Value);
    }
}