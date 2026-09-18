using System.Collections.Generic;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity.Entities;

/// <summary>
/// Represents an instance of an Enormous Asteroid.
/// Is a Scorable Entity.
/// </summary>
public class AsteroidEnormous : Asteroid, IScore
{
    
    protected override Texture2D Texture => Textures.AsteroidEnormous;
    protected override Texture2D ShadowTexture => Textures.ShadowOverlayEnormous;
    protected override Vector2 TextureOrigin => Textures.AsteroidEnormousOrigin;
    public int Score => 2000;

    public AsteroidEnormous(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
        Hitbox.Position = initialPosition;
    }

    protected override distance Radius => SpaceValues.EnormousAsteroidRadius;

    public override List<Entity> SplitUp()
    {
        List<Entity> newAsteroids = new();
        int numberOfNewAsteroids = 5;
        speed newAsteroidsSpeed = CalculateNewAsteroidSpeed(SpaceValues.GiantAsteroidRadius);
        newAsteroidsSpeed /= numberOfNewAsteroids;
        newAsteroids.Add(new AsteroidGiant(Position, AngleOfMotion - 144.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidGiant(Position, AngleOfMotion - 72.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidGiant(Position, AngleOfMotion, AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidGiant(Position, AngleOfMotion + 72.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidGiant(Position, AngleOfMotion + 144.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        return newAsteroids;
    }
}