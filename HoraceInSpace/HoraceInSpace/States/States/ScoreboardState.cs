using System.Collections.Generic;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

/// <summary>
/// Concrete state created for showing scoreboard.
/// </summary>
public class ScoreboardState : State
{
    private readonly IReadOnlyList<(int Score, string Name)> _scores;

    private State _nextState;

    private const float TitleY = 180;
    private const float ScoresStartY = 260;
    private const float RowSpacing = 30;

    private readonly float _nameX;
    private readonly float _scoreX;
    private readonly float _backHintY;

    /// <summary>
    /// Saves arguments as well as gets top 15 scores.
    /// </summary>
    /// <param name="arguments">Game arguments parsed at startup.</param>
    public ScoreboardState(GameArguments arguments)
        : base(arguments)
    {
        _scores = Scoreboard.GetTopX(15);

        _nameX = Center.X / 2f;
        _scoreX = Center.X * 1.5f;
        _backHintY = ScreenSize.Y - 50;
    }

    /// <summary>
    /// Draws the scores, header and back hint.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawTitle(spriteBatch);
        DrawScores(spriteBatch);
        DrawBackHint(spriteBatch);
    }

    /// <summary>
    /// Checks if Escape been pressed.
    /// </summary>
    /// <param name="gameTime">Current GameTime to prevent unintentional spamming of keys.</param>
    public override void CheckInputs(GameTime gameTime)
    {
        if (!KeyboardInput.Pressed(Keys.Escape))
            return;

        _nextState = new MenuState(Arguments);
        SwitchState = true;
    }

    /// <summary>
    /// Returns new state, that should be Menu unless changed.
    /// </summary>
    /// <returns>New Instance of MenuState</returns>
    public override State NewState()
    {
        return _nextState;
    }

    private void DrawTitle(SpriteBatch spriteBatch)
    {
        DrawCentered(spriteBatch, "Scoreboard", TitleY, Color.Cyan);
    }

    private void DrawScores(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _scores.Count; i++)
        {
            (int score, string name) = _scores[i];

            float y = ScoresStartY + i * RowSpacing;

            spriteBatch.DrawString(Font, $"{i + 1}.", new Vector2(_nameX - 40, y), Color.Gray);

            spriteBatch.DrawString(Font, name, new Vector2(_nameX, y), Color.White);

            DrawRightAligned(spriteBatch, score.ToString(), _scoreX, y, Color.Yellow);
        }
    }

    private void DrawBackHint(SpriteBatch spriteBatch)
    {
        DrawCentered(
            spriteBatch,
            "Press ESC to return",
            _backHintY,
            Color.Gray);
    }
}