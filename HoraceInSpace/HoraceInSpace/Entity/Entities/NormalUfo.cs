using System;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Entity.Entities;

/// <summary>
/// A concrete instance of a UFO.
/// Is a Scorable Entity.
/// </summary>
public class NormalUfo : Ufo, IScore
{
    
    public int Score => 1000;
    protected override distance Radius => SpaceValues.UfoRadius;
    protected override density Density => SpaceValues.UfoDensity;

    public NormalUfo(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
        Hitbox.Position = initialPosition;
    }

    protected override TimeSpan MinimalShootTime => 3.Seconds();
    protected override TimeSpan ShootTimeVariation => 3.Seconds();

    /// <summary>
    /// Plays shooting sound, and generates a new bullet that is aimed precisely at a given target.
    /// </summary>
    /// <param name="target">Where we want to shoot</param>
    /// <returns>A bullet aimed precisely at a target.</returns>
    public override Bullet Shoot(Position target)
    {
        Sounds.Shoot.Play();
        
        distance dx = target.X - Position.X;
        distance dy = target.Y - Position.Y;

        angle angle = MathF.Atan2((float)dy.Value, (float)dx.Value).Radians();

        Bullet bullet = new(Position, angle, angle, SpaceValues.BulletInitialSpeed)
        {
            Color = Color.Red
        };

        return bullet;
    }
}