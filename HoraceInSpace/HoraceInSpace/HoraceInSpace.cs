using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using HoraceInSpace.Assets;
using HoraceInSpace.States;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace;

public class HoraceInSpace : Game
{
    // No sleep when app
    [DllImport("kernel32.dll")]
    private static extern uint SetThreadExecutionState(uint esFlags);

    private const uint ES_CONTINUOUS = 0x80000000;
    private const uint ES_SYSTEM_REQUIRED = 0x00000001;
    private const uint ES_DISPLAY_REQUIRED = 0x00000002;

    private const int NumberOfStars = 1024;
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Vector2 _screenSize;

    private readonly GameArguments _arguments;
    
    private IStateSwitcher _stateSwitcher;

    public HoraceInSpace(string[] args)
    {
        _arguments = GameArguments.Parse(args);
        _graphics = new GraphicsDeviceManager(this);

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.HardwareModeSwitch = false; // Borderless fullscreen
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        Window.TextInput += TextInput;
        _screenSize = new Vector2(
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height);
        _stateSwitcher = new StateSwitcher(GraphicsDevice, _screenSize, NumberOfStars, _arguments);
        Textures.Initialize(GraphicsDevice);
        Assets.Assets.Initialize(GraphicsDevice);
        base.Initialize();
    }
    
    private void TextInput(object sender, TextInputEventArgs e)
    {
        _stateSwitcher.TextInput(e.Character);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Assets.Assets.Font = Content.Load<SpriteFont>("Minecraft");
        Textures.LoadContent(Content);
        Sounds.LoadContent(Content);
        _stateSwitcher.SetSpriteBatch(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardInput.Update(gameTime);
        if (_stateSwitcher.CanExit()) Exit();
        _stateSwitcher.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp);
        GraphicsDevice.Clear(Color.Black);
        _stateSwitcher.Draw(gameTime);
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