namespace HoraceInSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class Star : IStar
{
    private readonly Texture2D _pixel;
    private readonly Vector2 _position;
    private readonly Color _defaultColor = new (160, 160, 160);
    private Color _color = new (160, 160, 160);
    private Color _flickerColor = new (220, 220, 220);
    private  bool _flicker = false;
    private double _flickerTime = 0;
    private const double FlickerLengthMilliseconds = 250; // Milliseconds

    public Star(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _position = position;
        _pixel = new Texture2D(graphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }

    public void Flicker(GameTime gameTime, double intensity)
    {
        if (_flicker) return;
        _flickerColor = intensity > 1 ? new Color(20, 20, 20) : new Color(255, 255, 255);
        _flicker = true;
        _flickerTime = gameTime.TotalGameTime.TotalMilliseconds;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel, new Rectangle((int)_position.X, (int)_position.Y, 4, 4), _color);
    }

    public void Update(GameTime gameTime)
    {
        if (_flicker)
        {
            double elapsed = gameTime.TotalGameTime.TotalMilliseconds - _flickerTime;
            float t = MathHelper.Clamp((float)(elapsed / FlickerLengthMilliseconds), 0f, 1f);

            _color = Color.Lerp(_flickerColor, _defaultColor, t);
            if (t == 1f) _flicker = false;
        }   
    }
}