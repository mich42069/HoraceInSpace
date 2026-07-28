using HoraceInSpace.Helpers;
using HoraceInSpace.States.States.InGameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

public class MenuState(GameArguments arguments) : State(arguments)
{
    private readonly string[] _options =
    {
        "Play",
        "Scoreboard",
        "Exit"
    };

    private int _selected;

    private State _nextState;

    public override void Draw(SpriteBatch spriteBatch)
    {
        Vector2 screenSize = PhysicsLibToMonogame.ToVector2(SpaceValues.WorldSize);
        Vector2 center = screenSize / 2f;

        // Background
        spriteBatch.Draw(
            Assets.Assets.Pixel,
            new Rectangle(
                0,
                0,
                (int)screenSize.X,
                (int)screenSize.Y
            ),
            Color.Black
        );

        // Title
        const string title = "Horace In Space";

        Vector2 titleSize = Assets.Assets.Font.MeasureString(title);

        spriteBatch.DrawString(
            Assets.Assets.Font,
            title,
            new Vector2(
                center.X - titleSize.X / 2,
                center.Y - 180
            ),
            Color.Cyan
        );


        // Options
        for (int i = 0; i < _options.Length; i++)
        {
            bool selected = i == _selected;

            string text = selected
                ? "> " + _options[i]
                : "  " + _options[i];

            Color color = selected
                ? Color.Yellow
                : Color.White;

            Vector2 textSize = Assets.Assets.Font.MeasureString(text);

            spriteBatch.DrawString(
                Assets.Assets.Font,
                text,
                new Vector2(
                    center.X - textSize.X / 2,
                    center.Y - 40 + i * 50
                ),
                color
            );
        }
    }

    public override void CheckInputs(GameTime gameTime)
    {
        if (KeyboardInput.Held(Keys.Up, gameTime))
        {
            _selected--;

            if (_selected < 0)
                _selected = _options.Length - 1;
        }

        if (KeyboardInput.Held(Keys.Down, gameTime))
        {
            _selected++;

            if (_selected >= _options.Length)
                _selected = 0;
        }

        if (KeyboardInput.Pressed(Keys.Enter))
        {
            SelectOption();
        }
        if (KeyboardInput.Pressed(Keys.Escape))
        {
            Exit = true;
        }
    }

    private void SelectOption()
    {
        switch (_selected)
        {
            case 0:
                _nextState = new IngameState(Arguments);
                SwitchState = true;
                break;

            case 1:
                _nextState = new ScoreboardState(Arguments);
                SwitchState = true;
                break;

            case 2:
                Exit = true;
                break;
        }
    }

    public override State NewState()
    {
        return _nextState;
    }
}