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
    }
}