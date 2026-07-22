using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class AsteroidMedium : AAsteroid
{

    public AsteroidMedium(position screenSize, position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(screenSize, initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = 40.Meters();
        HitBox = new CircleHitBox(Position, Radius);
        Mass = 16_000.Kilograms();
        Area = Radius * Radius * Math.PI;
    }
    
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Textures.AsteroidMedium,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            Textures.AsteroidMediumOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }

}