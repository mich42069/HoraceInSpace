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
        Radius = SpaceValues.UfoRadius;
        Hitbox = new CircleHitbox(Radius);
        Score = 1000;
        Density = 2000.KilogramsPerCubicMeter();
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