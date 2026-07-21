using System;
using HoraceInSpacePhysicsLib;
using SharpDX.Direct2D1;

namespace HoraceInSpace;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class Star : IStar
{
    private const ushort DefaultColor = 160;
    private const ushort FlickerRange = 255 - DefaultColor;
    private const ushort StarSize = 3;
    private readonly Texture2D _pixel;
    private readonly Vector2 _position;
    private readonly Color _defaultColor = new (DefaultColor, DefaultColor, DefaultColor);
    private Color _color = new (DefaultColor, DefaultColor, DefaultColor);
    private Color _flickerColor;
    private bool _flicker = false;
    private double _flickerTime = 0;
    private time _flickerLength; // Milliseconds

    public Star(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _position = position;
        _pixel = new Texture2D(graphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }

    public void SpecialEffect(GameTime gameTime, float intensity, time length)
    {
        if (_flicker) return;
        
        _flickerLength = length;
        
        if (intensity < -1 || intensity > 1) intensity = 0;
        int newFlickerColor = (int)(DefaultColor + intensity * (int)FlickerRange);
        _flickerColor = new (newFlickerColor, newFlickerColor, newFlickerColor);
        
        _flicker = true;
        _flickerTime = gameTime.TotalGameTime.TotalMilliseconds;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel, new Rectangle((int)_position.X, (int)_position.Y, StarSize, StarSize), _color);
    }

    public void Update(GameTime gameTime)
    {
        if (_flicker)
        {
            time elapsed = (gameTime.TotalGameTime.TotalMilliseconds - _flickerTime).Milliseconds();
            float t = MathHelper.Clamp((float)(elapsed / _flickerLength), 0f, 1f);

            _color = Color.Lerp(_flickerColor, _defaultColor, t);
            if (t == 1f) _flicker = false;
        }   
    }
}