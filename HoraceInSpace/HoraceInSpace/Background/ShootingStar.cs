using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Background;

public class ShootingStar : IStar
{
    private static readonly Random Random = new();

    private readonly Texture2D _pixel;
    private readonly Vector2 _position;

    private Vector2 _direction;
    private float _rotation;

    private bool _active;
    private TimeSpan _startTime;
    private TimeSpan _duration;

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
        _pixel.SetData(new[] { Color.White });
    }

    public void SpecialEffect(GameTime gameTime, float intensity, TimeSpan length)
    {
        if (_active)
            return;

        _active = true;

        // Shooting stars are always very short flashes
        _startTime = gameTime.TotalGameTime;
        _duration = TimeSpan.FromSeconds(2);

        _progress = 0f;
        _alpha = 1f;

        // Mostly downward diagonal movement
        float angle = MathHelper.ToRadians(Random.Next(25, 70));

        _direction = new Vector2(
            MathF.Cos(angle),
            MathF.Sin(angle)
        );

        // Randomly shoot left or right
        if (Random.Next(2) == 0)
            _direction.X *= -1f;

        _rotation = MathF.Atan2(_direction.Y, _direction.X);

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
            _rotation,
            new Vector2(0.5f, 0.5f),
            new Vector2(length, TrailWidth),
            SpriteEffects.None,
            0f
        );
    }
}