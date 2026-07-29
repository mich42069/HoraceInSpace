using System;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.States.States.InGameState;

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

    public void Update(GameTime gameTime)
    {
        _difficulty = getDifficulty(gameTime);
    }
}