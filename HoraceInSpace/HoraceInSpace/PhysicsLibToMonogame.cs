using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;

namespace HoraceInSpace;

public static class PhysicsLibToMonogame
{
    public static Vector2 ToVector2(this position pos)
    {
        return new Vector2((float)pos.X.Value, (float)pos.Y.Value);
    }
}