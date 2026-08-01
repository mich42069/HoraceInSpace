using System.Text;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

/// <summary>
/// Instance of state representing the player's death.
/// Responsible for adding scores into scoreboard.
/// </summary>
public class DeathState : State
{
    private const int MaxNameLength = 32;

    private readonly int _score;
    private readonly StringBuilder _name = new();

    private readonly float _gameOverY;
    private readonly float _scoreY;
    private readonly float _enterNameY;
    private readonly float _nameY;
    
    private readonly float _hintY;
    private readonly float _controlsY;
    
    private const float HintBottomOffset = 80;
    private const float ControlsBottomOffset = 50;

    /// <summary>
    /// Creates a new instance and saves arguments and score.
    /// </summary>
    /// <param name="score">Score from previous game, to be saved into scoreboard.</param>
    /// <param name="arguments">Game arguments parsed at startup.</param>
    public DeathState(int score, GameArguments arguments)
        : base(arguments)
    {
        _score = score;

        _gameOverY = Center.Y - 160;
        _scoreY = Center.Y - 100;
        _enterNameY = Center.Y - 30;
        _nameY = Center.Y + 10;
        _hintY = ScreenSize.Y - 95;
        _controlsY = ScreenSize.Y - 50;
    }

    /// <summary>
    /// Takes in a character and writes it into the text field.
    /// </summary>
    /// <param name="character">Character to be written out.</param>
    public override void TextInput(char character)
    {
        if (_name.Length >= MaxNameLength)
            return;

        if (char.IsLetterOrDigit(character) || character == ' ')
            _name.Append(character);
    }

    /// <summary>
    /// Checks if Enter, Escape or Backspace been pressed.
    /// Enter saves score.
    /// Backspace deletes character.
    /// Escape exits and doesn't save the score.
    /// </summary>
    /// <param name="gameTime">Current GameTime to account for key spam and else.</param>
    public override void CheckInputs(GameTime gameTime)
    {
        HandleSave();
        HandleBackspace();
        HandleEscape();
    }

    /// <summary>
    /// Draws out the state by drawing the score, name title and hint.
    /// </summary>
    /// <param name="spriteBatch">Spritebatch responsible for drawing out everything.</param>
    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawCentered(spriteBatch, "GAME OVER"        , _gameOverY, Color.Red);
        DrawCentered(spriteBatch, $"Score: {_score}" , _scoreY, Color.White);
        DrawCentered(spriteBatch, "Enter your name:" , _enterNameY, Color.White);
        DrawCentered(spriteBatch, _name + "_"        , _nameY, Color.Yellow);

        DrawCentered(spriteBatch, "Type your name"   , _hintY, Color.Gray);
        DrawCentered(spriteBatch, "BACKSPACE Delete    ENTER Save    ESC Skip", _controlsY, Color.Gray);}

    /// <summary>
    /// Returns new state, that should be Menu unless changed.
    /// </summary>
    /// <returns>New Instance of MenuState</returns>
    public override State NewState()
    {
        return new MenuState(Arguments);
    }

    private void HandleSave()
    {
        if (!KeyboardInput.Pressed(Keys.Enter))
            return;

        string name = _name.Length == 0
            ? "Anonymous"
            : _name.ToString();

        Scoreboard.AddNewScore(name, _score);

        SwitchState = true;
    }

    private void HandleBackspace()
    {
        if (!KeyboardInput.Pressed(Keys.Back))
            return;

        if (_name.Length > 0)
            _name.Remove(_name.Length - 1, 1);
    }

    private void HandleEscape()
    {
        if (KeyboardInput.Pressed(Keys.Escape))
            SwitchState = true;
    }
}