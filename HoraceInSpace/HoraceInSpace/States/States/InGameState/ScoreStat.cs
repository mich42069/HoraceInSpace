using System;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States.InGameState;

/// <summary>
/// Drawable stat instance, that draws score in top right corner.
/// </summary>
public class ScoreStat : IDrawableStat
{
    private readonly Func<int> _getScore;
    private int _score;

    /// <summary>
    /// Saves passed delegate to call when updated to update its score.
    /// </summary>
    /// <param name="getScore">Method that allows it to see current score.</param>
    public ScoreStat(Func<int> getScore)
    {
        _getScore = getScore;
    }

    /// <summary>
    /// Draws the score in top right corner.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        string text = _score.ToString();

        Vector2 size = Assets.Assets.Font.MeasureString(text);
        Vector2 position = new(
            SpaceValues.ScreenSize.X - size.X - 10,
            10);

        spriteBatch.DrawString(
            Assets.Assets.Font,
            text,
            position,
            Color.White);
    }
    /// <summary>
    /// Updates its score by one given by delegate passed in constructor.
    /// </summary>
    /// <param name="gameTime">Unused.</param>
    public void Update(GameTime gameTime)
    {
        _score = _getScore();
    }
}