using HoraceInSpacePhysicsLib;

namespace HoraceInSpace.Entity.Hitbox;

public class PointHitbox(distance radius) : Hitbox(radius)
{
    public position GetPosition() => Position;
}