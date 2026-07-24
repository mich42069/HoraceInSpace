using HoraceInSpacePhysicsLib;

namespace HoraceInSpace.Entity;

public class AUfo : AEntity
{
    
    protected AUfo(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed)
    {
        Position = initialPosition;
        AngleOfMotion = angleOfMotion;
        AngleOfRotation = angleOfRotation;
        Speed = initialSpeed;
    }
}