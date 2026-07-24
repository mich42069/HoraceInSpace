using System;
using HoraceInSpacePhysicsLib;

namespace HoraceInSpace.Entity;

public abstract class AAsteroid : AEntity
{
    protected AAsteroid(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
        Density = SpaceValues.AsteroidDensity;
    }
    
    protected speed CalculateNewAsteroidSpeed(distance newAsteroidRadius)
    {
        volume newAsteroidVolume =
            4 / (double)3 * Double.Pi * newAsteroidRadius * newAsteroidRadius * newAsteroidRadius;
        mass newAsteroidMass = Density * newAsteroidVolume;
        return Momentum / newAsteroidMass;
    }
}