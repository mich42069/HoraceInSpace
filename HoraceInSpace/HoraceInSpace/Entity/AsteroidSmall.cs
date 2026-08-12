using System;
using HoraceInSpace.Assets;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpace.Helpers;
using HoraceInSpacePhysicsLib;
using HoraceInSpacePhysicsLib.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

/// <summary>
/// Represents an instance of a Small Asteroid.
/// Is a Scorable Entity.
/// </summary>
public class AsteroidSmall : Asteroid, IScore
{
    protected override Texture2D Texture => Textures.AsteroidSmall;
    protected override Texture2D ShadowTexture => Textures.ShadowOverlaySmall;
    protected override Vector2 TextureOrigin => Textures.AsteroidSmallOrigin;
    public int Score => 125;
    protected override distance Radius => SpaceValues.SmallAsteroidRadius;

    public AsteroidSmall(Position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
        Hitbox.Position = initialPosition;
    }
}