namespace HoraceInSpace;

using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class AsteroidBig : AAsteroid
{
    public AsteroidBig(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = 80.Meters();
        Hitbox = new CircleHitbox(Radius);
        Mass = 64_000.Kilograms();
        Area = Radius * Radius * Math.PI;
        Score = 500;
    }
    
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Textures.AsteroidBig,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            Textures.AsteroidBigOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }
}