using System;
using System.ComponentModel;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using HoraceInSpace.Assets;
using HoraceInSpace.Helpers;
using HoraceInSpace.States;
using KeyboardInput = HoraceInSpace.Helpers.KeyboardInput;

namespace HoraceInSpace;

/// <summary>
/// Class running the Game
/// </summary>
public class HoraceInSpace : Game
{
    // No sleep when app open
    [DllImport("kernel32.dll")]
    private static extern uint SetThreadExecutionState(uint esFlags);

    // These are flags used to ensure that the computer doesn't go to sleep while ingame.
    private const uint EsContinuous = 0x80000000;
    private const uint EsSystemRequired = 0x00000001;
    private const uint EsDisplayRequired = 0x00000002;

    private const int NumberOfStars = 1536;
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

    /// <summary>
    /// Initializes the window Textures and creates an instance of StateSwitcher.
    /// </summary>
    protected override void Initialize()
    {
        Window.TextInput += TextInput;
        _screenSize = new Vector2(
            GraphicsDevice.Viewport.Width,
            GraphicsDevice.Viewport.Height);
        _stateSwitcher = new StateSwitcher(_screenSize, NumberOfStars, _arguments);
        Textures.Initialize(GraphicsDevice);
        base.Initialize();
    }
    
    private void TextInput(object sender, TextInputEventArgs e)
    {
        _stateSwitcher.TextInput(e.Character);
    }

    /// <summary>
    /// Loads in all content, that is Font, Textures and Sounds.
    /// </summary>
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Assets.Assets.Font = Content.Load<SpriteFont>("Minecraft");
        Textures.LoadContent(Content);
        Sounds.LoadContent(Content);
        _stateSwitcher.SetSpriteBatch(_spriteBatch);
    }

    /// <summary>
    /// Updates Keyboard, checks if it can exit and updates StateSwitcher.
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Update(GameTime gameTime)
    {
        KeyboardInput.Update();
        if (_stateSwitcher.CanExit()) Exit();
        _stateSwitcher.Update(gameTime);
        base.Update(gameTime);
    }

    /// <summary>
    /// Clears the background and then draws the game, by calling StateSwitcher.
    /// </summary>
    /// <param name="gameTime"></param>
    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin(
            samplerState: SamplerState.PointClamp);
        GraphicsDevice.Clear(Color.Black);
        _stateSwitcher.Draw();
        base.Draw(gameTime);
        _spriteBatch.End();
    }
    
    /// <summary>
    /// Handling that the PC won't go to sleep when focused
    /// </summary>
    protected override void OnActivated(object sender, EventArgs args)
    {
        SetThreadExecutionState(
            EsContinuous |
            EsSystemRequired |
            EsDisplayRequired);

        base.OnActivated(sender, args);
    }

    /// <summary>
    /// Re-enables sleep functionality
    /// </summary>
    protected override void OnDeactivated(object sender, EventArgs args)
    {
        SetThreadExecutionState(EsContinuous);

        base.OnDeactivated(sender, args);
    }
}