using System;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class Bullet : AEntity
{
    protected override Texture2D Texture => Textures.Bullet;
    protected override Vector2 TextureOrigin => Textures.BulletOrigin;
    private readonly TimeSpan _lifeTime = TimeSpan.FromSeconds(5);
    private TimeSpan? _timeOfCreation = null;
    public Color Color { get; set; } = Color.White;
    public Bullet(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
        
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new PointHitbox(Radius);
    }

    public bool LifeTimeOver(GameTime gameTime)
    {
        if (!_timeOfCreation.HasValue) return false;
        return gameTime.TotalGameTime - _timeOfCreation.Value > _lifeTime;
    }

    protected override distance Radius => SpaceValues.BulletRadius;
    protected override density Density => SpaceValues.BulletDensity;

    public override void Update(GameTime gameTime)
    {
        _timeOfCreation ??= gameTime.TotalGameTime;
        base.Update(gameTime);
    }

    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Textures.Bullet,
            pos.ToVector2(),
            null,
            Color,
            (float)AngleOfRotation.Value,
            Textures.BulletOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }
}