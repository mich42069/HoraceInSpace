using System;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States.InGameState;

/// <summary>
/// Drawable stat instance, that draws difficulty in bottom left corner.
/// </summary>
/// <param name="getDifficulty">Delegate method that allows it to see current score.</param>
public class DifficultyStat(Func<GameTime, Difficulty> getDifficulty) : IDrawableStat
{
    private Difficulty _difficulty;

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 position = new(10, (float)SpaceValues.WorldSize.Y.Value - 30);

        spriteBatch.DrawString(
            Assets.Assets.Font,
            $"{_difficulty}",
            position,
            Color.Gray);
    }

    /// <summary>
    /// Updates difficulty by actual difficulty using delegate method passed at constructor.
    /// </summary>
    /// <param name="gameTime">Unused.</param>
    public void Update(GameTime gameTime)
    {
        _difficulty = getDifficulty(gameTime);
    }
}