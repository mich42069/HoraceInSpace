using System;
using System.Collections.Generic;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class AsteroidBig : Asteroid, IScore
{
    protected override Texture2D Texture => Textures.AsteroidBig;
    protected override Texture2D ShadowTexture => Textures.ShadowOverlayBig;
    protected override Vector2 TextureOrigin => Textures.AsteroidBigOrigin;
    public int Score => 500;

    public AsteroidBig(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
    }

    protected override distance Radius => SpaceValues.BigAsteroidRadius;

    public override List<Entity> SplitUp()
    {
        List<Entity> newAsteroids = new();
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
}