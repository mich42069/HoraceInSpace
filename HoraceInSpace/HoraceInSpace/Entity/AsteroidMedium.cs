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

public class AsteroidMedium : Asteroid, IScore
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

    public override List<Entity> SplitUp()
    {
        List<Entity> newAsteroids = new();
        int numberOfNewAsteroids = 3;
        speed newAsteroidsSpeed = CalculateNewAsteroidSpeed(SpaceValues.SmallAsteroidRadius);
        newAsteroidsSpeed /= numberOfNewAsteroids;
        newAsteroids.Add(new AsteroidSmall(Position, AngleOfMotion - 30.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidSmall(Position, AngleOfMotion, AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidSmall(Position, AngleOfMotion + 30.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        return newAsteroids;
    }
}