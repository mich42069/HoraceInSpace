using System.Collections.Generic;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

/// <summary>
/// Represents an instance of a Giant Asteroid.
/// Is a Scorable Entity.
/// </summary>
public class AsteroidGiant : Asteroid, IScore
{
    protected override Texture2D Texture => Textures.AsteroidGiant;
    protected override Texture2D ShadowTexture => Textures.ShadowOverlayGiant;
    protected override Vector2 TextureOrigin => Textures.AsteroidGiantOrigin;
    public int Score => 1000;

    public AsteroidGiant(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
        Hitbox.Position = initialPosition;
    }

    protected override distance Radius => SpaceValues.GiantAsteroidRadius;

    public override List<Entity> SplitUp()
    {
        List<Entity> newAsteroids = new();
        int numberOfNewAsteroids = 5;
        speed newAsteroidsSpeed = CalculateNewAsteroidSpeed(SpaceValues.BigAsteroidRadius);
        newAsteroidsSpeed /= numberOfNewAsteroids;
        newAsteroids.Add(new AsteroidBig(Position, AngleOfMotion - 144.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidBig(Position, AngleOfMotion - 72.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidBig(Position, AngleOfMotion, AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidBig(Position, AngleOfMotion + 72.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        newAsteroids.Add(new AsteroidBig(Position, AngleOfMotion + 144.Degrees(), AngleOfRotation, newAsteroidsSpeed));
        return newAsteroids;
    }
}