using System;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class Ufo : AUfo
{
    public Ufo(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = 30.Meters();
        Hitbox = new CircleHitbox(Radius);
        Mass = 16_000.Kilograms();
        Area = Radius * Radius * Math.PI;
        Score = 1000;
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