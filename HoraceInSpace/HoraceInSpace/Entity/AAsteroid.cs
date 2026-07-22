using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public abstract class AAsteroid : AEntity
{
    protected AAsteroid(position screenSize, position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        MapBoundingBox = screenSize;
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
    }
}