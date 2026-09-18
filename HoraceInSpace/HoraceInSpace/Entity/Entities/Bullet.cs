using System;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity.Entities;

/// <summary>
/// Represents an instance of a Bullet, defining its lifetime.
/// </summary>
public class Bullet : Entity
{
    protected override Texture2D Texture => Textures.Bullet;
    protected override Vector2 TextureOrigin => Textures.BulletOrigin;
    private readonly TimeSpan _lifeTime = TimeSpan.FromSeconds(5);
    private TimeSpan? _timeOfCreation = null;
    protected override distance Radius => SpaceValues.BulletRadius;
    protected override density Density => SpaceValues.BulletDensity;
    public Bullet(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
        
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
        Hitbox.Position = initialPosition;
    }

    /// <summary>
    /// Checks if the LifeTime of the bullet has been exceeded.
    /// </summary>
    /// <param name="gameTime">Current time of Game.</param>
    /// <returns>True if the lifetime of the bullet has been exceeded, false if not.</returns>
    public bool LifeTimeOver(GameTime gameTime)
    {
        if (!_timeOfCreation.HasValue) return false;
        return gameTime.TotalGameTime - _timeOfCreation.Value > _lifeTime;
    }


    /// <summary>
    /// Sets time of creation on first call. After that calls update on its base class.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update with.</param>
    public override void Update(GameTime gameTime)
    {
        _timeOfCreation ??= gameTime.TotalGameTime;
        base.Update(gameTime);
    }
}