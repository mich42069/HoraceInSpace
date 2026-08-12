using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity.Hitbox;

public interface IHitBox
{
    /// <summary>
    /// Check collision of Hitbox and another Hitbox.
    /// </summary>
    /// <param name="hitBox"></param>
    /// <returns>True if colliding, false otherwise.</returns>
    public bool CheckHit(IHitBox hitBox);
    /// <summary>
    /// Check collision of Hitbox and a point.
    /// </summary>
    /// <param name="point"></param>
    /// <returns>True if point inside the hitbox, false otherwise.</returns>
    public bool CheckHit(Position point);

    /// <summary>
    /// Position with public setter, so that it can be set for use.
    /// </summary>
    public Position Position { set; }
    
    /// <summary>
    /// Draws outline of hitbox.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch responsible for drawing the hitbox.</param>
    public void Draw(SpriteBatch spriteBatch);
}