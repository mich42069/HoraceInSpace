using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public abstract class AEntity
{
    protected position MapBoundingBox;
    protected IHitBox HitBox;
    protected position Position;
    protected speed Speed;
    protected acceleration Acceleration;
    protected mass Mass;
    protected area Area;
    protected angle AngleOfAcceleration;
    protected angle AngleOfMotion;
    protected angle AngleOfRotation;
    protected distance Radius;
    
    public abstract void Update(GameTime gameTime);

    public abstract bool CheckHit();
    
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Draw(Position, spriteBatch);

        if (Position.X < Radius)
            Draw(Position + (MapBoundingBox.X, 0.Meters()).At(), spriteBatch);

        if (Position.X > MapBoundingBox.X - Radius)
            Draw(Position - (MapBoundingBox.X, 0.Meters()).At(), spriteBatch);

        if (Position.Y < Radius)
            Draw(Position + (0.Meters(), MapBoundingBox.Y).At(), spriteBatch);

        if (Position.Y > MapBoundingBox.Y - Radius)
            Draw(Position - (0.Meters(), MapBoundingBox.Y).At(), spriteBatch);
    }
    protected virtual void Draw(position pos, SpriteBatch spriteBatch)
    {
    }
}