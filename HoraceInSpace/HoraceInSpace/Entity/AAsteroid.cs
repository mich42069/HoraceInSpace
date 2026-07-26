using System;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public abstract class AAsteroid : AEntity
{
    protected abstract Texture2D ShadowTexture { get; }
    protected AAsteroid(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
    }

    protected override density Density => SpaceValues.AsteroidDensity;
    
    protected speed CalculateNewAsteroidSpeed(distance newAsteroidRadius)
    {
        volume newAsteroidVolume =
            4 / (double)3 * Double.Pi * newAsteroidRadius * newAsteroidRadius * newAsteroidRadius;
        mass newAsteroidMass = Density * newAsteroidVolume;
        return Momentum / newAsteroidMass;
    }

    protected override void Draw(position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            TextureOrigin,
            TextureScale,
            SpriteEffects.None,
            0f);

        spriteBatch.Draw(
            ShadowTexture,
            pos.ToVector2(),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            TextureOrigin,
            TextureScale,
            SpriteEffects.None,
            0f);
    }
}