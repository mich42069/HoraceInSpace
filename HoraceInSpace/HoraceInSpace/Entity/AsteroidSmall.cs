using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class AsteroidSmall : AAsteroid
{

    public AsteroidSmall(position screenSize, position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(screenSize, initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = 20.Meters();
        HitBox = new CircleHitBox(Position, Radius);
        Mass = 3500.Kilograms();
        Area = Radius * Radius * Math.PI;
    }
    
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Textures.AsteroidSmall,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            Textures.AsteroidSmallOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }
}