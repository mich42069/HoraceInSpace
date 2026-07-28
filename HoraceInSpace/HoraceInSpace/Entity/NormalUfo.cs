using System;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class NormalUfo : Ufo, IScore
{
    
    public int Score => 1000;
    protected override distance Radius => SpaceValues.UfoRadius;
    protected override density Density => SpaceValues.UfoDensity;

    public NormalUfo(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
    }

    protected override TimeSpan MinimalShootTime => 3.Seconds();
    protected override TimeSpan ShootTimeVariation => 3.Seconds();

    public override Bullet Shoot(position target)
    {
        distance dx = target.X - Position.X;
        distance dy = target.Y - Position.Y;

        angle angle = MathF.Atan2((float)dy.Value, (float)dx.Value).Radians();

        Bullet bullet = new(Position, angle, angle, 1500.MetersPerSecond())
        {
            Color = Color.Red
        };

        return bullet;
    }
}