using System;
using System.Collections.Generic;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class AsteroidMedium : AAsteroid
{
    public AsteroidMedium(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Radius = SpaceValues.MediumAsteroidRadius;
        Hitbox = new CircleHitbox(Radius);
        Score = 250;
    }
    
    public override List<AEntity> SplitUp()
    {
        List<AEntity> newAsteroids = new();
        int numberOfNewAsteroids = 3;
        speed newAsteroidsSpeed = CalculateNewAsteroidSpeed(SpaceValues.SmallAsteroidRadius);
        newAsteroidsSpeed /= numberOfNewAsteroids;
        newAsteroids.Add(new AsteroidSmall(Position, AngleOfMotion - 30.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidSmall(Position, AngleOfMotion, AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidSmall(Position, AngleOfMotion + 30.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        return newAsteroids;
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