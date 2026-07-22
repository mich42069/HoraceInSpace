using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace;

public static class Textures
{
    public static Texture2D Horace { get; set; }
    public static Vector2 HoraceOrigin => new(Horace.Bounds.Center.X, Horace.Bounds.Center.Y);
    public static Texture2D AsteroidSmall { get; set; }
    public static Texture2D AsteroidMedium { get; set; }
    public static Texture2D AsteroidBig { get; set; }
    public static Texture2D Ufo { get; set; }
}