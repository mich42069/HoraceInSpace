using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Background;

/// <summary>
/// Interface of a Star that is drawn on the background.
/// </summary>
public interface IStar
{
    /// <summary>
    /// Given stars special effect trigger, like a flicker or the event of a comet appearing
    /// </summary>
    /// <param name="gameTime">Time that the effect happens</param>
    /// <param name="intensity">Intensity of the effect like what lightness to flicker with. In range [-1, 1]</param>
    /// <param name="length"></param>
    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length);
    /// <summary>
    /// Draws given Star
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch responsible for drawing the star</param>
    public void Draw(SpriteBatch spriteBatch);
    /// <summary>
    /// Updates the star, mainly advances its SpecialEffect by ElapsedGameTime from gameTime
    /// </summary>
    /// <param name="gameTime">Time to advance the effect by.</param>
    public void Update(GameTime gameTime);

    public double NonSpecialEffectChance { get; }
}