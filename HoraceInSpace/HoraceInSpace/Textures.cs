using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public static class Textures
{
    public static Texture2D Horace { get; set; }
    public static Vector2 HoraceOrigin => new(Horace.Bounds.Center.X, Horace.Bounds.Center.Y);
    public static Texture2D AsteroidSmall { get; set; }
    public static Vector2 AsteroidSmallOrigin => new(AsteroidSmall.Bounds.Center.X, AsteroidSmall.Bounds.Center.Y);
    public static Texture2D AsteroidMedium { get; set; }
    public static Vector2 AsteroidMediumOrigin => new(AsteroidMedium.Bounds.Center.X, AsteroidMedium.Bounds.Center.Y);
    public static Texture2D AsteroidBig { get; set; }
    public static Vector2 AsteroidBigOrigin => new(AsteroidBig.Bounds.Center.X, AsteroidBig.Bounds.Center.Y);
    public static Texture2D Ufo { get; set; }
    public static Vector2 UfoOrigin => new(Ufo.Bounds.Center.X, Ufo.Bounds.Center.Y);
    public static Texture2D Bullet { get; set; }
    public static Vector2 BulletOrigin => new(Bullet.Bounds.Center.X, Bullet.Bounds.Center.Y);
    public static Texture2D Thruster { get; set; }
    public static Vector2 ThrusterOrigin => new(Thruster.Bounds.Center.X, Thruster.Bounds.Center.Y);
    
    public static Texture2D Pixel;
    public static void Initialize(GraphicsDevice graphicsDevice){
        Pixel = new Texture2D(graphicsDevice, 1, 1);
        Pixel.SetData(new[] { Color.LightCyan });
    }
}