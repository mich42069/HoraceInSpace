using System;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using HoraceInSpace.Background;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace.States.States;

public class LoadState(GameArguments arguments, StarsBackground starsBackground) : State(arguments)
{
    private KeyboardState _previousKeyboard = Keyboard.GetState();
    private MouseState _previousMouse = Mouse.GetState();
    private bool _showText = false;
    private const string PressAnyKeyText = "Press ANY key to continue.";
    private readonly TimeSpan _loadInTime = 1.Seconds();
    private TimeSpan _startLoadTime;
    private int _whiteShade;

    public override void CheckInputs(GameTime gameTime)
    {
        if (AnyInputThisFrame() && starsBackground.AllLoaded)
        {
            SwitchState = true;
        }
    }

    public override void Update(GameTime gameTime)
    {
        if (!_showText && starsBackground.AllLoaded)
        {
            _startLoadTime = gameTime.TotalGameTime;
            _showText = true;
        }

        if (_showText)
        {
            double howMuchInLoadTime = (gameTime.TotalGameTime - _startLoadTime).TotalMilliseconds / _loadInTime.TotalMilliseconds;
            if (howMuchInLoadTime > 1) 
                _whiteShade = 255;
            else _whiteShade = (int)(255 * howMuchInLoadTime);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (_showText)
            DrawCentered(spriteBatch, PressAnyKeyText, Center.Y, new (_whiteShade, _whiteShade, _whiteShade));
    }


    private bool AnyInputThisFrame()
    {
        KeyboardState keyboard = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        bool anyInputThisFrame =
            keyboard.GetPressedKeys().Any(key => _previousKeyboard.IsKeyUp(key)) ||
            (mouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released) ||
            (mouse.RightButton == ButtonState.Pressed && _previousMouse.RightButton == ButtonState.Released) ||
            (mouse.MiddleButton == ButtonState.Pressed && _previousMouse.MiddleButton == ButtonState.Released);
        
        _previousKeyboard = keyboard;
        _previousMouse = mouse;
        
        return anyInputThisFrame;
    }

    public override State NewState()
    {
        return new MenuState(Arguments);
    }
}