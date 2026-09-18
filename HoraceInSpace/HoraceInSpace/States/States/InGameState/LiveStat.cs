using HoraceInSpace.Assets;
using HoraceInSpace.Entity;
using HoraceInSpace.Entity.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States.InGameState;

/// <summary>
/// Drawable stat instance, that draws lives in top left corner.
/// </summary>
/// <param name="horace">Horace that has the lives that we want to show.</param>
public class LiveStat(Horace horace) : IDrawableStat
{
    private const int Margin = 10;
    private const int Spacing = 8;
    private const float Scale = 1.5f;
    private int _horaceLives;

    /// <summary>
    /// For every live we draw the lives out.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _horaceLives; i++)
        {
            Vector2 position = new(
                Margin + i * (Textures.Horace.Width * Scale + Spacing),
                Margin);

            spriteBatch.Draw(
                Textures.Horace,
                position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f);
        }
    }

    /// <summary>
    /// Updates lives by Horace's actual number of lives left.
    /// </summary>
    /// <param name="gameTime">Unused.</param>
    public void Update(GameTime gameTime)
    {
        _horaceLives = horace.Lives;
    }
}