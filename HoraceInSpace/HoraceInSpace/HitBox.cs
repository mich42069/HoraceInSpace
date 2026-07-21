using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;

namespace HoraceInSpace;


using Microsoft.Xna.Framework;

public class CircleHitBox(position position, distance radius) : IHitBox
{
    private position Position { get; set; } = position;
    private distance Radius { get; set; } = radius;

    public bool CheckHit(IHitBox hitBox)
    {
        if (hitBox is CircleHitBox circle)
        {
            distance distanceToCircle = Position.DistanceTo(circle.Position);

            distance radiusSum = Radius + circle.Radius;

            return distanceToCircle <= radiusSum;
        }

        return false;
    }

    public bool CheckHit(position point)
    {
        return !(Position.DistanceTo(point) > Radius);
    }
}