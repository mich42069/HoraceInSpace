using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace.Helpers;

/// <summary>
/// Keyboard Wrapper to prevent spam.
/// </summary>
public static class KeyboardInput
{
    private static KeyboardState _current;
    private static KeyboardState _previous;

    private static readonly Dictionary<Keys, Button> _buttons = new();

    /// <summary>
    /// Updates current and previous state.
    /// </summary>
    public static void Update()
    {
        _previous = _current;
        _current = Keyboard.GetState();
    }

    /// <summary>
    /// Returns true if the key has been pressed. Only once per press.
    /// </summary>
    /// <param name="key">What key we want to check.</param>
    /// <returns>True on first press when held. False if not pressed or if pressed and this isn't the first time of being pressed.</returns>
    public static bool Pressed(Keys key)
    {
        return _current.IsKeyDown(key) &&
               _previous.IsKeyUp(key);
    }
    
    /// <summary>
    /// Used to register repeated presses, used with Button class.
    /// </summary>
    /// <param name="key">What key is pressed.</param>
    /// <param name="gameTime">What time it is.</param>
    /// <returns>True on first held down and then every 100ms after initial 400ms delay. False otherwise.</returns>
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

    /// <summary>
    /// Resets the keyboard.
    /// </summary>
    public static void Clear()
    {
        _previous = _current;
        _buttons.Clear();
    }
}