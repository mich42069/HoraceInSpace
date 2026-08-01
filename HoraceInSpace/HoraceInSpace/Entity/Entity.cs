using System;
using System.Collections.Generic;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

/// <summary>
/// Base Entity class, holding its position and other physical factors, color, hitbox, texture,
/// and basic methods like Update, Draw, SplitUp or and CheckHit.
/// </summary>
public abstract class Entity
{
    public Color Color { get; set; } = Color.White;
    protected abstract Texture2D Texture { get; }
    protected abstract Vector2 TextureOrigin { get; }
    protected virtual float TextureScale => (float)Radius.Value * 2 / Texture.Height;
    public static bool DrawHitbox = false;
    
    public IHitBox Hitbox;
    public position Position;
    
    protected angle AngleOfAcceleration;
    protected angle AngleOfMotion;
    protected angle AngleOfRotation;
    protected speed Speed;
    protected acceleration Acceleration;
    
    protected abstract distance Radius { get; }
    protected abstract density Density { get; }
    
    protected mass Mass => Density * Volume;
    protected momentum Momentum => Mass * Speed;
    protected area Area => Radius * Radius * Math.PI;
    protected volume Volume => 4/(double)3*Double.Pi*Radius*Radius*Radius;

    /// <summary>
    /// Split up returns a list of entities that are spawned when this one is destroyed. Mainly asteroids splitting up into other asteroids.
    /// </summary>
    /// <returns>List of new entities</returns>
    public virtual List<Entity> SplitUp() => new();
    
    /// <summary>
    /// Base update method that updates entities rotation, position, acceleration, speed and hitbox.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update the entity by.</param>
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
        Hitbox.Position = Position;
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

    /// <summary>
    /// Checks if this entity collides with another entity, by forwarding this operation to their hitboxes.
    /// </summary>
    /// <param name="entity">Other entity to check collision with.</param>
    /// <returns>True if colliding, false otherwise.</returns>
    public virtual bool CheckHit(Entity entity)
    {
        return Hitbox.CheckHit(entity.Hitbox);
    }
    
    /// <summary>
    /// Calls Draw(position, spritebatch) for all possible positions of the entity,
    /// takes into consideration that it can be seen around edges of the screen.
    /// 
    /// Calls Draw on hitbox, if DrawHitBox flag is true.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch responsible for drawing the hitbox.</param>
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
    
    /// <summary>
    /// Draws the entities Texture, with its Color, on position and TextureOrigin with given spriteBatch.
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="spriteBatch"></param>
    protected virtual void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            pos.ToVector2(),
            null,
            Color,
            (float)AngleOfRotation.Value,
            TextureOrigin,
            TextureScale,
            SpriteEffects.None,
            0f);
    }
}