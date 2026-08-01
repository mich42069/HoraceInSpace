using System;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;

namespace HoraceInSpace.Entity.Hitbox;

/// <summary>
/// A Hitbox instance of a Circular shape
/// </summary>
/// <param name="radius">Radius of the circle</param>
public class CircleHitbox(distance radius) : Hitbox(radius)
{
}