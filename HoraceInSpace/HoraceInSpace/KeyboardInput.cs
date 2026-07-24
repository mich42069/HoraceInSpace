using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using HoraceInSpace;

public static class KeyboardInput
{
    private static KeyboardState _current;
    private static KeyboardState _previous;

    private static readonly Dictionary<Keys, Button> _buttons = new();

    public static void Update(GameTime gameTime)
    {
        _previous = _current;
        _current = Keyboard.GetState();
    }

    public static bool Pressed(Keys key)
    {
        return _current.IsKeyDown(key) &&
               _previous.IsKeyUp(key);
    }

    public static bool Held(Keys key, GameTime gameTime)
    {
        if (!_buttons.TryGetValue(key, out Button button))
        {
            button = new Button();
            _buttons[key] = button;
        }

        return button.Update(
            _current.IsKeyDown(key),
            gameTime
        );
    }

    public static void Clear()
    {
        _previous = _current;
        _buttons.Clear();
    }
}