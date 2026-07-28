using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Background;

public class ShootingStar : IStar
{
    private static readonly Random Random = new();

    private readonly Texture2D _pixel;
    private readonly Vector2 _position;

    private readonly Vector2 _direction;
    private readonly angle _rotation;

    private bool _active;
    private TimeSpan _startTime;
    private readonly TimeSpan _duration = 2.Seconds();

    private float _progress;
    private float _alpha;
    private Vector2 _currentPosition;

    private const float TravelDistance = 1000f;
    private const float TrailLength = 50f;
    private const float TrailWidth = 2f;

    public ShootingStar(GraphicsDevice graphicsDevice, Vector2 position)
    {
        _position = position;
        _currentPosition = position;

        _pixel = new Texture2D(graphicsDevice, 1, 1);
        _pixel.SetData([Color.White]);
        
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

    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length)
    {
        if (intensity < -1 || intensity > 1) throw new ArgumentOutOfRangeException(nameof(intensity), "Is out of range, the correct value should be in interval [-1, 1]");

        if (_active)
            return;

        _active = true;

        // Shooting stars are always very short flashes
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

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_active)
            return;

        Vector2 tail = _currentPosition - _direction * TrailLength;
        Vector2 center = (_currentPosition + tail) * 0.5f;

        float length = Vector2.Distance(_currentPosition, tail);

        spriteBatch.Draw(
            _pixel,
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