using HoraceInSpacePhysicsLib;

namespace HoraceInSpace.Entity;

public class PointHitbox(distance radius) : Hitbox(radius)
{
    public position GetPosition() => Position;
}