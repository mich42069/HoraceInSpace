using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;

namespace HoraceInSpace;

public class Game1 : Game
{
    // No sleep when app
    [DllImport("kernel32.dll")]
    private static extern uint SetThreadExecutionState(uint esFlags);

    private const uint ES_CONTINUOUS = 0x80000000;
    private const uint ES_SYSTEM_REQUIRED = 0x00000001;
    private const uint ES_DISPLAY_REQUIRED = 0x00000002;

    private const int NumberOfStars = 1024;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Vector2 _screenSize;
    
    private IContextMachine _contextMachine;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.HardwareModeSwitch = false; // Borderless fullscreen
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _screenSize = new Vector2(
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height);
        _contextMachine = new ContextMachine(GraphicsDevice, _screenSize, NumberOfStars);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _contextMachine.SetSpriteBatch(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        if (_contextMachine.CanExit()) Exit();
        _contextMachine.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        GraphicsDevice.Clear(Color.Black);
        _contextMachine.Draw(gameTime);
        base.Draw(gameTime);
        _spriteBatch.End();
    }
    
    // Handling that the PC won't go to sleep when focused
    protected override void OnActivated(object sender, EventArgs args)
    {
        SetThreadExecutionState(
            ES_CONTINUOUS |
            ES_SYSTEM_REQUIRED |
            ES_DISPLAY_REQUIRED);

        base.OnActivated(sender, args);
    }

    // Re-enables sleep functionality
    protected override void OnDeactivated(object sender, EventArgs args)
    {
        SetThreadExecutionState(ES_CONTINUOUS);

        base.OnDeactivated(sender, args);
    }
}