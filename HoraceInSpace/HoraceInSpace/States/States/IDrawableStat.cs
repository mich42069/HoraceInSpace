using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States;

/// <summary>
/// Provides interface for statistics so that they can be updated and drawn on screen.
/// </summary>
public interface IDrawableStat
{
    /// <summary>
    /// Draws the statistics on screen.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public void Draw(SpriteBatch spriteBatch);
    
    /// <summary>
    /// Updates the statistics based on GameTime.
    /// </summary>
    /// <param name="gameTime">Current GameTime to update the statistics by.</param>
    public void Update(GameTime gameTime);
}