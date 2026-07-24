using System;
using System.Collections.Generic;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class AsteroidBig : AAsteroid
{
    public AsteroidBig(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = SpaceValues.BigAsteroidRadius;
        Hitbox = new CircleHitbox(Radius);
        Score = 500;
    }

    public override List<AEntity> SplitUp()
    {
        List<AEntity> newAsteroids = new();
        int numberOfNewAsteroids = 5;
        speed newAsteroidsSpeed = CalculateNewAsteroidSpeed(SpaceValues.MediumAsteroidRadius);
        newAsteroidsSpeed /= numberOfNewAsteroids;
        newAsteroids.Add(new AsteroidMedium(Position, AngleOfMotion - 144.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidMedium(Position, AngleOfMotion - 72.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidMedium(Position, AngleOfMotion, AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidMedium(Position, AngleOfMotion + 72.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidMedium(Position, AngleOfMotion + 144.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        return newAsteroids;
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