using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Entity.Hitbox;

/// <summary>
/// A Hitbox instance representing a point.
/// </summary>
/// <param name="radius">Regardless what is always overriden to 1 meter.</param>
public class PointHitbox(distance radius) : Hitbox(radius)
{
    protected override distance Radius { get; set; } = 1.Meters();
}