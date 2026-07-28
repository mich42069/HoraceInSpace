using Microsoft.Xna.Framework.Graphics;

namespace HoraceInSpace.Assets;

public static class Assets
{
    public static SpriteFont Font;
    public static Texture2D Pixel { get; set; }

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        Pixel = new Texture2D(graphicsDevice, 1, 1);
    }
}