using System;
using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public class AsteroidFactory
{
    private static Random _random = new Random();
    public static AAsteroid CreateAsteroid(position screenSize)
    {
        double whatToSpawn = _random.NextSingle();
        position spawnPosition = (_random.NextSingle() * screenSize.X, _random.NextSingle() * screenSize.Y).At();
        angle angleOfMotion = (_random.NextSingle() * 360).Degrees();
        angle angleOfRotation = (_random.NextSingle() * 360).Degrees();
        speed initialSpeed = _random.NextSingle() * 200.MetersPerSecond();
        if (whatToSpawn < 0.25) return new AsteroidSmall(screenSize, spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);
        if (whatToSpawn < 0.5) return new AsteroidMedium(screenSize, spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);
        if (whatToSpawn < 0.75) return new AsteroidBig(screenSize, spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);
        return new Ufo(screenSize, spawnPosition, angleOfMotion, angleOfRotation, initialSpeed);

    }
}