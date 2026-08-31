using System;
using HoraceInSpace.Assets;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Background;

public class ShootingStar : IStar
{
    private static readonly Random Random = new();

    private static Texture2D Pixel => Textures.StarColorPixel;
    private readonly Vector2 _position;

    private readonly Vector2 _direction;
    private readonly angle _rotation;

    private bool _active;
    private TimeSpan _startTime;
    private TimeSpan _duration;

    private float _progress;
    private float _alpha;
    private Vector2 _currentPosition;

    private const float TravelDistance = 1000f;
    private const float TrailLength = 50f;
    private const float TrailWidth = 2f;

    public ShootingStar(Vector2 position, float baseSpecialEffectChance)
    {
        NonSpecialEffectChance = 1 - baseSpecialEffectChance / 2;
        
        _position = position;
        _currentPosition = position;
        
        // Mostly downward diagonal movement
        angle angle = Random.Next(25, 70).Degrees();

        _direction = new Vector2(
            (float)angle.Cos(),
            (float)angle.Sin()
        );

        // Randomly shoot left or right
        if (Random.Next(2) == 0)
            _direction.X *= -1f;

        _rotation = (MathF.Atan2(_direction.Y, _direction.X)).Radians();
    }

    /// <summary>
    /// Special effect of a ShootingStar, draws the star for 'length' seconds.
    /// </summary>
    /// <param name="gameTime">Start time of the shooting star.</param>
    /// <param name="intensity">Doesn't do anything here, still must be [-1, 1].</param>
    /// <param name="length">Length in seconds of how long the process of the shooting star is.</param>
    /// <exception cref="ArgumentOutOfRangeException">Intensity was out of range [-1, 1]</exception>
    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length)
    {
        if (intensity is < -1 or > 1) throw new ArgumentOutOfRangeException(nameof(intensity), "Is out of range, the correct value should be in interval [-1, 1]");

        if (_active) // If triggered while active it returns.
            return;

        _active = true;
        _duration = length;
        _startTime = gameTime.TotalGameTime;

        _progress = 0f;
        _alpha = 1f;

        _currentPosition = _position;
    }

    public void Update(GameTime gameTime)
    {
        if (!_active)
            return;

        float elapsed = (float)(gameTime.TotalGameTime - _startTime).TotalSeconds;
        float duration = (float)_duration.TotalSeconds;

        _progress = MathHelper.Clamp(elapsed / duration, 0f, 1f);

        _currentPosition = _position + _direction * (TravelDistance * _progress);

        // Fade quickly near the end
        _alpha = 1f - _progress;

        if (_progress >= 1f)
        {
            _active = false;
            _alpha = 0f;
            _currentPosition = _position;
        }
    }

    public double NonSpecialEffectChance { get; private set; }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_active)
            return;

        Vector2 tail = _currentPosition - _direction * TrailLength;
        Vector2 center = (_currentPosition + tail) * 0.5f;

        float length = Vector2.Distance(_currentPosition, tail);

        spriteBatch.Draw(
            Pixel,
            center,
            null,
            Color.White * _alpha,
            (float)_rotation.Value,
            new Vector2(0.5f, 0.5f),
            new Vector2(length, TrailWidth),
            SpriteEffects.None,
            0f
        );
    }
}