using System;
using HoraceInSpace.Entity.Hitbox;
using HoraceInSpacePhysicsLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Entity;

public class AsteroidSmall : AAsteroid, IScore
{
    protected override Texture2D Texture => Textures.AsteroidSmall;
    protected override Texture2D ShadowTexture => Textures.ShadowOverlaySmall;
    protected override Vector2 TextureOrigin => Textures.AsteroidSmallOrigin;
    public int Score => 125;
    protected override distance Radius => SpaceValues.SmallAsteroidRadius;

    public AsteroidSmall(position initialPosition, angle angleOfMotion, angle angleOfRotation, speed initialSpeed) : base(initialPosition, angleOfMotion, angleOfRotation, initialSpeed)
    {
        Acceleration = 0.MetersPerSecondSquared();
        Hitbox = new CircleHitbox(Radius);
    }
}