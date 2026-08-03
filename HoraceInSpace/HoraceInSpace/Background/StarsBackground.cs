using System;
using System.Collections.Generic;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Background;

public class StarsBackground
{
    private readonly Random _random = new Random();
    private readonly List<IStar> _stars = new();
    private readonly double _nonSpecialEffectChance;
    private const int SpecialEffectCoefficient = 1;

    /// <summary>
    /// Creates a new StarsBackground based on the number of Stars we want to draw.
    /// There is also 100 times less Shooting stars than normal Stars generated.
    /// </summary>
    /// <param name="screenSize">Size of the screen, so the stars all generated in it.</param>
    /// <param name="numberOfStars">How many stars to draw</param>
    public StarsBackground(Vector2 screenSize, int numberOfStars)
    {
        _nonSpecialEffectChance = 1 - SpecialEffectCoefficient / (double)numberOfStars;
        for (int i = 0; i < numberOfStars; i++)
        {
            Vector2 position = new Vector2(_random.Next(0, (int)screenSize.X), _random.Next(0, (int)screenSize.Y));
            _stars.Add(new Star(position));
        }

        for (int i = 0; i < numberOfStars / 100; i++)
        {
            Vector2 position = new Vector2(_random.Next(0, (int)screenSize.X), _random.Next(0, (int)screenSize.Y));
            _stars.Add(new ShootingStar(position));
        }
    }

    /// <summary>
    /// Draws all stars from background.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch responsible for drawing the stars.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IStar star in _stars) 
        {
            star.Draw(spriteBatch);
        }
    }

    /// <summary>
    /// Updates all stars, as well as triggering their SpecialEffects randomly.
    /// </summary>
    /// <param name="gameTime">GameTime used for updating and creating special effects on stars.</param>
    public void Update(GameTime gameTime)
    {
        foreach (IStar star in _stars)
        {
            star.Update(gameTime);
            if (_random.NextDouble() > _nonSpecialEffectChance)
            {
                star.SpecialEffect(gameTime, IntensityCalculation(), TimeCalculation());
            }
        }

        return;

        float IntensityCalculation() => _random.NextSingle() + _random.NextSingle() - 1;
        TimeSpan TimeCalculation() => _random.NextSingle().Seconds() * 3;
    }
}