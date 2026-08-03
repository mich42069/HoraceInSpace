using System;
using Microsoft.Xna.Framework;

namespace HoraceInSpace.Helpers;

/// <summary>
/// A wrapper around a button, to prevent spam.
/// </summary>
public class Button
{
    private bool _wasPressed;
    private double _timer;

    private TimeSpan InitialDelay { get; set; } = TimeSpan.FromMilliseconds(400);
    private TimeSpan RepeatInterval { get; set; } = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// When given if button is pressed and given gametime, returns true every so often, based on internal parameters.
    /// </summary>
    /// <param name="isPressed">If button is pressed.</param>
    /// <param name="gameTime">Current GameTime.</param>
    /// <returns>true every 100ms, after first 400ms of being first held down., otherwise false</returns>
    public bool Update(bool isPressed, GameTime gameTime)
    {
        double elapsed = gameTime.ElapsedGameTime.TotalSeconds;

        if (!isPressed)
        {
            _wasPressed = false;
            _timer = 0;
            return false;
        }

        // First press
        if (!_wasPressed)
        {
            _wasPressed = true;
            _timer = InitialDelay.TotalSeconds;
            return true;
        }

        // Held down
        _timer -= elapsed;

        if (_timer <= 0)
        {
            _timer = RepeatInterval.TotalSeconds;
            return true;
        }

        return false;
    }
}