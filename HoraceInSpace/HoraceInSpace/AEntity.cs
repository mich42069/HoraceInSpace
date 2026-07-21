using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public abstract class AEntity
{
    protected IHitBox HitBox;
    protected position Position;
    protected speed Speed;
    protected acceleration Acceleration;
    protected mass Mass;
    protected area Area;
    protected angle AngleOfMotion;
    protected angle AngleOfRotation;
    
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract bool CheckHit();
}