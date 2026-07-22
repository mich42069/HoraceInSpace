using System;
using System.Collections.Generic;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class StarsBackground
{
    private Random _random = new Random();
    private List<IStar> _stars = new();
    private readonly int _numberOfStars;
    private readonly double _nonFlickerChance;
    private const int SpecialEffectCoefficient = 10;
    public StarsBackground(GraphicsDevice device, Vector2 screenSize, int count)
    {
        _numberOfStars = count;
        _nonFlickerChance = 1 - SpecialEffectCoefficient / (double)_numberOfStars;
        for (int i = 0; i < count; i++)
        {
            Vector2 position = new Vector2(_random.Next(0, (int)screenSize.X), _random.Next(0, (int)screenSize.Y));
            IStar tempStar = new Star(device,  position);
            _stars.Add(tempStar);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IStar star in _stars) 
        {
            star.Draw(spriteBatch);
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (IStar star in _stars)
        {
            star.Update(gameTime);
            if (_random.NextDouble() > _nonFlickerChance)
            {
                star.SpecialEffect(gameTime, IntensityCalculation(), TimeCalculation());
            }
        }

        return;

        float IntensityCalculation() => _random.NextSingle() + _random.NextSingle() - 1;
        // float IntensityCalculation() => 1;
        TimeSpan TimeCalculation() => _random.NextSingle().Seconds() * 3;
    }
}