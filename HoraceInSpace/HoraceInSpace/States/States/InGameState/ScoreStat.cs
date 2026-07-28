using System;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States.InGameState;

public class ScoreStat : IDrawableStat
{
    private readonly Func<int> _getScore;

    public ScoreStat(Func<int> getScore)
    {
        _getScore = getScore;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        string text = _getScore().ToString();

        Vector2 size = Assets.Assets.Font.MeasureString(text);
        Vector2 position = new(
            (float)SpaceValues.WorldSize.X.Value - size.X - 10,
            10);

        spriteBatch.DrawString(
            Assets.Assets.Font,
            text,
            position,
            Color.White);
    }

    public void Update(GameTime gameTime)
    {
    }
}