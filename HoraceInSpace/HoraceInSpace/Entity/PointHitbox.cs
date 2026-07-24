using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class PointHitbox(distance radius) : Hitbox(radius)
{
    public position GetPosition() => Position;
}