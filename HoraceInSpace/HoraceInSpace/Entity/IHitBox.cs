using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public interface IHitBox
{
    public bool CheckHit(IHitBox hitBox);
    public bool CheckHit(position point);

    public void SetPosition(position position);
    public void Draw(SpriteBatch spriteBatch);
}