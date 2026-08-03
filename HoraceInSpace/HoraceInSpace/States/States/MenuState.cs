using HoraceInSpace.Helpers;
using HoraceInSpace.States.States.InGameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

/// <summary>
/// Concrete State instance representing menu, allowing the player to either exit the game, show scoreboard or play.
/// </summary>
/// <param name="arguments">Game arguments parsed at startup.</param>
public class MenuState(GameArguments arguments) : State(arguments)
{
    private readonly string[] _options =
    [
        "Play",
        "Scoreboard",
        "Exit"
    ];
    
    private float _controlsHintY => (float)SpaceValues.WorldSize.Y.Value - 50;
    
    private const string ControlsHint =
        "v/^ Navigate    ENTER Select    ESC Exit";

    private int _selected;
    private State _nextState;

    private const float TitleOffsetY = -180;
    private const float OptionsStartOffsetY = -40;
    private const float OptionSpacing = 50;

    /// <summary>
    /// Draws the menu, specifically all the options and game title.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawTitle(spriteBatch);
        DrawOptions(spriteBatch);
        DrawControlsHint(spriteBatch);
    }

    /// <summary>
    /// Checks inputs from keyboard to navigate the menu. 
    /// </summary>
    /// <param name="gameTime">Current GameTime to prevent unintentional spamming of keys.</param>
    public override void CheckInputs(GameTime gameTime)
    {
        HandleNavigation(gameTime);
        HandleSelection();

        if (KeyboardInput.Pressed(Keys.Escape))
            Exit = true;
    }

    private void DrawTitle(SpriteBatch spriteBatch)
    {
        DrawCentered(spriteBatch, "Horace In Space", Center.Y + TitleOffsetY, Color.Cyan);
    }
    
    private void DrawControlsHint(SpriteBatch spriteBatch)
    {
        DrawCentered(spriteBatch, ControlsHint, _controlsHintY, Color.Gray);
    }

    private void DrawOptions(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _options.Length; i++)
        {
            bool selected = i == _selected;

            string text = selected
                ? "> " + _options[i]
                : "  " + _options[i];

            Color color = selected
                ? Color.Yellow
                : Color.White;

            DrawCentered(spriteBatch, text, Center.Y + OptionsStartOffsetY + i * OptionSpacing, color);
        }
    }

    private void HandleNavigation(GameTime gameTime)
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
    }

    private void HandleSelection()
    {
        if (!KeyboardInput.Pressed(Keys.Enter))
            return;

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
    
    /// <summary>
    /// When ready to switch states, this holds the next instance to switch to.
    /// </summary>
    /// <returns>Returns an instance of the next state.</returns>
    public override State NewState()
    {
        return _nextState;
    }
}