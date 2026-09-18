using System;
using HoraceInSpace.Assets;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace HoraceInSpace.Background;

public class Star : IStar
{
    private const ushort DefaultColor = 100;
    private const ushort FlickerRange = 255 - DefaultColor;
    private ushort _starSize;
    private static Texture2D Pixel => Textures.StarColorPixel;
    private readonly Vector2 _position;
    private readonly Color _defaultColor = new (DefaultColor, DefaultColor, DefaultColor);
    private Color _color = new (DefaultColor, DefaultColor, DefaultColor);
    private Color _flickerColor;
    private bool _flicker = false;
    private TimeSpan _flickerTime = 0.Seconds();
    private TimeSpan _flickerLength; // Milliseconds
    private TimeSpan _loadTime;
    private bool _drawn = false;

    public Star(Vector2 position, float baseSpecialEffectChance, int size, TimeSpan loadTime)
    {
        _loadTime = loadTime;
        NonSpecialEffectChance = 1 - baseSpecialEffectChance * 4;
        _starSize = (ushort)size;
        _position = position;
    }

    /// <summary>
    /// Special effect of a Star, flickers the star in the for length seconds.
    /// </summary>
    /// <param name="gameTime">Start time of the shooting star.</param>
    /// <param name="intensity">[-1, 1] - negative number lowers light intensity, positive number highers it.</param>
    /// <param name="length">Length of the flicker in seconds</param>
    /// <exception cref="ArgumentOutOfRangeException">Intensity was out of range [-1, 1]</exception>
    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length)
    {
        if (intensity is < -1 or > 1) throw new ArgumentOutOfRangeException(nameof(intensity), "Is out of range, the correct value should be in interval [-1, 1]");

        if (_flicker) return;
        
        _flickerLength = length;
        int newFlickerColor = (int)(DefaultColor + intensity * (int)FlickerRange);
        _flickerColor = new (newFlickerColor, newFlickerColor, newFlickerColor);
        
        _flicker = true;
        _flickerTime = gameTime.TotalGameTime;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_drawn) return;
        spriteBatch.Draw(Pixel, new Rectangle((int)_position.X, (int)_position.Y, _starSize, _starSize), _color);
    }

    public void Update(GameTime gameTime)
    {
        if (!_drawn && _loadTime < gameTime.TotalGameTime) _drawn = true;
        if (_flicker)
        {
            TimeSpan elapsed = gameTime.TotalGameTime - _flickerTime;
            float t = MathHelper.Clamp((float)(elapsed / _flickerLength), 0f, 1f);

            _color = Color.Lerp(_flickerColor, _defaultColor, t);
            if (t >= 1f) _flicker = false;
        }   
    }

    public double NonSpecialEffectChance { get; private set; }
}