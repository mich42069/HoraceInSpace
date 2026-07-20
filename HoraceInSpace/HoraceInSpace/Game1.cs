using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HoraceInSpace;

public class Game1 : Game
{
    private const int NumberOfStars = 512;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private StarsBackground _starsBackground;
    private Vector2 _screenSize;

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
        _starsBackground = new StarsBackground(GraphicsDevice, _screenSize, NumberOfStars);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _starsBackground.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        GraphicsDevice.Clear(Color.Black);
        _starsBackground.Draw(_spriteBatch);
        base.Draw(gameTime);
        _spriteBatch.End();
    }
}