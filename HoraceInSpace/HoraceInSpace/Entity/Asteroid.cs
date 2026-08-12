using System;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

/// <summary>
/// Abstract representation of an asteroid, defining density, and the existence of a ShadowTexture.
/// Defines Asteroid specific draw, and constructor.
/// </summary>
public abstract class Asteroid : Entity
{
    protected abstract Texture2D ShadowTexture { get; }
    protected Asteroid(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
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

    protected override void Draw(Position pos, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            PhysicsLibToMonogame.ToVector2(pos),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            TextureOrigin,
            TextureScale,
            SpriteEffects.None,
            0f);

        spriteBatch.Draw(
            ShadowTexture,
            PhysicsLibToMonogame.ToVector2(pos),
            null,
            Color.White,
            (float)AngleOfRotation.Value,
            TextureOrigin,
            TextureScale,
            SpriteEffects.None,
            0f);
    }
}