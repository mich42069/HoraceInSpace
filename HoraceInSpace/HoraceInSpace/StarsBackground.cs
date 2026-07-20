using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class StarsBackground
{
    Random rand = new Random();
    List<IStar> _stars = new();
    private const double NonFlickerChance = 0.999;
    public StarsBackground(GraphicsDevice device, Vector2 screenSize, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 position = new Vector2(rand.Next(0, (int)screenSize.X), rand.Next(0, (int)screenSize.Y));
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
        double intensity;
        foreach (IStar star in _stars)
        {
            star.Update(gameTime);
            if ((intensity = rand.NextDouble()) > NonFlickerChance)
            {
                star.Flicker(gameTime, intensity + (1-NonFlickerChance)/2);
            }
        }
    }
}