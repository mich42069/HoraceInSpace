using System;
using HoraceInSpace.Entity;
using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public class AsteroidFactory
{
    private static Random _random = new Random();
    public static AAsteroid CreateAsteroid()
    {
        position worldSize = SpaceValues.WorldSize;
        double whatToSpawn = _random.NextSingle();
        position spawnPosition = (_random.NextSingle() * worldSize.X, _random.NextSingle() * worldSize.Y).At();
        angle angleOfMotion = (_random.NextSingle() * 360).Degrees();
        angle angleOfRotation = (_random.NextSingle() * 360).Degrees();
        speed initialSpeed = _random.NextSingle() * 200.MetersPerSecond();
        if (whatToSpawn < 0.25) return new AsteroidSmall(spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);
        if (whatToSpawn < 0.5) return new AsteroidMedium(spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);
        if (whatToSpawn < 0.75) return new AsteroidBig(spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);
        return new Ufo(spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);

    }
}