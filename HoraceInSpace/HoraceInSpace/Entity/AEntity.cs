using System;
using System.Collections.Generic;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public abstract class AEntity
{   
    public int Score { get; protected set; }
    public static bool DrawHitbox = false;
    public IHitBox Hitbox;
    public position Position;
    protected speed Speed;
    protected acceleration Acceleration;
    protected mass Mass;
    protected area Area;
    protected angle AngleOfAcceleration;
    protected angle AngleOfMotion;
    protected angle AngleOfRotation;
    protected distance Radius;
    protected momentum Momentum => Mass * Speed;

    public virtual List<AEntity> SplitUp() => new ();
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

    public virtual bool CheckHit(AEntity entity)
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
    }
}