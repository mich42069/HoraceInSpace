using System;
using Microsoft.Xna.Framework;

namespace HoraceInSpace;

public class Button
{
    private bool _wasPressed;
    private double _timer;

    public TimeSpan InitialDelay { get; set; } = TimeSpan.FromMilliseconds(400);
    public TimeSpan RepeatInterval { get; set; } = TimeSpan.FromMilliseconds(100);

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