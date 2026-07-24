namespace HoraceInSpace;

using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CircleHitbox(distance radius) : Hitbox(radius)
{
    private position Position { get; set; }
    private distance Radius { get; set; } = radius;


    public override bool CheckHit(IHitBox hitBox)
    {
        if (hitBox is PointHitbox point) return CheckHit(point.GetPosition());
        if (hitBox is not CircleHitbox circle)
            return false;

        distance dx = Position.X - circle.Position.X;
        distance dy = Position.Y - circle.Position.Y;
        
        if (dx.Negative) dx *= -1;
        if (dy.Negative) dx *= -1;

        if (dx > SpaceValues.WorldSize.X / 2)
            dx = SpaceValues.WorldSize.X - dx;

        if (dy > SpaceValues.WorldSize.Y / 2)
            dy = SpaceValues.WorldSize.Y - dy;

        distance distanceToCircle = Math.Sqrt((dx * dx + dy * dy).Value).Meters();

        return distanceToCircle <= Radius + circle.Radius;
    }
}