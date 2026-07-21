using HoraceInSpacePhysicsLib;

namespace HoraceInSpace;

public interface IHitBox
{
    public bool CheckHit(IHitBox hitBox);
    public bool CheckHit(position point);
}