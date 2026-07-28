using System.Collections.Generic;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

public class ScoreboardState : State
{
    private readonly List<(int, string)> _scores;

    private State? _nextState;

    public ScoreboardState(GameArguments arguments) : base(arguments)
    {
        _scores = Scoreboard.GetTopX(15);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Vector2 screenSize = PhysicsLibToMonogame.ToVector2(SpaceValues.WorldSize);
        Vector2 center = screenSize / 2f;

        // Title
        const string title = "Scoreboard";

        Vector2 titleSize = Assets.Assets.Font.MeasureString(title);

        spriteBatch.DrawString(
            Assets.Assets.Font,
            title,
            new Vector2(
                center.X - titleSize.X / 2,
                180
            ),
            Color.Cyan
        );


        // Scores
        float nameX = center.X / 2;
        float scoreX = center.X * 1.5f;

        for (int i = 0; i < _scores.Count; i++)
        {
            int score = _scores[i].Item1;
            string name = _scores[i].Item2;

            float y = 260 + i * 30;

            // Rank + name
            spriteBatch.DrawString(
                Assets.Assets.Font,
                $"{i + 1}.",
                new Vector2(nameX - 40, y),
                Color.Gray
            );

            spriteBatch.DrawString(
                Assets.Assets.Font,
                name,
                new Vector2(nameX, y),
                Color.White
            );

            // Score aligned right
            string scoreText = score.ToString();

            Vector2 scoreSize = Assets.Assets.Font.MeasureString(scoreText);

            spriteBatch.DrawString(
                Assets.Assets.Font,
                scoreText,
                new Vector2(
                    scoreX - scoreSize.X,
                    y
                ),
                Color.Yellow
            );
        }


        // Back hint
        const string backText = "Press ESC to return";

        Vector2 backSize = Assets.Assets.Font.MeasureString(backText);

        spriteBatch.DrawString(
            Assets.Assets.Font,
            backText,
            new Vector2(
                center.X - backSize.X / 2,
                screenSize.Y - 50
            ),
            Color.Gray
        );
    }

    public override void CheckInputs(GameTime gameTime)
    {
        if (KeyboardInput.Pressed(Keys.Escape))
        {
            _nextState = new MenuState(Arguments);
            SwitchState = true;
        }
    }

    public override State NewState()
    {
        return _nextState;
    }
}