using System;
using System.Collections.Generic;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public abstract class Entity
{
    public Color Color { get; set; } = Color.White;
    protected abstract Texture2D Texture { get; }
    protected abstract Vector2 TextureOrigin { get; }
    protected virtual float TextureScale => (float)Radius.Value * 2 / Texture.Height;
    public static bool DrawHitbox = false;
    public IHitBox Hitbox;
    public position Position;
    protected speed Speed;
    protected acceleration Acceleration;
    protected mass Mass => Density * Volume;
    protected area Area => Radius * Radius * Math.PI;
    protected volume Volume => 4/(double)3*Double.Pi*Radius*Radius*Radius;
    protected angle AngleOfAcceleration;
    protected angle AngleOfMotion;
    protected angle AngleOfRotation;
    protected abstract distance Radius { get; }
    protected abstract density Density { get; }
    protected momentum Momentum => Mass * Speed;

    public virtual List<Entity> SplitUp() => new();
    public virtual void Update(GameTime gameTime)
    {
        UpdateRotation();
        UpdateAcceleration();
        UpdateSpeed(gameTime);
        UpdatePosition(gameTime);
        UpdateHitbox();
    }

    protected virtual void UpdateHitbox()
    {
        Hitbox.SetPosition(Position);
    }

    protected virtual void UpdatePosition(GameTime gameTime)
    {
        TimeSpan dt = gameTime.ElapsedGameTime;

        Position = new position(
            Position.X + Speed * dt * AngleOfMotion.Cos(),
            Position.Y + Speed * dt * AngleOfMotion.Sin());

        Position %= SpaceValues.WorldSize;
    }

    protected virtual void UpdateRotation() {}
    protected virtual void UpdateAcceleration() {}
    protected virtual void UpdateSpeed(GameTime gameTime) {}

    public virtual bool CheckHit(Entity entity)
    {
        return Hitbox.CheckHit(entity.Hitbox);
    }
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Draw(Position, spriteBatch);

        if (Position.X < Radius)
            Draw(Position + (SpaceValues.WorldSize.X, 0.Meters()).At(), spriteBatch);

        if (Position.X > SpaceValues.WorldSize.X - Radius)
            Draw(Position - (SpaceValues.WorldSize.X, 0.Meters()).At(), spriteBatch);

        if (Position.Y < Radius)
            Draw(Position + (0.Meters(), SpaceValues.WorldSize.Y).At(), spriteBatch);

        if (Position.Y > SpaceValues.WorldSize.Y - Radius)
            Draw(Position - (0.Meters(), SpaceValues.WorldSize.Y).At(), spriteBatch);

        if (DrawHitbox)
        {
            Hitbox.Draw(spriteBatch);
        }
    }
    protected virtual void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            PhysicsLibToMonogame.ToVector2(pos),
            null,
            Color,
            (float)AngleOfRotation.Value,
            TextureOrigin,
            TextureScale,
            SpriteEffects.None,
            0f);
    }
}