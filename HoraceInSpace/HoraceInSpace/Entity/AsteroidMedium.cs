using System;
using System.Collections.Generic;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class AsteroidMedium : AAsteroid, IScore
{
    protected override Texture2D Texture => Textures.AsteroidMedium;
    protected override Texture2D ShadowTexture => Textures.ShadowOverlayMedium;
    protected override Vector2 TextureOrigin => Textures.AsteroidMediumOrigin;
    public int Score => 250;

    public AsteroidMedium(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
    }

    protected override distance Radius => SpaceValues.MediumAsteroidRadius;

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
}