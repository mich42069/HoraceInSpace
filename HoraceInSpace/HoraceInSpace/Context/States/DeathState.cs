using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace HoraceInSpace.Context.States;

public class DeathState : AState
{
    private readonly int _score;
    private const int MaxNameLength = 32;

    private readonly StringBuilder _name = new();

    private bool _finished;

    public DeathState(int score, GameArguments arguments) : base(arguments)
    {
        _score = score;
    }
    
    public override void TextInput(char character)
    {
        if (_name.Length >= MaxNameLength)
            return;

        if (char.IsLetterOrDigit(character) || character == ' ')
        {
            _name.Append(character);
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        Vector2 screenSize = SpaceValues.WorldSize.ToVector2();
        Vector2 center = screenSize / 2f;

        void DrawCentered(string text, float y, Color color)
        {
            Vector2 size = Assets.Font.MeasureString(text);

            spriteBatch.DrawString(
                Assets.Font,
                text,
                new Vector2(
                    center.X - size.X / 2,
                    y
                ),
                color
            );
        }

        DrawCentered(
            "GAME OVER",
            center.Y - 160,
            Color.Red
        );

        DrawCentered(
            $"Score: {_score}",
            center.Y - 100,
            Color.White
        );

        DrawCentered(
            "Enter your name:",
            center.Y - 30,
            Color.White
        );

        DrawCentered(
            _name + "_",
            center.Y + 10,
            Color.Yellow
        );

        DrawCentered(
            "Press Enter to save",
            center.Y + 80,
            Color.Gray
        );
    }
    public override void CheckInputs(GameTime gameTime)
    {
        // Save
        if (KeyboardInput.Pressed(Keys.Enter))
        {
            string name = _name.Length == 0
                ? "Anonymous"
                : _name.ToString();

            Scoreboard.AddNewScore(name, _score);

            _finished = true;
            SwitchState = true;
            return;
        }

        // Remove character
        if (KeyboardInput.Pressed(Keys.Back))
        {
            if (_name.Length > 0)
                _name.Remove(_name.Length - 1, 1);
        }
    }

    public override AState NewState()
    {
        return _finished
            ? new MenuState(Arguments)
            : this;
    }
}