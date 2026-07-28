using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Entity.Hitbox;

public class PointHitbox(distance radius) : Hitbox(radius)
{
    public position GetPosition() => Position;
}