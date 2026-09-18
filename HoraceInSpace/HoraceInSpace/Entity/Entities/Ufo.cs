using System;
using HoraceInSpace.Assets;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity.Entities;

/// <summary>
/// Abstract instance of a UFO, it's main purpose is an entity that can shoot.
/// </summary>
public abstract class Ufo : Entity
{
    protected override Texture2D Texture => Textures.Ufo;
    protected override Vector2 TextureOrigin => Textures.UfoOrigin;
    protected abstract TimeSpan MinimalShootTime { get; }
    protected abstract TimeSpan ShootTimeVariation { get; }
    
    private static readonly Random Random = new();
    private bool _readyToShoot = false;
    /// <summary>
    /// Returns true if the bullet is loaded and ready to shoot, time between shots is dependent on concrete UFOs.
    /// </summary>
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
    
    protected Ufo(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
    }
    
    /// <summary>
    /// Override of entity update, adding the calculation of the next shot time.
    /// </summary>
    /// <param name="gameTime"></param>
    public override void Update(GameTime gameTime)
    {
        if (_nextShootTime == null || _nextShootTime < gameTime.TotalGameTime)
        {
            if (_nextShootTime != null) _readyToShoot = true;
            _nextShootTime = gameTime.TotalGameTime + MinimalShootTime + ShootTimeVariation * Random.NextDouble();
        }
        base.Update(gameTime);
    }
    
    /// <summary>
    /// Method giving the ability to Shoot, returning a bullet with a speed and angle.
    /// </summary>
    /// <param name="target">Where do we want to shoot</param>
    /// <returns>A bullet with a speed, and direction.</returns>
    public abstract Bullet Shoot(Position target);
}