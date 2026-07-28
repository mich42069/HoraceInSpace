using System.Text;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

public class DeathState(int score, GameArguments arguments) : State(arguments)
{
    private const int MaxNameLength = 32;

    private readonly StringBuilder _name = new();

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
        Vector2 screenSize = PhysicsLibToMonogame.ToVector2(SpaceValues.WorldSize);
        Vector2 center = screenSize / 2f;

        void DrawCentered(string text, float y, Color color)
        {
            Vector2 size = Assets.Assets.Font.MeasureString(text);

            spriteBatch.DrawString(
                Assets.Assets.Font,
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
            $"Score: {score}",
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

            Scoreboard.AddNewScore(name, score);

            SwitchState = true;
            return;
        }

        // Remove character
        if (KeyboardInput.Pressed(Keys.Back))
        {
            if (_name.Length > 0)
                _name.Remove(_name.Length - 1, 1);
        }
        
        // Remove character
        if (KeyboardInput.Pressed(Keys.Escape))
        {
            SwitchState = true;
        }
    }

    public override State NewState()
    {
        return new MenuState(Arguments);
    }
}