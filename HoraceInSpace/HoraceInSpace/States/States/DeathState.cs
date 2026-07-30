using System.Text;
using HoraceInSpace.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace.States.States;

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

    public DeathState(int score, GameArguments arguments)
        : base(arguments)
    {
        _score = score;

        _gameOverY = Center.Y - 160;
        _scoreY = Center.Y - 100;
        _enterNameY = Center.Y - 30;
        _nameY = Center.Y + 10;
        _hintY = Center.Y + 80;
    }

    public override void TextInput(char character)
    {
        if (_name.Length >= MaxNameLength)
            return;

        if (char.IsLetterOrDigit(character) || character == ' ')
            _name.Append(character);
    }

    public override void CheckInputs(GameTime gameTime)
    {
        HandleSave();
        HandleBackspace();
        HandleEscape();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawCentered(spriteBatch, "GAME OVER"          , _gameOverY , Color.Red);
        DrawCentered(spriteBatch, $"Score: {_score}"   , _scoreY    , Color.White);
        DrawCentered(spriteBatch, "Enter your name:"   , _enterNameY, Color.White);
        DrawCentered(spriteBatch, _name + "_"          , _nameY     , Color.Yellow);
        DrawCentered(spriteBatch, "Press Enter to save", _hintY     , Color.Gray);
    }

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