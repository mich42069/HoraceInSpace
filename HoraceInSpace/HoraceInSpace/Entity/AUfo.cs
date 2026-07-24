using System;
using System.DirectoryServices.ActiveDirectory;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Entity;

public class AUfo : AEntity
{
    private static readonly Random Random = new();
    private bool _readyToShoot = false;
    public bool IsReadyToShoot
    {
        get
        {
            bool temp = _readyToShoot;
            _readyToShoot = false;
            return temp;
        }
    }

    private readonly TimeSpan _minimalShootTime = 3.Seconds();
    private readonly TimeSpan _shootTimeVariation = 3.Seconds();
    private TimeSpan? _nextShootTime = null;
    
    protected AUfo(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
    }
    
    public override void Update(GameTime gameTime)
    {
        if (_nextShootTime == null || _nextShootTime < gameTime.TotalGameTime)
        {
            if (_nextShootTime != null) _readyToShoot = true;
            _nextShootTime = gameTime.TotalGameTime + _minimalShootTime + _shootTimeVariation * Random.NextDouble();
        }
        base.Update(gameTime);
    }
    public virtual Bullet Shoot(position target)
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