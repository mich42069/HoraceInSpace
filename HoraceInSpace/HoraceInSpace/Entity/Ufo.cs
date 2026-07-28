using System;
using System.DirectoryServices.ActiveDirectory;
using HoraceInSpace.Assets;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public abstract class Ufo : Entity
{
    protected override Texture2D Texture => Textures.Ufo;
    protected override Vector2 TextureOrigin => Textures.UfoOrigin;
    protected abstract TimeSpan MinimalShootTime { get; }
    protected abstract TimeSpan ShootTimeVariation { get; }
    
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
    private TimeSpan? _nextShootTime = null;
    
    protected Ufo(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
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
            _nextShootTime = gameTime.TotalGameTime + MinimalShootTime + ShootTimeVariation * Random.NextDouble();
        }
        base.Update(gameTime);
    }
    public abstract Bullet Shoot(position target);
}