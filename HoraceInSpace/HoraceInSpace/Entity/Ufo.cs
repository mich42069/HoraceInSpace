using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public class Ufo : AAsteroid
{

    public Ufo(position screenSize, position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(screenSize, initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = 30.Meters();
        HitBox = new CircleHitBox(Position, Radius);
        Mass = 16_000.Kilograms();
        Area = Radius * Radius * Math.PI;
    }
    
    
    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Textures.Ufo,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            Textures.UfoOrigin,
            1f,
            SpriteEffects.None,
            0f);
    }
}